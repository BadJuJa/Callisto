using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealth : MonoBehaviour
{
    public Slider slider;
    public float AnimationTime;

    private float _currentValue;
    private float _desiredValue;

    private void Start()
    {
        _currentValue = slider.value;

        StartCoroutine(LerpHealth());
    }

    private void OnEnable()
    {
        GlobalEvents.OnPlayerHealthChanged += UpdateSlider;
    }

    private void OnDisable()
    {
        GlobalEvents.OnPlayerHealthChanged -= UpdateSlider;
    }

    public void UpdateSlider(float fillValue)
    {
        _desiredValue = _currentValue - fillValue;
        if ( _desiredValue < 0 )
        {
            _desiredValue = 0;
        }
    }

    private IEnumerator LerpHealth()
    {
        while (true)
        {
            if (_currentValue != _desiredValue)
            {
                _currentValue = Mathf.Lerp(_currentValue, _desiredValue, AnimationTime);
                slider.value = _currentValue;
            }
            yield return null;
        }
    }
}
