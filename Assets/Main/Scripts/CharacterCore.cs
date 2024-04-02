using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCore : MonoBehaviour, IDamagable
{
    private float _health;
    [SerializeField] private float _maxHealth = 100;

    private void Awake()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(float value)
    {
        if (_health > value) _health -= value;
        else Die();
    }

    private void Die()
    {
        print($"You died, {name}!");
        Destroy(gameObject);
    }
}
