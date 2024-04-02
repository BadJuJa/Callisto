using UnityEngine;

public class OffensiveProjectile : MonoBehaviour {
    private ProjectileData _projectileData;

    private int _remainingPiercing;

    public void Initialize(ProjectileData projectileData)
    {
        _projectileData = projectileData;
        _remainingPiercing = _projectileData.MaxPiercing;
    }

    private void Start()
    {
        var _rb = GetComponent<Rigidbody>();
        _rb.velocity = transform.forward * _projectileData.Speed;
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagable) && _remainingPiercing > 0)
        {
            damagable.TakeDamage(_projectileData.Damage);
            if (--_remainingPiercing < 1)
                Destroy(gameObject);
        }
    }
}
