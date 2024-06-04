using BadJuja.Core;
using UnityEngine;
using UnityEngine.AI;

namespace BadJuja.Enemy {
    public class AnimatorControllerScript : MonoBehaviour {
        private NavMeshAgent _agent;

        private Animator _animator;

        private CombatBase _attackSctipt;

        int velocityParamHash = Animator.StringToHash("Velocity");
        int attackTriggerParamHash = Animator.StringToHash("AttackTrigger");

        private void Awake()
        {
            _agent = GetComponentInParent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        public void Initialize(CombatBase enemyAttack)
        {
            _attackSctipt = enemyAttack;
        }

        private void LateUpdate()
        {
            if (GameManager.GameIsPaused) return;

            _animator.SetFloat(velocityParamHash, _agent.velocity.magnitude);
        }

        public void TriggerAttack()
        {
            _animator.SetTrigger(attackTriggerParamHash);
        }

        public void Attack()
        {
            _attackSctipt.Attack();
        }
    }
}