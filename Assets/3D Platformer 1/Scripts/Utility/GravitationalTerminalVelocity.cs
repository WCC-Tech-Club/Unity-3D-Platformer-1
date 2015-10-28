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
        rigidbody.velocity += (Physics.gravity - (rigidbody.velocity * rigidbody.drag)) * Time.deltaTime;
    }
}
