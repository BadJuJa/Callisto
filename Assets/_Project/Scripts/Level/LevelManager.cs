using BadJuja.Core.Events;
using System.Collections;
using UnityEngine;

namespace BadJuja.LevelManagement {
    public class LevelManager : MonoBehaviour {
        public GameObject[] Rooms;
        public GameObject[] Intersections;
        public Transform[] SpawnPoints;

        public Transform PlayerTransform;

        private GameObject previousRoom;
        private GameObject currentRoom;

        private int roomIndex = 0;
        private int spawnPointIndex = 0;
        private Vector3 roomPlayerSpawnpoint;

        private void OnEnable()
        {
            PlayerRelatedEvents.OnPlayerExitedRoom += Proceed_p1;
            LevelLoadingRelatedEvents.OnScreenFadeOutFinished += Proceed_p2;
        }

        private void OnDisable()
        {
            PlayerRelatedEvents.OnPlayerExitedRoom -= Proceed_p1;
            LevelLoadingRelatedEvents.OnScreenFadeOutFinished -= Proceed_p2;
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

        private void SpawnNewRoom()
        {
            previousRoom = currentRoom;

            GameObject roomToSpawn;
            roomToSpawn = roomIndex % 6 == 5 ? GetRandomIntersection() : GetRandomRoom();

            spawnPointIndex = spawnPointIndex == 0 ? 1 : 0;
            currentRoom = Instantiate(roomToSpawn, SpawnPoints[spawnPointIndex].position, Quaternion.identity);
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