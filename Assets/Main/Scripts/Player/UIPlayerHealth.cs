using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealth : MonoBehaviour
{
    public Slider slider;
    public Slider easeSlider;

    public float lerpSpeed = 0.05f;

    private void OnEnable()
    {
        GlobalEvents.OnPlayerHealthChanged += UpdateSlider;
    }

    private void OnDisable()
    {
        GlobalEvents.OnPlayerHealthChanged -= UpdateSlider;
    }

    private void Update()
    {
        if (slider.value != easeSlider.value)
        {
            easeSlider.value = Mathf.Lerp(easeSlider.value, slider.value, lerpSpeed);
            //easeSlider.value = Mathf.Lerp(slider.value, easeSlider.value, lerpSpeed);
        }
    }

    public void UpdateSlider(float fillValue)
    {
        if (fillValue < 0 )
        {
            fillValue = 0;
        }

        slider.value = fillValue;
    }
}
