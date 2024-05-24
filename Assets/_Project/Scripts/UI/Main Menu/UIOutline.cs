using UnityEngine;
using UnityEngine.EventSystems;

namespace BadJuja.UI {
    [RequireComponent(typeof(UnityEngine.UI.Outline))]
    public class UIOutline : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

        public UnityEngine.UI.Outline Outline;
        
        [HideInInspector] public bool IsSelected = false;

        private void Awake()
        {
            Outline = GetComponent<UnityEngine.UI.Outline>();
            Outline.effectDistance = new(10, 10);
            Outline.enabled = false;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            Outline.enabled = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!IsSelected)
                Outline.enabled = false;
        }

        public void SetSelection(bool value)
        {
            IsSelected = value;
            Outline.enabled = value;
        }
    }
}