using BadJuja.Core;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.Player {
    public class PlayerCombat : MonoBehaviour {
        public Transform FiringPoint;
        public List<ProjectileData> AttackProjectiles = new();

        private Dictionary<PlayerAttackTypes, ProjectileData> _attackProjectiles = new();
        private Dictionary<PlayerAttackTypes, float> _attackTimes = new();

        public SharedPlayerData PlayerData;

        public PlayerAttackTypes AttackType = PlayerAttackTypes.Light;

        private Player _player;
        private SelfUpgrade selfUpgradeMod;

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
            _player = GetComponentInParent<Player>();
            selfUpgradeMod = GetComponent<SelfUpgrade>();

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

            selfUpgradeMod.Remove(_player);

            AttackType = (PlayerAttackTypes)value;

            PlayerData.CurrentAttackTime = _attackTimes[AttackType];
            selfUpgradeMod.Strenght = _attackProjectiles[AttackType].Damage;
            selfUpgradeMod.Apply(_player);

            GlobalEvents.Send_OnPlayerChangedAttackType();
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
                offensiveProjectile.Initialize(projectileData, _player.GetDamage);
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
}