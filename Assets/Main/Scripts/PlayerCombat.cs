using System.Collections.Generic;
using UnityEngine;

public enum PlayerAttackTypes {
    Melee,
    Light,
    Heavy,
}

public class PlayerCombat : MonoBehaviour {
    public Transform FiringPoint;
    public List<ProjectileData> AttackProjectiles = new();

    private Dictionary<PlayerAttackTypes, ProjectileData> _attackProjectiles = new();
    private Dictionary<PlayerAttackTypes, float> _attackTimes = new();

    public SharedPlayerData PlayerData;

    public PlayerAttackTypes AttackType = PlayerAttackTypes.Light;

    private void OnEnable()
    {
        GlobalEvents.Event_OnSwitchToAttackType += SwitchAttackType;
        GlobalEvents.Event_OnPlayerAttacked += Attack;
    }

    private void OnDisable()
    {
        GlobalEvents.Event_OnSwitchToAttackType -= SwitchAttackType;
        GlobalEvents.Event_OnPlayerAttacked -= Attack;
    }

    private void Awake()
    {
        foreach (var projectileData in AttackProjectiles)
        {
            _attackProjectiles.Add(projectileData.AttackType, projectileData);
            _attackTimes.Add(projectileData.AttackType, projectileData.AttackTime);
        }
    }

    private void Start()
    {
        SwitchAttackType(2);
    }

    private void Attack()
    {
        if (!_attackProjectiles.ContainsKey(AttackType))
        {
            Debug.LogError("No projectile assigned for the current attack method.");
            return;
        }

        var projectileData = _attackProjectiles[AttackType];
        GameObject projectile = Instantiate(projectileData.ProjectilePrefab, FiringPoint.position, Quaternion.LookRotation(FiringPoint.forward));
        OffensiveProjectile offensiveProjectile = projectile.GetComponent<OffensiveProjectile>();

        if (offensiveProjectile != null)
        {
            offensiveProjectile.Initialize(projectileData);
        }
        else
        {
            Debug.LogError("Projectile prefab doesn't have OffensiveProjectile component attached.");
        }
    }

    private void SwitchAttackType(int value)
    {
        if (value < 0 || value >= System.Enum.GetValues(typeof(PlayerAttackTypes)).Length)
        {
            Debug.LogError("Invalid attack method value.");
            return;
        }

        AttackType = (PlayerAttackTypes)value;

        PlayerData.CurrentAttackTime = _attackTimes[AttackType];

        GlobalEvents.Send_OnPlayerChangedAttackType(AttackType);
    }
}
