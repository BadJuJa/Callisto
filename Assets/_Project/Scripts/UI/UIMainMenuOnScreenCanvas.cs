using System.Collections;
using UnityEngine;

namespace BadJuja.UI
{
    public class UIMainMenuOnScreenCanvas : MonoBehaviour
    {
        [Header("Buttons")]
        public GameObject ReturnToLayoutSelection;
        public GameObject ReturnToCharacterSelection_Shock_Fire;
        public GameObject ReturnToCharacterSelection_Frost;

        public void ToggleReturnToLayoutSelectionButton()
        {
            ReturnToLayoutSelection.SetActive(!ReturnToLayoutSelection.activeInHierarchy);
        }

        public void ToggleReturnToCharacterSelectionButton_Frost()
        {
            ReturnToCharacterSelection_Frost.SetActive(!ReturnToCharacterSelection_Frost.activeInHierarchy);
        }

        public void ToggleReturnToCharacterSelectionButton_Shock_Fire()
        {
            ReturnToCharacterSelection_Shock_Fire.SetActive(!ReturnToCharacterSelection_Shock_Fire.activeInHierarchy);
        }
    }
}
