using BadJuja.Core;
using BadJuja.Core.Data;
using BadJuja.Core.Events;
using UnityEngine;

namespace BadJuja.Player {
    public class PlayerCombat : MonoBehaviour {

        // No Player attack animation, only weapon switch animation

        public PlayerCurrentEquipment PlayerCurrentEquipment;

        public Transform FiringPoint;

        [SerializeField] private GameObject _meleeWeapon;
        [SerializeField] private GameObject _lightWeapon;
        [SerializeField] private GameObject _heavyWeapon;

        private GameObject[] _weapons;
        private GameObject _currentWeapon;

        private PlayerWeaponTypes CurrentWeaponType = PlayerWeaponTypes.Light;

        private Player _player;

        private void OnEnable()
        {
            PlayerRelatedEvents.OnSwitchToAttackType += SwitchAttackType;
            PlayerRelatedEvents.PlayerInitiated += () => SwitchAttackType(0);
        }

        private void OnDisable()
        {
            PlayerRelatedEvents.OnSwitchToAttackType -= SwitchAttackType;
            PlayerRelatedEvents.PlayerInitiated -= () => SwitchAttackType(0);
        }

        private void Awake()
        {
            _player = GetComponentInParent<Player>();
            InitiateWeaponry();
        }

        private void InitiateWeaponry()
        {
            if (PlayerCurrentEquipment == null) return;

            _meleeWeapon.GetComponent<PlayerWeaponMelee>()
                .Initiate(
                    PlayerCurrentEquipment.MeleeWeaponData, 
                    PlayerCurrentEquipment.MeleeWeaponElement
                );
            _lightWeapon.GetComponent<PlayerWeaponRanged>()
                .Initiate(
                    PlayerCurrentEquipment.LightRangedWeaponData, 
                    PlayerCurrentEquipment.LightRangedWeaponElement
                );
            _heavyWeapon.GetComponent<PlayerWeaponRanged>()
                .Initiate(
                    PlayerCurrentEquipment.HeavyRangedWeaponData, 
                    PlayerCurrentEquipment.HeavyRangedWeaponElement
                );

            _weapons = new GameObject[]
            {
                _meleeWeapon,
                _lightWeapon,
                _heavyWeapon
            };

        }
        private void SwitchAttackType(int value)
        {
            if (value < 0 || value >= System.Enum.GetValues(typeof(PlayerWeaponTypes)).Length)
            {
                Debug.LogError("Invalid attack method value.");
                return;
            }
            _player.RemoveStatMod(AllCharacterStats.Damage, this);
            CurrentWeaponType = (PlayerWeaponTypes)value;

            float _ = 0;
            switch (CurrentWeaponType)
            {
                case PlayerWeaponTypes.Melee:
                    _ = PlayerCurrentEquipment.MeleeWeaponData.Damage;
                    break;
                case PlayerWeaponTypes.Light:
                    _ = PlayerCurrentEquipment.LightRangedWeaponData.Damage;
                    break;
                case PlayerWeaponTypes.Heavy:
                    _ = PlayerCurrentEquipment.HeavyRangedWeaponData.Damage;
                    break;
            }

            _player.ApplyStatMod(AllCharacterStats.Damage, Core.CharacterStats.StatModType.Flat, _, this);

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
        }
    }
}