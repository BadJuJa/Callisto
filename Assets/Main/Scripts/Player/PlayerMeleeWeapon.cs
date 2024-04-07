using UnityEngine;

public class PlayerMeleeWeapon : MonoBehaviour
{
    public CharacterCore characterCore;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        if (other.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(characterCore.Damage.Value);
        }
    }

}
