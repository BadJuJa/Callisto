using BadJuja.Core;
using BadJuja.Core.Data;
using BadJuja.Core.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.LevelManagement {
    public class RoomControls : MonoBehaviour, IGeneralRoomControl {
        [SerializeField] private BoxCollider _cameraConfinderCollider;

        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform[] _enemySpawnPoints;

        private Camera _playerCamera;

        private List<EnemyData> _enemies;
        private Transform EnemiesContainer;
        private float _timeBetweenSpawns;
        private int _enemyCount = 0;

        public Transform PlayerSpawnPoint => _playerSpawnPoint;

        private void OnDestroy()
        {
            PlayerRelatedEvents.OnEnteredLevel -= () => LevelLoadingRelatedEvents.Send_OnNewCameraConfinderColliderAvailable(_cameraConfinderCollider);
            EnemyRelatedEvents.OnEnemyDied -= (_) => _enemyCount--;
        }


        public void Initialize(List<EnemyData> enemyDatas)
        {
            #region Camera
            if (_cameraConfinderCollider != null)
            {
                PlayerRelatedEvents.OnEnteredLevel += () => LevelLoadingRelatedEvents.Send_OnNewCameraConfinderColliderAvailable(_cameraConfinderCollider);
            }
            _playerCamera = Camera.main;
            #endregion

            EnemyRelatedEvents.OnEnemyDied += (_) => _enemyCount--;

            EnemiesContainer = new GameObject("Enemies").transform;
            EnemiesContainer.SetParent(transform);

            _enemies = enemyDatas;
            _enemyCount = enemyDatas.Count;

            _timeBetweenSpawns = Mathf.Clamp(20 / _enemies.Count, 1, 2.5f);

            StartCoroutine(SpawnEnemies());
        }

        private IEnumerator SpawnEnemies()
        {
            bool spawned = false;
            
            yield return new WaitForSeconds(2f);

            foreach (var enemy in _enemies)
            {
                spawned = false;
                while (!spawned)
                {
                    Transform spawnPoint = GetValidSpawnPoint();
                    if (spawnPoint == null)
                    {
                        yield return new WaitForSeconds(1f);
                        continue;
                    }

                    var go = Instantiate(_enemyPrefab, spawnPoint.position, spawnPoint.rotation, EnemiesContainer);
                    go.GetComponent<IEnemyInit>().Initialize(enemy);

                    spawned = true;
                    yield return new WaitForSeconds(_timeBetweenSpawns);
                }
            }

            StartCoroutine(CheckEnemyCount());
        }


        private IEnumerator CheckEnemyCount()
        {
            while (true)
            {
                if (_enemyCount <= 0)
                {
                    PlayerRelatedEvents.Send_OnClearedTheRoom();
                    break;
                }
                yield return new WaitForSeconds(1f);
            }
        }

        bool IsInView(Vector3 position)
        {
            Vector3 viewportPoint = _playerCamera.WorldToViewportPoint(position);
            return viewportPoint.z > 0 && viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
        }
        private Transform GetValidSpawnPoint()
        {
            Transform validPoint = null;

            List<Transform> validSpawnPoints = new List<Transform>(_enemySpawnPoints);

            while(validPoint == null)
            {
                Transform spawnPoint = validSpawnPoints[Random.Range(0, validSpawnPoints.Count)];

                if (!IsInView(spawnPoint.position))
                {
                    validPoint = spawnPoint;
                    break;
                }
            }

            return validPoint;
        }

    }
}