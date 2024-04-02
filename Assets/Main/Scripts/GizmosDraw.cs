using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosDraw : MonoBehaviour
{
    private InputHandler _input;

    private void Awake()
    {
        _input = GetComponent<InputHandler>();
    }

    private void OnDrawGizmos()
    {
        if (!_input) return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + _input.LookInputVector3D.ToIso());
    }
}
