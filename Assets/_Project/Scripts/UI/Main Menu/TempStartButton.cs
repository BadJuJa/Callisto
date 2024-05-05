using UnityEngine;
using UnityEngine.SceneManagement;

namespace BadJuja.UI.MainMenu
{
    public class TempStartButton : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadSceneAsync(1);
        }
    }
}
