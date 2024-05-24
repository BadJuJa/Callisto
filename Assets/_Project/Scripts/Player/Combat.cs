using BadJuja.Core;
using BadJuja.Core.CharacterStats;
using BadJuja.Core.Data;
using BadJuja.Core.Events;
using BadJuja.Player.Weapons;
using System.Collections;
using UnityEngine;

namespace BadJuja.Player {
    public class Combat : MonoBehaviour {

        private PlayerCurrentEquipment _currentEquipment;

        [SerializeField] private GameObject _meleeWeapon;
        [SerializeField] private GameObject _lightWeapon;
        [SerializeField] private GameObject _heavyWeapon;

        private GameObject[] _weapons;
        private GameObject _currentWeapon;
        public Transform FiringPoint { get; private set; }

        private PlayerWeaponTypes _weaponType = PlayerWeaponTypes.Light;

        private StatModifier damageStatMod;

        private bool _canSwitchWeapon = false;
        public void Initialize(Transform firingPoint, PlayerCurrentEquipment playerCurrentEquipment)
        {
            _currentEquipment = playerCurrentEquipment;
            FiringPoint = firingPoint;

            SubsribeToEvents();

            InitiateWeaponry();

            _canSwitchWeapon = true;
            SwitchAttackType(0);
        }

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

        private void InitiateWeaponry()
        {
            if (_currentEquipment == null) return;

            IStats playerStats = GetComponentInParent<IStats>();

            _meleeWeapon.GetComponent<WeaponMelee>()
                .Initiate(
                    _currentEquipment.MeleeWeaponData, 
                    _currentEquipment.MeleeWeaponElement.Element,
                    playerStats
                );
            _lightWeapon.GetComponent<WeaponRanged>()
                .Initiate(
                    _currentEquipment.LightRangedWeaponData, 
                    _currentEquipment.LightRangedWeaponElement.Element,
                    playerStats
                );
            _heavyWeapon.GetComponent<WeaponRanged>()
                .Initiate(
                    _currentEquipment.HeavyRangedWeaponData, 
                    _currentEquipment.HeavyRangedWeaponElement.Element, 
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

            _weaponType = (PlayerWeaponTypes)value;

            float weaponDamage = 0;
            switch (_weaponType)
            {
                case PlayerWeaponTypes.Melee:
                    weaponDamage = _currentEquipment.MeleeWeaponData.Damage;
                    break;
                case PlayerWeaponTypes.Light:
                    weaponDamage = _currentEquipment.LightRangedWeaponData.Damage;
                    break;
                case PlayerWeaponTypes.Heavy:
                    weaponDamage = _currentEquipment.HeavyRangedWeaponData.Damage;
                    break;
            }

            damageStatMod = new(weaponDamage, StatModType.Flat, this);

            ModifiersRelaterEvents.Send_AddPlayerModifier(AllCharacterStats.Damage, damageStatMod, this);

            SwitchWeapon();

            PlayerRelatedEvents.Send_OnChangedAttackType();
        }

        private void SwitchWeapon()
        {
            if (_currentWeapon != null) _currentWeapon.SetActive(false);

            switch (_weaponType)
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