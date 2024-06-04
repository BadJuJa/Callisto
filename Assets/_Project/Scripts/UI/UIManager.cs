using BadJuja.Core.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BadJuja.UI
{
    public class UIManager : MonoBehaviour
    {
        public GameObject InGameUI;
        public GameObject PauseMenu;
        public GameObject GameOverScreen;

        public PauseMenu.PlayerStats PauseMenuPlayerStats;

        private void Awake()
        {
            InGameUI.SetActive(true);
            PauseMenu.SetActive(false);
            GameOverScreen.SetActive(false);

            GameRelatedEvents.OnPause += ToglePause;
            PlayerRelatedEvents.OnDeath += OnGameOver;
        }

        private void OnDestroy()
        {
            GameRelatedEvents.OnPause -= ToglePause;
            PlayerRelatedEvents.OnDeath -= OnGameOver;
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

        public void RestartGame()
        {
            //SceneManager.LoadScene
        }

        private void OnGameOver()
        {
            InGameUI.SetActive(false);
            PauseMenu.SetActive(false);
            GameOverScreen.SetActive(true);
        }
    }
}
