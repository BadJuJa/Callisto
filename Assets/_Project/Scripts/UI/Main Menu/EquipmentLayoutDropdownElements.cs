using BadJuja.Core;
using BadJuja.Core.Data;
using BadJuja.Core.Data.Lists;
using TMPro;
using UnityEngine;

namespace BadJuja.UI.MainMenu
{
    public class EquipmentLayoutDropdownElements : MonoBehaviour
    {
        private TMP_Dropdown dropdown;

        public PlayerWeaponTypes weaponType;

        public ElementDataList ElementDataList;
        public PlayerCurrentEquipment playerCurrentEquipment;

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

            var _ = ElementDataList.Elements;

            foreach (ElementData data in _)
            {
                dropdown.options.Add(new(data.Sprite));
            }

            ChangeChoice(0);
            dropdown.RefreshShownValue();
        }

        private void ChangeChoice(int value)
        {
            var newChoice = ElementDataList.Elements[value];

            if (weaponType == PlayerWeaponTypes.Melee)
                playerCurrentEquipment.MeleeWeaponElement = newChoice;
            else if (weaponType == PlayerWeaponTypes.Light)
                playerCurrentEquipment.LightRangedWeaponElement = newChoice;
            else
                playerCurrentEquipment.HeavyRangedWeaponElement = newChoice;
        }
    }
}
