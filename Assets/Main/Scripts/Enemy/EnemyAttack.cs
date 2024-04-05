using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Combat")]
    private float _attackCooldown;
    private bool _canAttack = false;

    [Header("References")]
    private EnemyAnimatorController _animatorController;
    private IEnemyCentral _centralInterface;
    public Transform AttackPoint;
    public LayerMask PlayerLayer;

    private EnemyData _data;

    private void Awake()
    {
        _centralInterface = GetComponentInParent<EnemyCentral>();
        _data = _centralInterface.EnemyData;
    }

    private void Start()
    {
        _animatorController = GetComponentInChildren<EnemyAnimatorController>();

        _attackCooldown = _data.AttackCooldown;
    }


    private void Update()
    {
        if (_animatorController == null) return;

        if (!_canAttack)
        {
            if (_attackCooldown <= 0) _canAttack = true;
            else _attackCooldown -= Time.deltaTime;
        }

        if (_canAttack && _centralInterface.PlayerInReachDistance)
        {
            if (_animatorController)
                _animatorController.TriggerAttack();
            _canAttack = false;
            _attackCooldown = _data.AttackCooldown;
        }
    }

    public void Attack()
    {
        switch (_data.EnemyType)
        {
            case EnemyTypes.Melee:

                Collider[] res = Physics.OverlapSphere(AttackPoint.position, .75f, PlayerLayer);
                if (res.Length > 0)
                {
                    if (res[0].TryGetComponent(out IDamagable damagable))
                    {
                        damagable.TakeDamage(_data.Damage);
                    }
                }

                break;

            case EnemyTypes.Range:
                
                GameObject projectile = Instantiate(_data.AttackProjectilePrefab, AttackPoint.position, Quaternion.LookRotation(AttackPoint.forward));
                OffensiveProjectile offensiveProjectile = projectile.GetComponent<OffensiveProjectile>();

                break;
        }

    }
}
