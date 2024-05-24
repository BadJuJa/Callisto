using UnityEngine;

namespace BadJuja.UI.MainMenu.MainScreen {
    public class ButtonsActions : MonoBehaviour {
        
        [Header("Referenced screens")]
        public GameObject DifficultyScreen;
        public GameObject SettingsScreen;

        public void ToDifficulty()
        {
            gameObject.SetActive(false);
            DifficultyScreen.SetActive(true);
        }

        public void ToSettings()
        {
            gameObject.SetActive(false);
            SettingsScreen.SetActive(true);
        }

        public void ExitButton()
        {
            Application.Quit();
        }
    }
}