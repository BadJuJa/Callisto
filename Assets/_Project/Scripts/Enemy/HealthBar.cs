using UnityEngine;
using UnityEngine.UI;

namespace BadJuja.Enemy {
    public class HealthBar : MonoBehaviour {
        public Transform Canvas;
        public Slider slider;

        private Transform _cam;

        private void Awake()
        {
            _cam = Camera.main.transform;
        }

        private void LateUpdate()
        {
            Canvas.rotation = Quaternion.LookRotation(Canvas.position - _cam.position);
        }

        public void UpdateHealth(float newValue)
        {
            slider.value = newValue;
        }
    }
}