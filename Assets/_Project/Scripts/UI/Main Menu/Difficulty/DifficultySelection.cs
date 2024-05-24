using UnityEngine;
using UnityEngine.EventSystems;

namespace BadJuja.UI.MainMenu.DifficultyScreen {
    public class DifficultySelection : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

        public ButtonsActions Buttons;

        public Core.Settings.DifficultyLevel difficulty;
        public Core.Settings.Settings Settings;

        private SelectionOutlineHandler _outlineHandler;
        private UIOutline _outline;

        private void Awake()
        {
            _outlineHandler = GetComponentInParent<SelectionOutlineHandler>();
            _outline = GetComponent<UIOutline>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Settings.DiffecultyLevel = difficulty;
            Buttons.ToEquipment();

            _outlineHandler.ChangeSelectedItem(_outline);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {

        }

        public void OnPointerExit(PointerEventData eventData)
        {

        }
    }
}