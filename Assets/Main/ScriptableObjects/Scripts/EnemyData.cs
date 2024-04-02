using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Data/Enemy")]
public class EnemyData : ScriptableObject {

    [Header("Prefab Settings")]
    public GameObject Prefab;

    [Header("Combat Settings")]
    public float AttackRange;
    public float Damage;
    public float Health;
    public float AttackCooldown;

    [Header("Movement Settings")]
    public float MovementSpeed;

    [Header("Other Settings")]
    public EnemyTypes EnemyType;
}
