using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.Core.Data.Lists {
    [CreateAssetMenu(fileName = "New Melee Weapon Data List", menuName = "Data/Weapon/Melee Weapon List")]
    public class MeleeWeaponDataList : ScriptableObject {
        public List<MeleeWeaponData> WeaponDatas = new();
    }
}
