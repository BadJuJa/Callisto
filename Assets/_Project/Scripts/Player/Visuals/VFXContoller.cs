using BadJuja.Core.Data;
using BadJuja.Core.Events;
using UnityEngine;

namespace BadJuja.Player.Visuals
{
    public class VFXContoller : MonoBehaviour
    {
        public PlayerCurrentEquipment _playerCurrentEquipment;

        public GameObject MeleeAreaEffectPrefab;
        private GameObject _meleeAreaEffectGO;

        private void Awake()
        {
            if (_playerCurrentEquipment.MeleeWeaponData.Behaviour == Core.MeleeWeaponBehaviour.AreaDamage)
            {
                PlayerRelatedEvents.OnSwitchToAttackType += EnableAreaVFX;
                InitiateMeleeVFC();
            }
        }

        private void EnableAreaVFX(int value)
        {
            if (value == 0)
                _meleeAreaEffectGO.SetActive(true);
            else
                _meleeAreaEffectGO.SetActive(false);
        }

        private void InitiateMeleeVFC()
        {
            if (_playerCurrentEquipment.MeleeWeaponData.Behaviour == Core.MeleeWeaponBehaviour.AreaDamage)
            {
                _meleeAreaEffectGO = Instantiate(MeleeAreaEffectPrefab, transform, false);
                _meleeAreaEffectGO.transform.localPosition = new(0, 0.05f, 0);
                float newScale = (1 / 4.5f) * _playerCurrentEquipment.MeleeWeaponData.Radius;
                _meleeAreaEffectGO.transform.localScale = newScale * Vector3.one;
                _meleeAreaEffectGO.SetActive(true);
            }
        }
    }
}
