using BadJuja.Core.Data;
using BadJuja.Core.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.LevelManagement {
    public class LevelManager : MonoBehaviour {
        public GameObject[] Rooms;
        public GameObject[] Intersections;
        public Transform[] SpawnPoints;

        public EnemyList EasyEnemyList;
        public EnemyList NormalEnemyList;
        public EnemyList HardEnemyList;

        public Transform PlayerTransform;

        public Core.Settings.Settings Settings;

        private GameObject previousRoom;
        private GameObject currentRoom;

        private int roomIndex = 0;
        private int spawnPointIndex = 0;
        private Vector3 roomPlayerSpawnpoint;

        private void OnDestroy()
        {
            PlayerRelatedEvents.OnPlayerExitedRoom -= Proceed_p1;
            LevelLoadingRelatedEvents.OnScreenFadeOutFinished -= Proceed_p2;
        }

        public void Initialize()
        {
            PlayerRelatedEvents.OnPlayerExitedRoom += Proceed_p1;
            LevelLoadingRelatedEvents.OnScreenFadeOutFinished += Proceed_p2;
        }
        private void Proceed_p1()
        {
            if (PlayerTransform == null) return;

            PlayerTransform.gameObject.SetActive(false);
            
            // Затенить экран
            FadeOut();
        }

        private void Proceed_p2()
        {
            SpawnNewRoom();

            if (currentRoom.TryGetComponent(out IGeneralRoomControl controls))
            {
                roomPlayerSpawnpoint = controls.PlayerSpawnPoint.position;
            }


            PlayerTransform.position = roomPlayerSpawnpoint;

            Destroy(previousRoom);

            PlayerTransform.gameObject.SetActive(true);

            IEnumerator _()
            {
                yield return new WaitForSeconds(.5f);
                FadeIn();
            }

            StartCoroutine(_());

            PlayerRelatedEvents.Send_PlayerEnteredLevel();
        }

        private GameObject GetRandomRoom()
        {
            return Rooms[Random.Range(0, Rooms.Length)];
        }

        private GameObject GetRandomIntersection()
        {
            return Intersections[Random.Range(0, Intersections.Length)];
        }

        private List<EnemyData> GetNextRoomEnemies()
        {
            int count = 5 + roomIndex;

            int easyCount = 0;
            int normalCount = 0;
            int hardCount = 0;
            switch (Settings.DiffecultyLevel)
            {
                case Core.Settings.DiffecultyLevel.Easy:
                    easyCount = Mathf.RoundToInt(count * 0.7f);
                    normalCount = count - easyCount;
                    break;
                case Core.Settings.DiffecultyLevel.Normal:
                    normalCount = Mathf.RoundToInt(count * 0.6f);
                    easyCount = Mathf.RoundToInt(count * 0.2f);
                    hardCount = count - normalCount - easyCount;
                    break;
                case Core.Settings.DiffecultyLevel.Hard:
                    hardCount = Mathf.RoundToInt(count * 0.7f);
                    normalCount = count - hardCount;
                    break;
            }
            List<EnemyData> result = new();
            if (easyCount > 0)
                result.AddRange(EasyEnemyList.GetRandomEnemies(easyCount));
            if (normalCount > 0)
                result.AddRange(NormalEnemyList.GetRandomEnemies(normalCount));
            if (hardCount > 0)
                result.AddRange(HardEnemyList.GetRandomEnemies(hardCount));
            return result;
        }

        private void SpawnNewRoom()
        {
            previousRoom = currentRoom;

            GameObject roomToSpawn;
            roomToSpawn = roomIndex % 6 == 5 ? GetRandomIntersection() : GetRandomRoom();

            spawnPointIndex = spawnPointIndex == 0 ? 1 : 0;
            currentRoom = Instantiate(roomToSpawn, SpawnPoints[spawnPointIndex].position, Quaternion.identity);

            if(currentRoom.TryGetComponent(out RoomControls roomControls))
            {
                roomControls.Initialize(GetNextRoomEnemies());
            }
            roomIndex++;
        }

        private void FadeIn()
        {
            LevelLoadingRelatedEvents.Send_OnScreenFadeInStarted();
        }

        private void FadeOut()
        {
            LevelLoadingRelatedEvents.Send_OnScreenFadeOutStarted();
        }
    }
}