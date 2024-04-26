using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja
{
    public class SettingsButtonsHide : MonoBehaviour
    {
        public GameObject ToVideoButton;
        public List<GameObject> ToGraphicsButtons;
        public GameObject ToSoundButton;

        public void HideButtons_ToVideo()
        {
            ToVideoButton.SetActive(false);
            ToGraphicsButtons.ForEach(button => button.SetActive(true));
            ToSoundButton.SetActive(true);
        }
        
        public void HideButtons_ToGraphics()
        {
            ToVideoButton.SetActive(true);
            ToGraphicsButtons.ForEach(button => button.SetActive(false));
            ToSoundButton.SetActive(true);
        }

        public void HideButtons_ToSounds()
        {
            ToVideoButton.SetActive(true);
            ToGraphicsButtons.ForEach(button => button.SetActive(true));
            ToSoundButton.SetActive(false);
        }

        public void RevealAllButtons()
        {
            ToVideoButton.SetActive(true);
            ToGraphicsButtons.ForEach(button => button.SetActive(true));
            ToSoundButton.SetActive(true);
        }
    }
}
