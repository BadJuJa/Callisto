using UnityEngine;
using UnityEngine.SceneManagement;

namespace BadJuja
{
    public class UIMainMenuTempStartButton : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadSceneAsync(1);
        }
    }
}
