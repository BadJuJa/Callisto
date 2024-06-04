using BadJuja.Core;
using UnityEngine;
using UnityEngine.AI;

namespace BadJuja.Enemy {
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movement : MonoBehaviour {
        private NavMeshAgent _agent;
        private IEnemyCentral _centralInterface;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _centralInterface = GetComponent<IEnemyCentral>();
        }
        private void Update()
        {
            _agent.isStopped = !GameManager.GameIsPaused;
            if (GameManager.GameIsPaused) return;

            if (_centralInterface.PlayerTransform == null) return;

            if (!_centralInterface.PlayerInReachDistance)
            {
                MoveTowardsPlayer();
            }
            else
            {
                _agent.isStopped = true;
            }
        }

        private void MoveTowardsPlayer()
        {
            _agent.isStopped = false;
            _agent.SetDestination(_centralInterface.PlayerTransform.position);
        }

        public void SetSpeed(float value)
        {
            _agent.speed = value;
        }
    }
}