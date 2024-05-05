using UnityEngine;

namespace BadJuja.LevelManagement {
    public class IntersectionControls : MonoBehaviour, IGeneralRoomControl {
        public Transform playerSpawnPoint;

        public Transform PlayerSpawnPoint => playerSpawnPoint;
    }
}