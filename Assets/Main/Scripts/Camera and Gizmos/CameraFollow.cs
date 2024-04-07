using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform CameraFollowTransform;
    public Vector3 offset;

    void Update()
    {
        if (!CameraFollowTransform) return;

        transform.position = CameraFollowTransform.position + offset;
    }
}
