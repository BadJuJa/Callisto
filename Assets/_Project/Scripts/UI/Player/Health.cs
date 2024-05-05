using BadJuja.Core.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BadJuja.UI.Player {
    public class Health : MonoBehaviour {
        public Slider slider;
        public Slider easeSlider;
        public TextMeshProUGUI text;

        public float lerpSpeed = 0.05f;

        private float fillValue = 0;

        private void Awake()
        {
            PlayerRelatedEvents.OnHealthChange += UpdateSlider;
        }

        private void OnDestroy()
        {
            PlayerRelatedEvents.OnHealthChange -= UpdateSlider;
        }

        private void Update()
        {
            if (slider.value != easeSlider.value)
            {
                easeSlider.value = Mathf.Lerp(easeSlider.value, slider.value, lerpSpeed);
            }
        }

        public void UpdateSlider(float currenValue, float maxValue)
        {
            fillValue = currenValue / maxValue;

            if (fillValue < 0)
            {
                fillValue = 0;
            }

            slider.value = fillValue;

            text.SetText($"{currenValue}/{maxValue}");
        }
    }
}