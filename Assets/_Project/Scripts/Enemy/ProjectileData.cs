using UnityEngine;

namespace BadJuja.Enemy {
    [CreateAssetMenu(fileName = "Enemy Projectile Data", menuName = "Data/Enemy Projectile")]
    public class ProjectileData : ScriptableObject {
        [Header("Model")]
        public GameObject ProjectilePrefab;

        [Header("Damage Data")]
        public float Speed = 25;
        public int MaxPiercing = 1;
    }
}