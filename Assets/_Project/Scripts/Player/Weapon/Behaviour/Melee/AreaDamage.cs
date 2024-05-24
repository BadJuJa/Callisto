using BadJuja.Core;
using System.Collections.Generic;
using UnityEngine;

namespace BadJuja.Player.Weapons.Behaviour {
    public class AreaDamage : IWeaponBehaviour {

        private IWeapon _parentWeapon;
        private Transform _parentWeaponTransform;
        private float _radius = .5f;
        private float _attackInterval = 1f;

        private Timer _timer;
        
        public AreaDamage(WeaponMelee weaponScript, float radius, float attackInterval)
        {
            _parentWeapon = weaponScript.GetComponent<IWeapon>();
            _parentWeaponTransform = weaponScript.gameObject.transform;

            _radius = radius;
            _attackInterval = attackInterval;

            _timer = new(weaponScript);

            _timer.TimeIsOver += TriggerAttack;
            weaponScript.WasEnabled += StartCounting;
        }

        private void StartCounting()
        {
            _timer.Set(_attackInterval);
            _timer.StartCountingTime();
        }
         
        private void TriggerAttack()
        {
            Collider[] enemies = Physics.OverlapSphere(_parentWeaponTransform.position, _radius, LayerMask.GetMask("Enemy"));
            StartCounting();
            
            if (enemies.Length == 0) return;

            foreach(Collider enemy in enemies)
            {
                if (enemy.TryGetComponent(out IDamagable damagable))
                {
                    _parentWeapon.DealDamage(damagable, enemies.Length);
                }
            }
        }

        public void Upgrade(Dictionary<string, float> args)
        {
            if (args.ContainsKey("Interval"))
            {
                var temp = _attackInterval * (1 - args["Interval"] / 100);

                _attackInterval = Mathf.Clamp(temp, 0.1f, 1f);
            }

            if (args.ContainsKey("Radius"))
                _radius += args["Radius"];
        }
    }
}