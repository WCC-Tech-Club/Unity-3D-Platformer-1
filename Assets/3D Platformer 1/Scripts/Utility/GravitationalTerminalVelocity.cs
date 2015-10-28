using UnityEngine;

using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class GravitationalTerminalVelocity : MonoBehaviour
{
    private new Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rigidbody.useGravity = false;
        Vector3 velocity = rigidbody.velocity;
        velocity.y += (Physics.gravity.y - (velocity.y * rigidbody.drag)) * Time.deltaTime;
        rigidbody.velocity = velocity;
    }
}
