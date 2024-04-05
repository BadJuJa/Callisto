using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimatorController : MonoBehaviour
{
    private NavMeshAgent _agent;

    private Animator _animator;

    private EnemyAttack _central;

    int velocityParamHash = Animator.StringToHash("Velocity");
    int attackTriggerParamHash = Animator.StringToHash("AttackTrigger");

    private void Start()
    {
        _agent = GetComponentInParent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _central = GetComponentInParent<EnemyAttack>();

    }

    private void LateUpdate()
    {
        _animator.SetFloat(velocityParamHash, _agent.velocity.magnitude);
    }

    public void TriggerAttack()
    {
        _animator.SetTrigger(attackTriggerParamHash);
    }

    public void Attack()
    {
        _central.Attack();
    }
}
