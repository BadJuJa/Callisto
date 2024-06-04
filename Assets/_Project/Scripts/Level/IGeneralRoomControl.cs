using BadJuja.Core.Data;
using UnityEngine;

namespace BadJuja.LevelManagement {
    public interface IGeneralRoomControl {
        public Transform PlayerSpawnPoint { get; }
        public void Initialize(EnemyData[] enemyData = null) { }
    }
}