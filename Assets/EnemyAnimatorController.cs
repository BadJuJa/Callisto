using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    private Rigidbody _rb;

    private Animator _animator;

    private EnemyCentral _central;

    int velocityParamHash = Animator.StringToHash("Velocity");
    int attackTriggerParamHash = Animator.StringToHash("AttackTrigger");

    private void Start()
    {
        _rb = GetComponentInParent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _central = GetComponentInParent<EnemyCentral>();

    }

    private void LateUpdate()
    {
        _animator.SetFloat(velocityParamHash, _rb.velocity.magnitude);
    }

    public void TriggerAttack()
    {
        print("Attack Triggered");
        _animator.SetTrigger(attackTriggerParamHash);
    }

    public void Attack()
    {
        _central.Attack();
    }
}
