using BadJuja.Core;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerExperience : MonoBehaviour
{
    public Slider easeSlider;
    public TextMeshProUGUI levelText;

    public float lerpSpeed = 0.05f;

    private float fillValue = 0f;

    private void OnEnable()
    {
        GlobalEvents.OnPlayerExperienceChanged += UpdateSlider;
        GlobalEvents.OnPlayerLevelIncreased += UpdateLevel;
    }

    private void OnDisable()
    {
        GlobalEvents.OnPlayerExperienceChanged -= UpdateSlider;
        GlobalEvents.OnPlayerLevelIncreased -= UpdateLevel;
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
