using UnityEngine;

namespace BadJuja.Core.Data {
    [CreateAssetMenu(fileName = "Enemy Data", menuName = "Data/Enemy/Data")]
    public class EnemyData : ScriptableObject {

        [Header("Prefab Settings")]
        public GameObject Prefab;
        public GameObject AttackProjectilePrefab;

        [Header("Combat Settings")]
        public float AttackRange;
        public float Damage;
        public float AttackCooldown;

        [Header("Other Settings")]
        public EnemyTypes EnemyType;
        public float ExperienceReward;

        [Header("Stats")]
        public ElementData Element;
        public Lists.EnemyStatsOverridePreset StatsOverride;
    }
}