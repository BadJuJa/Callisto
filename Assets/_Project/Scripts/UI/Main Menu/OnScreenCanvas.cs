using UnityEngine;

namespace BadJuja.UI.MainMenu
{
    public class OnScreenCanvas : MonoBehaviour
    {
        [Header("Buttons")]
        public GameObject ReturnToLayoutSelection;

        [Header("Character Info")]
        public GameObject FrostInfo;
        public GameObject FireInfo;
        public GameObject ShockInfo;

        public void EnableShockInfo()
        {
            ShockInfo.SetActive(true);
        }

        public void EnableFireInfo()
        {
            FireInfo.SetActive(true);
        }

        public void EnableFrostInfo()
        {
            FrostInfo.SetActive(true);
        }
        public void Toggle_ReturnToEquipmentSelection()
        {
            ReturnToLayoutSelection.SetActive(!ReturnToLayoutSelection.activeInHierarchy);
        }
    }
}
