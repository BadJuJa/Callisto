using BadJuja.CharacterStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AllCharacterStats {
    Health,
    DamageReduction,
    Damage,
}

public class CharacterCore : MonoBehaviour, IDamagable 
{
    public float PlayerStatCheckDelay = 1f;
    public Transform UpgradesParent;


    public CharacterStat Health = new();
    private float _currentHealth = 0;
    private float _lastMaxHealth;

    public CharacterStat DamageReduction = new();
    public CharacterStat Damage = new();

    public Dictionary<AllCharacterStats, CharacterStat> Stats = new();

    private int _level = 1;

    private float baseXpForLevel = 100f;
    private float xpForLevelMult = 1.25f;
    private float _xpToNextLevel {
        get {
            return _level * baseXpForLevel * xpForLevelMult;
        }
    }
    private float _currentExperience = 0;

    private void Awake()
    {
        Stats.Add(AllCharacterStats.Health, Health);
        Stats.Add(AllCharacterStats.DamageReduction, DamageReduction);
        Stats.Add(AllCharacterStats.Damage, Damage);

        _currentHealth = Health.Value;
        _lastMaxHealth = Health.Value;
    }

    private void OnEnable()
    {
        StartCoroutine(PlayerStatsCheck());
        GlobalEvents.OnEnemyDied += AddExperience;
    }

    private void OnDisable()
    {
        GlobalEvents.OnEnemyDied -= AddExperience;
    }

    public void TakeDamage(float value)
    {
        value *= 1 - DamageReduction.Value / 100;
        if (value < Health.Value)
        {
            _currentHealth -= value;
        }
        else
        {
            _currentHealth = 0;
        }

        if (_currentHealth == 0)
        {
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
        return _currentHealth / Health.Value;
    }

    private IEnumerator PlayerStatsCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(PlayerStatCheckDelay);
            
            if (Health.Value != _lastMaxHealth)
            {
                float difference = Health.Value - _lastMaxHealth;
                _lastMaxHealth += difference;
                print(difference);
                if (_currentHealth + difference <= 0)
                {
                    _currentHealth = 1f;
                }
                else
                {
                    _currentHealth += difference;
                }
                GlobalEvents.Send_OnPlayerHealthChanged(GetHealthPercentage());
            }
        }
    }

    private void AddExperience(float xpValue)
    {
        _currentExperience += xpValue;
        if (_currentExperience >= _xpToNextLevel)
        {
            _currentExperience -= _xpToNextLevel;
            _level++;
            print(_level);
            GlobalEvents.Send_OnPlayerLevelIncreased(true);
        }

    }
}
