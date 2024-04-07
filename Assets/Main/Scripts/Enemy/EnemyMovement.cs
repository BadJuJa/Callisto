using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent _agent;
    private IEnemyCentral _enemyCentralInterface;
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _enemyCentralInterface = GetComponent<IEnemyCentral>();

        Initialize();
    }

    public void Initialize()
    {
        if (_enemyCentralInterface.EnemyData == null) return;

        _agent.speed = _enemyCentralInterface.EnemyData.MovementSpeed;
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

}

