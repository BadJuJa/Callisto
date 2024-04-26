using BadJuja.Core;
using UnityEngine;
using UnityEngine.AI;

namespace BadJuja.Enemy {
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMovement : MonoBehaviour {
        private NavMeshAgent _agent;
        private IEnemyCentral _enemyCentralInterface;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _enemyCentralInterface = GetComponent<IEnemyCentral>();
        }
        private void Update()
        {
            if (_enemyCentralInterface.PlayerTransform == null) return;

            if (!_enemyCentralInterface.PlayerInReachDistance)
            {
                MoveTowardsPlayer();
            }
            else
            {
                _agent.isStopped = true;
            }
        }

        void MoveTowardsPlayer()
        {
            _agent.isStopped = false;
            _agent.SetDestination(_enemyCentralInterface.PlayerTransform.position);
        }

        public void SetSpeed(float value)
        {
            _agent.speed = value;
        }
    }
}