using UnityEngine;

namespace BadJuja.UI.MainMenu.DifficultyScreen {
    public class ButtonsActions : MonoBehaviour {

        [Header("Referenced screens")]
        public GameObject EquipmentScreen;
        public GameObject MainScreen;

        public void ToEquipment()
        {
            gameObject.SetActive(false);
            EquipmentScreen.SetActive(true);
        }

        public void ToMainMenu()
        {
            gameObject.SetActive(false);
            MainScreen.SetActive(true);
        }
    }
}