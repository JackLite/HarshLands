using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector3 _force;
    public float speed = 5f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void ProcessInput(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        _force = new Vector3(value.x, 0, value.y);
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(_force * speed, ForceMode.Acceleration);
    }
}
