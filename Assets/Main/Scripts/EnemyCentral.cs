using System.Collections;
using UnityEngine;

public enum EnemyTypes {
    Melee,
    Range,
}


public class EnemyCentral : MonoBehaviour, IDamagable, IEnemyCentral {

    [Header("References")]
    public EnemyData Data;

    [Header("General")]
    private float _maxHealth;
    private float _health;

    public bool PlayerInReachDistance { get; set; }
    public EnemyData EnemyData { get => Data; private set => Data = value; }
    public Transform PlayerTransform { get; private set; }

    private void InitializeData()
    {
        if (!Data) return;

        _maxHealth = Data.Health;

        Instantiate(original: Data.Prefab, parent: gameObject.transform, rotation: Quaternion.identity, position: transform.position);
    }

    private void Awake()
    {
        InitializeData();

        StartCoroutine(FindPlayer());
    }
    
    void Start()
    {
        _health = _maxHealth;
    }
    
    void Update()
    {
        if (true)
        {
            // TODO: Player dies
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

    private IEnumerator FindPlayer()
    {
        while (PlayerTransform == null)
        {
            PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            yield return new WaitForSeconds(.5f);
        }
    }
}
