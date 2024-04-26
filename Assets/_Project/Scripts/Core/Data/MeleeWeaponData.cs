using UnityEngine;

namespace BadJuja.Core.Data
{
    [CreateAssetMenu(fileName = "New Melee Weapon Data", menuName = "Data/Melee Weapon")]
    public class MeleeWeaponData : WeaponData {
        public MeleeWeaponBehaviour Behaviour;
    }
}
