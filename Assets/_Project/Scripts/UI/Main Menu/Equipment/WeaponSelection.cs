using UnityEngine;

namespace BadJuja.UI.MainMenu.EquipmentScreen {
    public class WeaponSelection : MonoBehaviour {

        public GameObject MeleeWeaponListScreen;
        public GameObject LightWeaponListScreen;
        public GameObject HeavyWeaponListScreen;

        private UIOutline[] uiOutlines;

        private void Awake()
        {
            uiOutlines = GetComponentsInChildren<UIOutline>();
        }

        public void ShowMeleeScreen()
        {
            MeleeWeaponListScreen.SetActive(true);
            uiOutlines[0].SetSelection(true);

            LightWeaponListScreen.SetActive(false);
            uiOutlines[1].SetSelection(false);

            HeavyWeaponListScreen.SetActive(false);
            uiOutlines[2].SetSelection(false);

        }
        public void ShowLightScreen()
        {
            MeleeWeaponListScreen.SetActive(false);
            uiOutlines[0].SetSelection(false);

            LightWeaponListScreen.SetActive(true);
            uiOutlines[1].SetSelection(true);

            HeavyWeaponListScreen.SetActive(false);
            uiOutlines[2].SetSelection(false);
        }
        public void ShowHeavyScreen()
        {
            MeleeWeaponListScreen.SetActive(false);
            uiOutlines[0].SetSelection(false);

            LightWeaponListScreen.SetActive(false);
            uiOutlines[1].SetSelection(false);

            HeavyWeaponListScreen.SetActive(true);
            uiOutlines[2].SetSelection(true);
        }
    }
}