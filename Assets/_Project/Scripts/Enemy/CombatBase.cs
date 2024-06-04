using BadJuja.Core;
using BadJuja.Core.CharacterStats;
using BadJuja.Core.Data;
using System.Collections;
using UnityEngine;
 
namespace BadJuja.Enemy {
    public abstract class CombatBase {
        
        private AnimatorControllerScript _animatorController;
        private MonoBehaviour _context;
        private Timer _timer;
        
        private IStats _stats;        
        private bool _canAttack = false;

        protected IEnemyCentral _centralInterface;
        protected Transform _attackPoint;
        protected EnemyData _data;

        protected CombatBase(MonoBehaviour context, IEnemyCentral enemyCentral)
        {
            _context = context;

            _centralInterface = enemyCentral;
            
            _timer = new(_context);
            _timer.TimeIsOver += () => _context.StartCoroutine(StartAttacking());
        }

        public virtual void Initialize(EnemyData Data, IStats stats, Transform attackPoint, AnimatorControllerScript animatorController)
        {
            _data = Data;
            _stats = stats;
            _attackPoint = attackPoint;
            _animatorController = animatorController;
            
            StartCounting();
        }

        private void StartCounting()
        {
            _timer.Set(_data.AttackCooldown);
            _timer.StartCountingTime();
        }

        private IEnumerator StartAttacking()
        {
            _canAttack = true;
            while (_canAttack)
            {
                if (_centralInterface.PlayerInReachDistance)
                {
                    _animatorController.TriggerAttack();
                    _canAttack = false;
                }
                else yield return new WaitForSeconds(.1f);
            }
        }

        public virtual void Attack()
        {
            StartCounting();
        }

        protected float CalculateDamage()
        {
            float baseDamage = _stats.GetStatValue(AllCharacterStats.Damage);
            float elementalMult = 1 + _stats.GetElementalBonus(_data.Element.Element) / 100;

            return baseDamage * elementalMult;
        }
    }
}