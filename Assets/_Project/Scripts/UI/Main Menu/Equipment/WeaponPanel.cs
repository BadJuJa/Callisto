using BadJuja.Core;
using BadJuja.Core.Data;
using BadJuja.Core.Data.Lists;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.UI.MainMenu.EquipmentScreen {
    public class WeaponPanel : MonoBehaviour {

        public GameObject ItemTemplate;

        public RangedWeaponDataList LightWeaponDataList;
        public RangedWeaponDataList HeavyWeaponDataList;
        public MeleeWeaponDataList MeleeWeaponDataList;

        private void Awake()
        {
            if (MeleeWeaponDataList != null)
                FillList(MeleeWeaponDataList.WeaponDatas, PlayerWeaponTypes.Melee);
            else if (LightWeaponDataList != null)
                FillList(LightWeaponDataList.WeaponDatas, PlayerWeaponTypes.Light);
            else if (HeavyWeaponDataList != null)
                FillList(HeavyWeaponDataList.WeaponDatas, PlayerWeaponTypes.Heavy);
        }
            
        private void FillList<T>(List<T> inputList, PlayerWeaponTypes weaponType)
        {
            foreach (var data in inputList)
            {
                GameObject item = Instantiate(ItemTemplate, transform);
                item.GetComponent<WeaponPanelItem>().Init(data as WeaponData, weaponType);
                item.SetActive(true);
            }
        }
    }
}