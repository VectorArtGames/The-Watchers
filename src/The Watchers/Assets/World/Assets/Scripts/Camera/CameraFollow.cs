using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float Damping;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,
            target.position, Time.fixedDeltaTime * Damping);
    }
}
