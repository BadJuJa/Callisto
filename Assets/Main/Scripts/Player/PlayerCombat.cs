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

    private CharacterCore characterCore;
    private StatUpgrade selfUpgradeMod;

    private void OnEnable()
    {
        GlobalEvents.OnSwitchToAttackType += SwitchAttackType;
        GlobalEvents.OnPlayerAttacked += Attack;
    }

    private void OnDisable()
    {
        GlobalEvents.OnSwitchToAttackType -= SwitchAttackType;
        GlobalEvents.OnPlayerAttacked -= Attack;
    }

    private void Awake()
    {
        characterCore = GetComponentInParent<CharacterCore>();
        selfUpgradeMod = GetComponent<StatUpgrade>();

        foreach (var projectileData in AttackProjectiles)
        {
            _attackProjectiles.Add(projectileData.AttackType, projectileData);
            _attackTimes.Add(projectileData.AttackType, projectileData.AttackTime);
        }
    }

    private void Start()
    {
        SwitchAttackType(1);
    }

    private void Attack()
    {
        switch (AttackType)
        {
            case PlayerAttackTypes.Melee:
                LauchMeleeAttack();
                break;
            case PlayerAttackTypes.Light:
            case PlayerAttackTypes.Heavy:
                LauchRangeAttack();
                break;
        }
    }

    private void SwitchAttackType(int value)
    {
        if (value < 0 || value >= System.Enum.GetValues(typeof(PlayerAttackTypes)).Length)
        {
            Debug.LogError("Invalid attack method value.");
            return;
        }
        
        selfUpgradeMod.Remove(characterCore);

        AttackType = (PlayerAttackTypes)value;

        PlayerData.CurrentAttackTime = _attackTimes[AttackType];
        selfUpgradeMod.Strenght = _attackProjectiles[AttackType].Damage;
        selfUpgradeMod.Apply(characterCore);
        
        GlobalEvents.Send_OnPlayerChangedAttackType(AttackType);
    }

    private void LauchRangeAttack()
    {
        if (!_attackProjectiles.ContainsKey(AttackType))
        {
            Debug.LogError("No projectile assigned for the current attack method.");
            return;
        }

        var projectileData = _attackProjectiles[AttackType];
        GameObject projectile = Instantiate(
            projectileData.ProjectilePrefab, 
            FiringPoint.position, 
            Quaternion.LookRotation(FiringPoint.forward), 
            transform);
        OffensiveProjectile offensiveProjectile = projectile.GetComponent<OffensiveProjectile>();

        if (offensiveProjectile != null)
        {
            offensiveProjectile.Initialize(projectileData, characterCore.Damage.Value);
        }
        else
        {
            Debug.LogError("Projectile prefab doesn't have OffensiveProjectile component attached.");
        }
    }

    private void LauchMeleeAttack()
    {

    }
}
