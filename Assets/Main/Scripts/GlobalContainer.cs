using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalContainer : MonoBehaviourSingleton<GlobalContainer>
{
    [SerializeField] private SharedPlayerData _sharedPlayerData;

    public Transform PlayerTransform;

    protected override void SingletonStarted()
    {
        base.SingletonStarted();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _sharedPlayerData.PlayerTransform = PlayerTransform;
    }
}
