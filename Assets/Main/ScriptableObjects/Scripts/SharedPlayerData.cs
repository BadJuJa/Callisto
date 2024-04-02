using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SharedPlayerData", menuName = "Data/Shared Player Data", order = 1)]
public class SharedPlayerData : ScriptableObject
{
    public float CurrentAttackTime = 1f;
    public Transform PlayerTransform;
}
