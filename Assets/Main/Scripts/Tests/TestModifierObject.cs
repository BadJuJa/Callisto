using UnityEngine;

public class TestModifierObject : MonoBehaviour
{
    public Transform tObject;

    private StatUpgrade upgrade;
    private bool applied = false;

    private void Awake()
    {
        upgrade = GetComponent<StatUpgrade>();
    }

    private void Update()
    {
        if (tObject == null) return;
        if (applied) return;

        tObject.Rotate(Vector3.up * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (applied) return;
        if (!other.CompareTag("Player")) return;

        if (other.TryGetComponent(out CharacterCore c))
        {
            upgrade.Apply(c);
            var t = c.Stats[upgrade.Stat].Value;
            applied = true;

            var holder = GetComponentInParent<UpgradeHolder>();
            
            transform.SetParent(c.UpgradesParent);

            holder.Inform();

            tObject.gameObject.SetActive(false);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
