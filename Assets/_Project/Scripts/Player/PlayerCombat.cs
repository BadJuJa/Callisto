using BadJuja.Core;
using BadJuja.Core.CharacterStats;
using BadJuja.Core.Data;
using BadJuja.Core.Events;
using BadJuja.Player.Weapons;
using System.Collections;
using UnityEngine;

namespace BadJuja.Player {
    public class PlayerCombat : MonoBehaviour {

        private PlayerCurrentEquipment _playerCurrentEquipment;


        [SerializeField] private GameObject _meleeWeapon;
        [SerializeField] private GameObject _lightWeapon;
        [SerializeField] private GameObject _heavyWeapon;

        private GameObject[] _weapons;
        private GameObject _currentWeapon;
        public Transform FiringPoint { get; private set; }

        private PlayerWeaponTypes CurrentWeaponType = PlayerWeaponTypes.Light;

        private StatModifier damageStatMod;

        private bool _canSwitchWeapon = false;

        private void OnEnable()
        {
            _canSwitchWeapon = true;
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        private void SubsribeToEvents()
        {
            PlayerRelatedEvents.OnSwitchToAttackType += SwitchAttackType;
        }

        private void UnsubscribeFromEvents()
        {
            PlayerRelatedEvents.OnSwitchToAttackType -= SwitchAttackType;
        }

        public void Initialize(Transform firingPoint, PlayerCurrentEquipment playerCurrentEquipment)
        {
            _playerCurrentEquipment = playerCurrentEquipment;
            FiringPoint = firingPoint;

            SubsribeToEvents();

            InitiateWeaponry();

            _canSwitchWeapon = true;
            SwitchAttackType(0);
        }

        private void InitiateWeaponry()
        {
            if (_playerCurrentEquipment == null) return;

            IStats playerStats = GetComponentInParent<IStats>();

            _meleeWeapon.GetComponent<PlayerWeaponMelee>()
                .Initiate(
                    _playerCurrentEquipment.MeleeWeaponData, 
                    _playerCurrentEquipment.MeleeWeaponElement.Element,
                    playerStats

                );
            _lightWeapon.GetComponent<PlayerWeaponRanged>()
                .Initiate(
                    _playerCurrentEquipment.LightRangedWeaponData, 
                    _playerCurrentEquipment.LightRangedWeaponElement.Element,
                    playerStats
                );
            _heavyWeapon.GetComponent<PlayerWeaponRanged>()
                .Initiate(
                    _playerCurrentEquipment.HeavyRangedWeaponData, 
                    _playerCurrentEquipment.HeavyRangedWeaponElement.Element, 
                    playerStats
                );

            _weapons = new GameObject[]
            {
                _meleeWeapon,
                _lightWeapon,
                _heavyWeapon
            };

            foreach (var item in _weapons)
            {
                item.SetActive(false);
            }
        }
        private void SwitchAttackType(int value)
        {
            if (value < 0 || value >= System.Enum.GetValues(typeof(PlayerWeaponTypes)).Length)
            {
                Debug.LogError("Invalid attack method value.");
                return;
            }

            if (!_canSwitchWeapon) return;

            ModifiersRelaterEvents.Send_RemovePlayerModifier(AllCharacterStats.Damage, this);

            CurrentWeaponType = (PlayerWeaponTypes)value;

            float weaponDamage = 0;
            switch (CurrentWeaponType)
            {
                case PlayerWeaponTypes.Melee:
                    weaponDamage = _playerCurrentEquipment.MeleeWeaponData.Damage;
                    break;
                case PlayerWeaponTypes.Light:
                    weaponDamage = _playerCurrentEquipment.LightRangedWeaponData.Damage;
                    break;
                case PlayerWeaponTypes.Heavy:
                    weaponDamage = _playerCurrentEquipment.HeavyRangedWeaponData.Damage;
                    break;
            }

            damageStatMod = new(weaponDamage, StatModType.Flat, this);

            ModifiersRelaterEvents.Send_AddPlayerModifier(AllCharacterStats.Damage, damageStatMod, this);

            SwitchWeapon();

            PlayerRelatedEvents.Send_OnPlayerChangedAttackType();
        }

        private void SwitchWeapon()
        {
            if (_currentWeapon != null) _currentWeapon.SetActive(false);

            switch (CurrentWeaponType)
            {
                case PlayerWeaponTypes.Melee:
                    _currentWeapon = _weapons[0];
                    break;
                case PlayerWeaponTypes.Light:
                    _currentWeapon = _weapons[1];
                    break;
                case PlayerWeaponTypes.Heavy:
                    _currentWeapon = _weapons[2];
                    break;
            }

            _currentWeapon.SetActive(true);

            StartCoroutine(WeaponSwitchDelay());
        }

        private IEnumerator WeaponSwitchDelay()
        {
            _canSwitchWeapon = false;
            yield return new WaitForSeconds(1f);
            _canSwitchWeapon = true;
        }
    }
}