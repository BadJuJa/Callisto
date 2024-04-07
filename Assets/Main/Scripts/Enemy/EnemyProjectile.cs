using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public EnemyProjectileData ProjectileData;
    private float _damage;

    private int _remainingPiercing;

    public void Initialize(float enemyDamage)
    {
        _remainingPiercing = ProjectileData.MaxPiercing;
        _damage = enemyDamage;

        var _rb = GetComponent<Rigidbody>();
        _rb.velocity = transform.forward * ProjectileData.Speed;

        Destroy(gameObject, 5);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (other.TryGetComponent(out IDamagable damagable) && _remainingPiercing > 0)
        {
            damagable.TakeDamage(_damage);
            if (--_remainingPiercing < 1)
                Destroy(gameObject);
        }
    }
}
