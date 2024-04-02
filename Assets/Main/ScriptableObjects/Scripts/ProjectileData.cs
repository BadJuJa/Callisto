using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "Data/Projectile", order = 1)]
public class ProjectileData : ScriptableObject {
    [Header("Model")]
    public GameObject ProjectilePrefab;

    [Header("Attack Data")]
    public PlayerAttackTypes AttackType;
    public float AttackTime;

    [Header("Damage Data")]
    public float Speed = 25;
    public float Damage = 10;
    public int MaxPiercing = 1;
}
