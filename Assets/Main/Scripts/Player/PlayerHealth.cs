using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    private float _maxHealth = 100;
    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float value)
    {
        if (value < _currentHealth)
        {
            _currentHealth -= value;
        } else
        {
            _currentHealth = 0;
            Die();
        }

        GlobalEvents.Send_OnPlayerHealthChanged(GetHealthPercentage());
    }

    private void Die()
    {
        print($"You died, {name}!");
        Destroy(gameObject);
    }

    private float GetHealthPercentage()
    {
        print(_currentHealth / _maxHealth);
        return _currentHealth / _maxHealth;
    }

    
}
