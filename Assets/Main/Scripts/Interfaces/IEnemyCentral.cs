using UnityEngine;

public interface IEnemyCentral
{
    public bool PlayerInReachDistance {  get; set; }
    public EnemyData EnemyData { get; }
    public Transform PlayerTransform { get; }
}
