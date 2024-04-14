using UnityEngine;

namespace BadJuja.LevelManagement {
    public class IntersectionControls : MonoBehaviour, IGeneralRoomControl {
        public Transform playerSpawnPoint;

        public Transform UpgradeSpawnPosition;
        public GameObject UpgradePrefab;

        public Transform PlayerSpawnPoint => playerSpawnPoint;
    }
}