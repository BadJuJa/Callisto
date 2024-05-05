using BadJuja.Core.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BadJuja.UI.Player {
    public class Experience : MonoBehaviour {
        public Slider easeSlider;
        public TextMeshProUGUI levelText;

        public float lerpSpeed = 0.05f;

        private float fillValue = 0f;

        private void Awake()
        {
            PlayerRelatedEvents.OnExperienceChange += UpdateSlider;
            PlayerRelatedEvents.OnLevelIncrease += UpdateLevel;
        }

        private void OnDestroy()
        {
            PlayerRelatedEvents.OnExperienceChange -= UpdateSlider;
            PlayerRelatedEvents.OnLevelIncrease -= UpdateLevel;
        }

        private void Update()
        {
            if (easeSlider.value != fillValue)
            {
                easeSlider.value = Mathf.Lerp(easeSlider.value, fillValue, lerpSpeed);
            }
        }

        public void UpdateSlider(float currenValue, float maxValue)
        {
            fillValue = currenValue / maxValue;

            if (currenValue > maxValue)
            {
                fillValue = maxValue;
            }
        }

        private void UpdateLevel()
        {
            int.TryParse(levelText.GetParsedText(), out int level);
            levelText.SetText($"{level + 1}");
        }
    }
}