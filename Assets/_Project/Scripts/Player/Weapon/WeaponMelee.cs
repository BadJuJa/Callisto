using BadJuja.Core;
using BadJuja.Core.CharacterStats;
using BadJuja.Core.Data;
using System;
using UnityEngine;

namespace BadJuja.Player.Weapons {
    public class WeaponMelee : Weapon {
        
        private MeleeWeaponData _data;
        private MeleeWeaponBehaviour _behaviourType;
        private IWeaponBehaviour _behaviourInterface;

        private object _behaviourScript;

        public Action WasEnabled;

        private void OnEnable()
        {
            WasEnabled?.Invoke();
        }

        public void Initiate(MeleeWeaponData data, WeaponElements element, IStats playerStats)
        {
            _data = data;
            _behaviourType = _data.Behaviour;

            base.Initiate(element, playerStats);
        }

        protected override void InitiateBehaviour()
        {
            switch (_behaviourType)
            {
                case MeleeWeaponBehaviour.CircularMotion:

                    int _maxNumberOfObjects = 5;
                    GameObject[] _distributedObjects = new GameObject[_maxNumberOfObjects];

                    for (int i = 0; i < _maxNumberOfObjects; i++)
                    {
                        _distributedObjects[i] = Instantiate(_data.Model, transform.position, Quaternion.identity, transform);
                    }

                    _behaviourScript = new Behaviour.CircleDistribution(transform, _distributedObjects, _data.Radius);
                    _behaviourInterface = (IWeaponBehaviour)_behaviourScript;
                    break;

                case MeleeWeaponBehaviour.AreaDamage:
                    _behaviourScript = new Behaviour.AreaDamage(this, _data.Radius, 1f);
                    _behaviourInterface = (IWeaponBehaviour)_behaviourScript;
                    break;
            }
        }
    }
}