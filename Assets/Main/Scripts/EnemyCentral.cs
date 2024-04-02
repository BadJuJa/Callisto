using UnityEngine;
using UnityEngine.AI;

public enum EnemyTypes {
    Melee,
    Range,
}

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyCentral : MonoBehaviour, IDamagable {
    public enum EnemyStates
    {
        Idle,
        Chasing,
        Attack,
    }

    [Header("References")]
    public EnemyData Data;
    public Transform AttackPoint;
    public Transform PlayerTransform;
    public LayerMask PlayerLayer;
    private NavMeshAgent _agent;
    private EnemyAnimatorController _animatorController;

    [Header("General")]
    private float _maxHealth;
    private float _health;
    private EnemyStates currentState = EnemyStates.Idle;

    [Header("Combat")]
    private float _attackCooldown;

    [Header("Inner useage")]
    private float distanceToPlayer;

    private void InitializeData()
    {
        if (!Data) return;

        _maxHealth = Data.Health;
        _attackCooldown = Data.AttackCooldown;

        Instantiate(original: Data.Prefab, parent: gameObject.transform, rotation: Quaternion.identity, position: transform.position);
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        

        InitializeData();

        if (!PlayerTransform)
        {
            var playerGO = GameObject.FindGameObjectWithTag("Player");
            if (playerGO)
            {
                PlayerTransform = playerGO.transform;
            }   
        }
    }
    
    void Start()
    {
        _agent.speed = Data.MovementSpeed;
        _health = _maxHealth;

        _animatorController = GetComponentInChildren<EnemyAnimatorController>();


        SwitchToState(EnemyStates.Chasing);
    }
    
    void Update()
    {
        if (PlayerTransform)
            distanceToPlayer = Vector3.Distance(transform.position, PlayerTransform.position);
        else return;

        switch (currentState)
        {
            case EnemyStates.Idle:
                UpdateIdleState();
                break;
            case EnemyStates.Attack:
                UpdateAttackState();
                break;
            case EnemyStates.Chasing:
                UpdateChasingState();
                break;
        }

        
        if (true)
        {
            // TODO: Player dies
            //SwitchToState(EnemyStates.Idle);
        }
    }

    private void UpdateIdleState()
    {
        // Play some animations
    }

    private void UpdateChasingState()
    {
        if (distanceToPlayer <= Data.AttackRange)
        {
            SwitchToState(EnemyStates.Attack);
        }
        else
        {
            MoveTowardsPlayer();
        }
    }
    
    private void UpdateAttackState()
    {
        if (_attackCooldown <= 0)
        {
            if (_animatorController)
                _animatorController.TriggerAttack();
            
            _attackCooldown = Data.AttackCooldown;
        }
        else _attackCooldown -= Time.deltaTime;

        if (distanceToPlayer > Data.AttackRange) SwitchToState(EnemyStates.Chasing);
    }
    void MoveTowardsPlayer()
    {
        // check if has clear view of the player
        _agent.SetDestination(PlayerTransform.position);
    }

    public void Attack()
    {
        switch(Data.EnemyType)
        {
            case EnemyTypes.Melee:

                var res = Physics.OverlapSphere(AttackPoint.position, Data.AttackRange, PlayerLayer);
                res[0].GetComponent<IDamagable>().TakeDamage(Data.Damage);

                break;
            case EnemyTypes.Range:
                // spawn projectile in the AttackPoint to the direction of a player
                break;
        }
        
    }

    public void TakeDamage(float value)
    {
        if (_health > value) _health -= value;
        else Die();
    }

    private void Die()
    {
        print($"You died, {name}!");
        Destroy(gameObject);
    }

    public void SwitchToState(EnemyStates newState)
    {
        switch (newState)
        {
            case EnemyStates.Idle:
                _agent.isStopped = true;
                break;
            case EnemyStates.Attack:
                _agent.isStopped = true;
                break;
            case EnemyStates.Chasing:
                _agent.isStopped = false;
                break;
        }
        currentState = newState;
    }
}
