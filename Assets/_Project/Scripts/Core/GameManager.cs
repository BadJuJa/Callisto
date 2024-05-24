using BadJuja.Core.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BadJuja.Core {
    [RequireComponent(typeof(PlayerInput))]
    public class GameManager : MonoBehaviour {

        public static bool GameIsPaused = false;
        public static InputHandler InputHandler { get; private set; }

        public void Initialize()
        {
            InputHandler = new(GetComponent<PlayerInput>());

            GameRelatedEvents.OnPause += OnPauseSwitch;
        }

        private void OnPauseSwitch()
        {
            GameIsPaused = !GameIsPaused;

            InputHandler.SetActive(!GameIsPaused);
        }
    }
}