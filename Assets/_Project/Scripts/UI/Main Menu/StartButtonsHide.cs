using UnityEngine;

namespace BadJuja.UI.MainMenu {
    public class StartButtonsHide : MonoBehaviour
    {
        public GameObject ToEquipmentButton;
        public GameObject ToDifficultyButton;

        public void HideButtons_ToEquipment()
        {
            ToDifficultyButton.SetActive(true);
            ToEquipmentButton.SetActive(false);
        }
        
        public void HideButtons_ToDifficulty()
        {
            ToDifficultyButton.SetActive(false);
            ToEquipmentButton.SetActive(true);
        }

        public void RevealAllButtons()
        {
            ToDifficultyButton.SetActive(true);
            ToEquipmentButton.SetActive(true);
        }

        public void ExitButton()
        {
            Application.Quit();
        }
    }
}
