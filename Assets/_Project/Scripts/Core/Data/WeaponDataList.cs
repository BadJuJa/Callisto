using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.Core.Data
{
    [CreateAssetMenu(fileName = "New Ranged Weapon Data List", menuName = "Data/Ranged Weapon Data List")]
    public class RangedWeaponDataList : ScriptableObject
    {
        public List<RangedWeaponData> WeaponDatas = new();
    }

    [CreateAssetMenu(fileName = "New Melee Weapon Data List", menuName = "Data/Melee Weapon Data List")]
    public class MeleeWeaponDataList : ScriptableObject {
        public List<MeleeWeaponData> WeaponDatas = new();
    }
}
