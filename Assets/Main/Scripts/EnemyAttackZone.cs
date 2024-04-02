using UnityEngine;

public class EnemyAttackZone : MonoBehaviour
{
    public EnemyData _data;

    private EnemyCentral central;
    private BoxCollider _col;

    private void Awake()
    {
        central = GetComponentInParent<EnemyCentral>();
        _col = GetComponent<BoxCollider>();
        _data = central.Data;
    }

    private void Start()
    {
        _col.size = new(_col.size.x, _col.size.y, _data.AttackRange);
        _col.center += new Vector3(0, 0, _data.AttackRange / 2); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            central.SwitchToState(EnemyCentral.EnemyStates.Attack);
        }
    }
}
