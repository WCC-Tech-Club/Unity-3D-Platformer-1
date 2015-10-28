using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public sealed class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float maxAngularVelocity;           // Max rotational speed the ball can have.
    [SerializeField]
    private float rollTorque;                   // Amount of force applied the ball to roll.
    [SerializeField]
    private float midAirControl;                // Amount of force applied to the ball while falling/mid air.
    [SerializeField]
    private float groundCheckDistance = 0.1f;   // Distance to check for ground for checking if mid air control is aloud.
    [SerializeField]
    private float groundCheckRadius = 0.5f;     // Radius of the sphere to cast to check for ground.
    [SerializeField]
    private LayerMask groundLyaers = -1;        // Layers that can be considered ground.

    private new Rigidbody rigidbody;            // Reference to local Rigidbody.

    private float originalAngularDrag;          // Reference to the original angular drag.

    void Awake()
    {
        // Reference local components.
        rigidbody = GetComponent<Rigidbody>();

        originalAngularDrag = rigidbody.angularDrag;
    }

    public void Roll(float horizontal, float vertical, float forwardOffset, bool breaking)
    {
        // Set the rigidbody max angular velocity to that in the inspector.
        rigidbody.maxAngularVelocity = maxAngularVelocity;

        // Set the angular drag of the rigidbody based on if we are breaking or not.
        rigidbody.angularDrag = breaking ? float.MaxValue : originalAngularDrag;

        // Calculate the direction of movement.
        Vector3 direction = Quaternion.Euler(0, forwardOffset, 0) * new Vector3(horizontal, 0, vertical).normalized;

        // Ignore this, not needed in use but needed to call the sphere cast
        RaycastHit hit;

        // If we are not grounded...
        if (!Physics.SphereCast(rigidbody.position, groundCheckRadius, Vector3.down, out hit, groundCheckDistance, groundLyaers, QueryTriggerInteraction.Ignore))
        {
            // ... apply a uniform force to the rigidbody.
            rigidbody.AddForce(direction * midAirControl);
        }

        // Convert the direction into angular torque and apply the modifier.
        Vector3 torque = new Vector3(direction.z, 0, -direction.x) * rollTorque;

        // Apply the torque to the rigidbody.
        rigidbody.AddTorque(torque);
    }
}
