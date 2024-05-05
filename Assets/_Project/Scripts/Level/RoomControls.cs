using BadJuja.Core;
using BadJuja.Core.Data;
using BadJuja.Core.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.LevelManagement {
    public class RoomControls : MonoBehaviour, IGeneralRoomControl {
        public GameObject enemyPrefab;
        public Transform playerSpawnPoint;
        public List<Transform> spawnPoints;
        public float timeBetweenSpawns = 2f;
        public Transform Enemies;

        public BoxCollider CameraConfinderCollider;

        private Camera playerCamera;

        private List<EnemyData> _enemies;
        private int _enemyCount = 0;

        public Transform PlayerSpawnPoint => playerSpawnPoint;

        private void OnDestroy()
        {
            PlayerRelatedEvents.OnPlayerEnteredLevel -= () => LevelLoadingRelatedEvents.Send_OnNewCameraConfinderColliderAvailable(CameraConfinderCollider);
            EnemyRelatedEvents.OnEnemyDied -= (_) => _enemyCount--;
        }


        public void Initialize(List<EnemyData> enemyDatas)
        {
            #region Camera
            if (CameraConfinderCollider != null)
            {
                PlayerRelatedEvents.OnPlayerEnteredLevel += () => LevelLoadingRelatedEvents.Send_OnNewCameraConfinderColliderAvailable(CameraConfinderCollider);
            }
            playerCamera = Camera.main;
            #endregion

            EnemyRelatedEvents.OnEnemyDied += (_) => _enemyCount--;

            _enemies = enemyDatas;
            _enemyCount = enemyDatas.Count;


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

                    var go = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation, Enemies);
                    go.GetComponent<IEnemyInit>().Initialize(enemy);

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
                    PlayerRelatedEvents.Send_OnPlayerClearedTheRoom();
                    break;
                }
                yield return new WaitForSeconds(1f);
            }
        }

        bool IsInView(Vector3 position)
        {
            Vector3 viewportPoint = playerCamera.WorldToViewportPoint(position);
            return viewportPoint.z > 0 && viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
        }
        private Transform GetValidSpawnPoint()
        {
            Transform validPoint = null;

            List<Transform> validSpawnPoints = new List<Transform>(spawnPoints);

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