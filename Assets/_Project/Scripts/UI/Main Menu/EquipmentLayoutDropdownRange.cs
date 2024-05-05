using BadJuja.Core.Data;
using BadJuja.Core.Data.Lists;
using TMPro;
using UnityEngine;

namespace BadJuja.UI.MainMenu {
    public class EquipmentLayoutDropdownRange : MonoBehaviour
    {
        private TMP_Dropdown dropdown;

        public enum RangedType {
            Light,
            Heavy
        }

        public RangedType rangedType;

        public PlayerCurrentEquipment playerCurrentEquipment;
        public RangedWeaponDataList WeaponsList;

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
            var newChoice = WeaponsList.WeaponDatas[value];

            if (rangedType == RangedType.Light)
                playerCurrentEquipment.LightRangedWeaponData = newChoice;
            else
                playerCurrentEquipment.HeavyRangedWeaponData = newChoice;
        }
    }
}
