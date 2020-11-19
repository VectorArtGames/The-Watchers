using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float Damping;
    public float _desiredRotation;
    public float DesiredRotation
    {
        get => _desiredRotation;
        set => _desiredRotation = Mathf.Repeat(value, 360);
    }

    public float RotationDamping;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DesiredRotation += 90;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            DesiredRotation -= 90;
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,
            target.position, Time.fixedDeltaTime * Damping);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.AngleAxis(DesiredRotation, Vector3.up),
            RotationDamping * Time.deltaTime);
    }
}
