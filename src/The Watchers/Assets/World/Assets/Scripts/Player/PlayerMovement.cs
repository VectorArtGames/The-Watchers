using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float Speed;
    public Vector3 Movement;

    private void Awake()
    {
        TryGetComponent(out controller);
    }

    // Update is called once per frame
    void Update()
    {
        Movement.x = Input.GetAxis("Horizontal");
        Movement.z = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        var dir = transform.TransformDirection(Movement);
        controller.SimpleMove(new Vector3(dir.x, 0, dir.z) * Speed);
    }
}
