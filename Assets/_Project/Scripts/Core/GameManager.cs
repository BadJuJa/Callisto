using BadJuja.Core.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BadJuja.Core {
    [RequireComponent(typeof(PlayerInput))]
    public class GameManager : MonoBehaviour {

        public static bool GameIsPaused = false;
        public static bool PlayerDied = false;

        public static InputHandler InputHandler { get; private set; }

        public void Initialize()
        {
            Time.timeScale = 1f;

            InputHandler = new(GetComponent<PlayerInput>());

            GameIsPaused = false;
            PlayerDied = false;

            GameRelatedEvents.OnPause += OnPauseSwitch;
            PlayerRelatedEvents.OnDeath += OnPlayerDied;
        }

        private void OnDestroy()
        {
            InputHandler.SetActive(false);
            InputHandler.DisablePauseAction();
        }

        private void OnPauseSwitch()
        {
            GameIsPaused = !GameIsPaused;

            InputHandler.SetActive(!GameIsPaused);
        }

        private void OnPlayerDied()
        {
            PlayerDied = true;
            InputHandler.DisablePauseAction();
            OnPauseSwitch();
            Time.timeScale = 0;
        }
    }
}