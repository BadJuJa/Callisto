using BadJuja.Core.Data;
using BadJuja.Core.Data.Lists;
using TMPro;
using UnityEngine;

namespace BadJuja.UI.MainMenu
{
    public class EquipmentLayoutDropdownFillMelee : MonoBehaviour
    {
        private TMP_Dropdown dropdown;

        public PlayerCurrentEquipment playerCurrentEquipment;
        public MeleeWeaponDataList WeaponsList;

        private void OnEnable()
        {
            dropdown.onValueChanged.AddListener(ChangeChoice);
        }

        private void OnDisable()
        {
            dropdown.onValueChanged.RemoveListener(ChangeChoice);
        }

        private void Awake()
        {
            dropdown = GetComponent<TMP_Dropdown>();

            FillDropdownList();
        }

        private void FillDropdownList()
        {
            dropdown.ClearOptions();

            var _ = WeaponsList.WeaponDatas;

            foreach (WeaponData data in _)
            {
                dropdown.options.Add(new(data.name, data.Sprite));
            }

            ChangeChoice(0);
            dropdown.RefreshShownValue();
        }

        private void ChangeChoice(int value)
        {
            playerCurrentEquipment.MeleeWeaponData = WeaponsList.WeaponDatas[value];
        }
    }
}
