using BadJuja.Core;
using BadJuja.Core.Data;
using BadJuja.Core.Data.Lists;
using TMPro;
using UnityEngine;

namespace BadJuja.UI.MainMenu
{
    public class CharacterInfo : MonoBehaviour
    {
        public PlayerCurrentEquipment PlayerCurrentEquipment;
        public CharacterModifierPreset Preset;
        public GameObject CharacterModel;

        [Space]

        public GameObject Button_Confirm;
        public GameObject Button_Return;
        public TextMeshProUGUI Text;

        private GameObject _characterInfo;

        private void Awake()
        {
            _characterInfo = gameObject;

            if (Text == null) return;

            Text.SetText(GetPresetText());
            
        }

        private string GetPresetText()
        {
            string result = "";

            var mods = Preset.CharacterModifiers;

            for (int i = 0; i < mods.Count; i++)
            {
                result += ModToString(mods[i].Stat, mods[i].Strenght);
            }

            return result;
        }

        private string ModToString(AllCharacterStats stat, float strenght)
        {
            string result = "\n";

            result += stat.ToString() + " ";
            result += strenght < 0 ? strenght.ToString() : "+" + strenght.ToString();
            result += (stat == AllCharacterStats.Health || stat == AllCharacterStats.Armor) ? "" : "%";
            result += "\n";
            return result;
        }

        public void Confirm()
        {
            PlayerCurrentEquipment.CharacterBonusModifiers = Preset;
            PlayerCurrentEquipment.Model = CharacterModel;
            _characterInfo.SetActive(false);
        }

        public void Return()
        {
            _characterInfo.SetActive(false);
        }
    }
}
