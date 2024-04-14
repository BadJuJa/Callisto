using BadJuja.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.LevelManagement {
    public class RoomControls : MonoBehaviour, IGeneralRoomControl {
        public GameObject enemyPrefab;
        public Transform playerSpawnPoint;
        public List<Transform> spawnPoints;
        public List<RoomEnemiesList> enemiesToSpawn = new();
        public float timeBetweenSpawns = 2f;
        public Transform Enemies;

        public BoxCollider CameraConfinderCollider;

        private Camera playerCamera;

        public Transform PlayerSpawnPoint => playerSpawnPoint;

        private void OnEnable()
        {
            if (CameraConfinderCollider != null)
            {
                GlobalEvents.OnPlayerEnteredLevel += () => GlobalEvents.Send_OnNewCameraConfinderColliderAvailable(CameraConfinderCollider);
            }
        }
        private void OnDisable()
        {
            GlobalEvents.OnPlayerEnteredLevel -= () => GlobalEvents.Send_OnNewCameraConfinderColliderAvailable(CameraConfinderCollider);
        }

        private void Start()
        {
            playerCamera = Camera.main;
            StartCoroutine(WaitUntilSpawnEnemies(2f));
        }

        private void SpawnEnemies()
        {
            foreach (var enemy in enemiesToSpawn)
            {
                StartCoroutine(SpawnEnemyType(enemy));
            }

            StartCoroutine(CheckEnemyCount());
        }

        bool IsInView(Vector3 position)
        {
            Vector3 viewportPoint = playerCamera.WorldToViewportPoint(position);
            return viewportPoint.z > 0 && viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
        }

        private IEnumerator WaitUntilSpawnEnemies(float value)
        {
            yield return new WaitForSeconds(value);
            SpawnEnemies();
        }

        private IEnumerator CheckEnemyCount()
        {
            while (true)
            {
                if (Enemies.childCount < 1)
                {
                    GlobalEvents.Send_OnPlayerClearedTheRoom();
                    break;
                }
                yield return new WaitForSeconds(.5f);
            }
        }

        private IEnumerator SpawnEnemyType(RoomEnemiesList enemy)
        {
            List<Transform> validSpawnPoints = new List<Transform>(spawnPoints);
            int spawned = 0;
            while (spawned < enemy.Amount)
            {
                for (int i = 0; i < enemy.Amount - spawned; i++)
                {
                    int index = Random.Range(0, validSpawnPoints.Count);
                    Transform spawnPoint = validSpawnPoints[index];

                    if (!IsInView(spawnPoint.position))
                    {
                        var go = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation, Enemies);
                        go.GetComponent<IEnemyInit>().Initialize(enemy.Data);
                        spawned++;
                    }

                    yield return new WaitForSeconds(timeBetweenSpawns);
                }
            }
        }
    }
}