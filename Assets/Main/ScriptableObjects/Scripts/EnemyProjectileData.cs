using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy Projectile Data", menuName = "Data/Enemy Projectile")]
public class EnemyProjectileData : ScriptableObject {
    [Header("Model")]
    public GameObject ProjectilePrefab;

    [Header("Damage Data")]
    public float Speed = 25;
    public int MaxPiercing = 1;
}
