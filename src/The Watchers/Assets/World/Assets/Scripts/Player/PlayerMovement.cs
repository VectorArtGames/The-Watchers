using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float Speed;
    public float RotationSpeed = 5f;

    public Vector3 Movement;

    private void Awake()
    {
        TryGetComponent(out controller);
    }

    // Update is called once per frame
    void Update()
    {
        Movement.z = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        var mousePos = Input.mousePosition;
        var playerPos = Camera.main.WorldToScreenPoint(transform.position);
        var n = mousePos - playerPos;
        var angle = (Mathf.Atan2(n.y, n.x) *
            Mathf.Rad2Deg) - 90 - (Camera.main.transform.rotation.eulerAngles.y);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(-angle, Vector3.up), RotationSpeed * Time.fixedDeltaTime);

        var dir = transform.TransformDirection(Movement);
        controller.SimpleMove(new Vector3(dir.x, 0, dir.z) * Speed);
    }

    public void Teleport(Vector3 position)
    {
        controller.enabled = false;
        transform.position = position;
        controller.enabled = true;
    }
}
