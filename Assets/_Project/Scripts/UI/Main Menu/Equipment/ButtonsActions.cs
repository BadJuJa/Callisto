using BadJuja.Core.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BadJuja.UI.MainMenu.EquipmentScreen {
    public class ButtonsActions : MonoBehaviour {

        [Header("Referenced screens")]
        public GameObject DifficultyScreen;
        public GameObject CharacterScreen;
        public GameObject WeaponScreen;

        [Space]
        public PlayerCurrentEquipment PlayerCurrentEquipment;
        public Button StartButton;

        [Space]
        public int CoreSceneBuildIndex = 1;
        public void ToCharacter()
        {
            WeaponScreen.SetActive(false);
            CharacterScreen.SetActive(true);
            CheckEquipment();
        }

        public void ToWeapons()
        {
            CharacterScreen.SetActive(false);
            WeaponScreen.SetActive(true);
            CheckEquipment();
        }

        public void ToDifficulty()
        {
            ToCharacter();
            gameObject.SetActive(false);
            DifficultyScreen.SetActive(true);
        }

        public void CheckEquipment()
        {
            if (PlayerCurrentEquipment.Model == null
                || PlayerCurrentEquipment.MeleeWeaponData == null
                || PlayerCurrentEquipment.LightRangedWeaponData == null
                || PlayerCurrentEquipment.HeavyRangedWeaponData == null) StartButton.interactable = false;
            else
                StartButton.interactable = true;
        }

        public void StartGame(int sceneIndex = -1)
        {
            if (sceneIndex < 0)
            {
                sceneIndex = CoreSceneBuildIndex;
            }
            SceneManager.LoadScene(sceneIndex);
        }
    }
}