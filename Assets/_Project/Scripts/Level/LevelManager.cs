using BadJuja.Core;
using BadJuja.Core.Events;
using System.Collections;
using UnityEngine;

namespace BadJuja.LevelManagement {
    public class LevelManager : MonoBehaviour {

        public DifficultyEscalation DifficultyEscalation;

        [Space]
        public GameObject[] Rooms;
        public GameObject[] Intersections;
        
        [Space, Tooltip("Spawn Intersection every N rooms")]
        public int IntersectionsFrequency = 5;

        [Space]
        public GameObject CurrentRoom;

        private Transform _playerTransform;
        private int roomIndex = 0;

        private void OnDestroy()
        {
            PlayerRelatedEvents.OnPlayerExitedRoom -= ProceedNext_pt1;
            LevelLoadingRelatedEvents.OnScreenFadeOutFinished -= ProceedNext_pt2;
        }

        public void Initialize(Transform playerTransform)
        {
            _playerTransform = playerTransform;
            PlayerRelatedEvents.OnPlayerExitedRoom += ProceedNext_pt1;
            LevelLoadingRelatedEvents.OnScreenFadeOutFinished += ProceedNext_pt2;
        }
        private void ProceedNext_pt1()
        {
            if (_playerTransform == null) return;

            _playerTransform.gameObject.SetActive(false);

            // Затенить экран
            LevelLoadingRelatedEvents.Send_OnScreenFadeOutStarted();
        }

        private void ProceedNext_pt2()
        {
            Destroy(CurrentRoom);

            SpawnRoom();

            if (CurrentRoom.TryGetComponent(out IGeneralRoomControl controls))
            {
                _playerTransform.position = controls.PlayerSpawnPoint.position;
            }

            IEnumerator _()
            {
                yield return new WaitForSeconds(.5f);
                LevelLoadingRelatedEvents.Send_OnScreenFadeInStarted();
                _playerTransform.gameObject.SetActive(true);
            }

            StartCoroutine(_());

            PlayerRelatedEvents.Send_EnteredLevel();
        }

        private GameObject GetRandomRoom(GameObject[] roomList)
        {
            return roomList[Random.Range(0, roomList.Length)];
        }

        private void SpawnRoom()
        {
            GameObject roomToSpawn;
            roomToSpawn = roomIndex % 6 == 5 ? GetRandomRoom(Intersections) : GetRandomRoom(Rooms);

            CurrentRoom = Instantiate(roomToSpawn, Vector3.zero, Quaternion.identity);

            if(CurrentRoom.TryGetComponent(out RoomControls roomControls))
            {
                roomControls.Initialize(DifficultyEscalation.GetNextRoomEnemies(roomIndex));
            }
            roomIndex++;
        }
    }
}