using BadJuja.Core.Data.Lists;
using BadJuja.Core.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using BadJuja.Core;

namespace BadJuja.UI.MainMenu.EquipmentScreen {
    public class CharacterSelection : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

        public TextMeshProUGUI Text;

        public PlayerCurrentEquipment PlayerCurrentEquipment;
        public CharacterModifierPreset Preset;
        public GameObject CharacterModel;

        private string _presetText;
        private string _oldText;

        private SelectionOutlineHandler _outlineHandler;
        private UIOutline _outline;
        private void Awake()
        {
            _presetText = GetPresetText();
            _outlineHandler = GetComponentInParent<SelectionOutlineHandler>();
            _outline = GetComponent<UIOutline>();
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

        public void OnPointerClick(PointerEventData eventData)
        {
            PlayerCurrentEquipment.CharacterBonusModifiers = Preset;
            PlayerCurrentEquipment.Model = CharacterModel;
            _oldText = _presetText;
            _outlineHandler.ChangeSelectedItem(_outline);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_outline.IsSelected)
                Text.SetText(_oldText);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _oldText = Text.text;

            Text.SetText(_presetText);
        }
    }
}