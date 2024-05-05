using BadJuja.Core.Events;
using Cinemachine;
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
        LevelLoadingRelatedEvents.OnNewCameraConfinerColliderAvailable += AssignNewConfiner;
    }

    private void AssignNewConfiner(Collider collider)
    {
        confiner.m_BoundingVolume = collider;
    }
}
