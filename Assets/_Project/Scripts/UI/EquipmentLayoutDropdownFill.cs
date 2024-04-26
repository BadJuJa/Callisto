using BadJuja.Core.Data;
using TMPro;
using UnityEngine;

namespace BadJuja.UI
{
    public class EquipmentLayoutDropdownFill : MonoBehaviour
    {
        private TMP_Dropdown dropdown;

        public dynamic WeaponsList;

        private void OnEnable()
        {
            dropdown.onValueChanged.AddListener(PrintChoice);
        }

        private void OnDisable()
        {
            dropdown.onValueChanged.RemoveListener(PrintChoice);
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

            dropdown.RefreshShownValue();
        }

        private void PrintChoice(int value)
        {
            print(WeaponsList.WeaponDatas[value].name);
        }
    }
}
