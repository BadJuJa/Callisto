using UnityEngine;

namespace BadJuja.Core.Data
{
    [CreateAssetMenu(fileName = "New Ranged Weapon Data", menuName = "Data/Weapon/Ranged Weapon")]
    public class RangedWeaponData : WeaponData {
        public RangedWeaponBehaviour Behaviour;
        public float FireRate;
    }
}
