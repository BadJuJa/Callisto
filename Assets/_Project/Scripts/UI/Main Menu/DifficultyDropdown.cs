using BadJuja.Core.Settings;
using System;
using TMPro;
using UnityEngine;

namespace BadJuja.UI.MainMenu
{
    public class DifficultyDropdown : MonoBehaviour
    {
        TMP_Dropdown dropdown;

        public Settings Settings;

        private void OnEnable()
        {
            dropdown.onValueChanged.AddListener(ChangeChoice);
        }

        private void OnDisable()
        {
            dropdown.onValueChanged.RemoveListener(ChangeChoice);
        }

        void Awake()
        {
            dropdown = GetComponent<TMP_Dropdown>();

            FillDropdownList();
        }

        private void FillDropdownList()
        {
            dropdown.ClearOptions();

            var a = Enum.GetValues(typeof(DiffecultyLevel));
            foreach(var b in a)
                dropdown.options.Add(new(b.ToString()));

            ChangeChoice(0);
            dropdown.RefreshShownValue();
        }

        private void ChangeChoice(int value)
        {
            if (Settings == null) return;

            Settings.DiffecultyLevel = (DiffecultyLevel)value;
        }
    }
}
