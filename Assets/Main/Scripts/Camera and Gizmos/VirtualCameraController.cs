using BadJuja.Core;
using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraController : MonoBehaviour
{
    private CinemachineConfiner confiner;

    private void Awake()
    {
        confiner = GetComponent<CinemachineConfiner>();
    }

    private void OnEnable()
    {
        GlobalEvents.OnNewCameraConfinerColliderAvailable += AssignNewConfiner;
    }

    private void AssignNewConfiner(Collider collider)
    {
        confiner.m_BoundingVolume = collider;
    }
}
