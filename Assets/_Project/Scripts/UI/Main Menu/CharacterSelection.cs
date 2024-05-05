using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace BadJuja.UI.MainMenu {
    public class CharacterSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {
        public UnityEvent OnClick;
        
        private Outline _outline;

        private bool _enabled;

        private void Start()
        {
            _outline = GetComponent<Outline>();
            _enabled = true;
            _outline.enabled = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_enabled) return;

            _outline.enabled = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_enabled) return;

            _outline.enabled = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_enabled) return;

            OnClick?.Invoke();
            _outline.enabled = false;
            _enabled = false;
        }

        public void EnableComponent()
        {
            _enabled = true;
        }
    }
}
