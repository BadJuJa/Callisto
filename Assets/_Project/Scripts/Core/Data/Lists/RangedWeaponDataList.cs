using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.Core.Data.Lists
{
    [CreateAssetMenu(fileName = "New Ranged Weapon Data List", menuName = "Data/Weapon/Ranged Weapon List")]
    public class RangedWeaponDataList : ScriptableObject
    {
        public List<RangedWeaponData> WeaponDatas = new();
    }
}
