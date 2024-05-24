using UnityEngine;

namespace BadJuja.UI.MainMenu {
    public class SelectionOutlineHandler : MonoBehaviour {
        
        private UIOutline _outline;

        public void ChangeSelectedItem(UIOutline outline)
        {
            if (_outline != null)
            {
                _outline.SetSelection(false);
            }

            _outline = outline;

            _outline.SetSelection(true);
        }
    }
}