using BadJuja.Core.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.Player.VFX
{
    public class PlayerVFXContoller : MonoBehaviour
    {
        public PlayerCurrentEquipment _playerCurrentEquipment;

        public GameObject MeleeAreaEffectPrefab;
        private GameObject _meleeAreaEffectGO;

        private void Start()
        {
            InitiateMeleeVFC();
        }

        private void InitiateMeleeVFC()
        {
            if (_playerCurrentEquipment.MeleeWeaponData.Behaviour == Core.MeleeWeaponBehaviour.AreaDamage)
            {
                _meleeAreaEffectGO = Instantiate(MeleeAreaEffectPrefab, transform, false);
                _meleeAreaEffectGO.transform.localPosition = new(0, 0.05f, 0);
                float newScale = (1 / 4.5f) * _playerCurrentEquipment.MeleeWeaponData.Radius;
                _meleeAreaEffectGO.transform.localScale = newScale * Vector3.one;
                _meleeAreaEffectGO.SetActive(false);
            }
        }
    }
}
