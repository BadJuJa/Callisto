using BadJuja.Core.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BadJuja.Enemy {
    [RequireComponent(typeof(Outline))]
    public class EnemySelectionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {
        
        private Outline _outline;
        public bool IsSelected = false;

        private void Awake()
        {
            _outline = GetComponent<Outline>();
            EnemyRelatedEvents.PriorityTargetChanged += CheckSelection;
            _outline.enabled = false;
        }

        private void OnDestroy()
        {
            EnemyRelatedEvents.PriorityTargetChanged -= CheckSelection;
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (!IsSelected)
                _outline.enabled = true;
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            if (!IsSelected)
                _outline.enabled = false;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            EnemyRelatedEvents.Send_PriorityTargetChanged(gameObject.transform);
        }

        private void SetSelection(bool value)
        {
            IsSelected = value;
            _outline.enabled = value;
        }

        private void CheckSelection(Transform newTarget)
        {
            if (newTarget != gameObject.transform)
                SetSelection(false);
            else
                SetSelection(true);
        }
    }
}