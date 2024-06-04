using BadJuja.Core;
using BadJuja.Core.Data;
using BadJuja.Core.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.LevelManagement {
    public class RoomControls : MonoBehaviour, IGeneralRoomControl {
        [SerializeField] private BoxCollider _cameraConfinder;

        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform[] _enemySpawnPoints;

        private Camera _camera;

        private EnemyData[] _enemies;
        private int _enemyCount = 0;

        public Transform PlayerSpawnPoint => _playerSpawnPoint;

        private void OnDestroy()
        {
            PlayerRelatedEvents.OnEnteredLevel -= () => LevelLoadingRelatedEvents.Send_OnNewCameraConfinderColliderAvailable(_cameraConfinder);
            EnemyRelatedEvents.OnEnemyDied -= (_) => _enemyCount--;
        }

        public void Initialize(EnemyData[] enemyDatas)
        {
            #region Camera
            if (_cameraConfinder != null)
            {
                PlayerRelatedEvents.OnEnteredLevel += () => LevelLoadingRelatedEvents.Send_OnNewCameraConfinderColliderAvailable(_cameraConfinder);
            }
            _camera = Camera.main;
            #endregion

            EnemyRelatedEvents.OnEnemyDied += (_) => _enemyCount--;

            Transform enemiesContainer = new GameObject("Enemies").transform;
            enemiesContainer.SetParent(transform);

            _enemies = enemyDatas;
            _enemyCount = enemyDatas.Length;

            float timeBetweenSpawns = Mathf.Clamp(20 / _enemies.Length, 1, 2.5f);

            StartCoroutine(SpawnEnemies(enemiesContainer, timeBetweenSpawns));
        }

        private IEnumerator SpawnEnemies(Transform container = null, float timeBetweenSpawns = 0f)
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

                    var go = Instantiate(_enemyPrefab, spawnPoint.position, spawnPoint.rotation, container);
                    go.GetComponent<IEnemyInitialize>().Initialize(enemy);

                    spawned = true;
                    yield return new WaitForSeconds(timeBetweenSpawns);
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

        private bool IsInView(Vector3 position)
        {
            Vector3 viewportPoint = _camera.WorldToViewportPoint(position);
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