using UnityEngine;

namespace BadJuja.Player {
    [CreateAssetMenu(fileName = "SharedPlayerData", menuName = "Data/Shared Player Data", order = 1)]
    public class SharedPlayerData : ScriptableObject {
        public float CurrentAttackTime = 1f;
        public float MeleeDamage = 40;
        public Transform PlayerTransform;
    }
}