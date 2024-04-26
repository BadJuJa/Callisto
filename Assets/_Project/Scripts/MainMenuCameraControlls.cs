using UnityEngine;

namespace BadJuja
{
    public class MainMenuCameraControlls : MonoBehaviour
    {
        private Animator animator;
        private int cameraStateParameterId;
        private int startStateParameterId;
        private int settingsStateParameterId;

        private int currentCameraState;
        private int currentStartState;
        private int currentSettingsState;


        private void Awake()
        {
            animator = GetComponent<Animator>();

            cameraStateParameterId = Animator.StringToHash("CameraState");
            startStateParameterId = Animator.StringToHash("StartState");
            settingsStateParameterId = Animator.StringToHash("SettingsState");

            ResetStates();
        }

        public void ChangeCurrentCameraState(int value)
        {
            currentCameraState = value;
            animator.SetInteger(cameraStateParameterId, currentCameraState);
        }

        public void ChangeCurrentStartState(int value)
        {
            currentStartState = value;
            animator.SetInteger(startStateParameterId, currentStartState);
        }

        public void ChangeCurrentSettingsState(int value)
        {
            currentSettingsState = value;
            animator.SetInteger(settingsStateParameterId, currentSettingsState);
        }

        public void OnReturnClicked()
        {
            ResetStates();
        }

        private void ResetStates()
        {
            ChangeCurrentCameraState(0);
            ChangeCurrentStartState(0);
            ChangeCurrentSettingsState(0);
        }
    }
}
