using BadJuja.Core.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BadJuja.UI
{
    public class UIManager : MonoBehaviour
    {
        public GameObject InGameUI;
        public GameObject PauseMenu;

        public PauseMenu.PlayerStats PauseMenuPlayerStats;

        private void Awake()
        {
            GameRelatedEvents.OnPause += ToglePause;
        }

        private void OnDestroy()
        {
            GameRelatedEvents.OnPause -= ToglePause;
        }
        private void ToglePause()
        {
            InGameUI.SetActive(!InGameUI.activeSelf);
            PauseMenu.SetActive(!PauseMenu.activeSelf);
        }

        private void Start()
        {
            PauseMenuPlayerStats.Initialize();
        }

        public void ToMainMenu(int sceneBuildIndex)
        {
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }
}
