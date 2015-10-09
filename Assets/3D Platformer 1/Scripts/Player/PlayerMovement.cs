using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public sealed class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float maxAngularVelocity;           // Max rotational speed the ball can have.
    [SerializeField]
    private float rollTorque;                   // Amount of force applied the ball to roll.
    [SerializeField]
    private float jumpForce;                    // Amount of upwards force applied to the ball to jump.

    private new Rigidbody rigidbody;            // Reference to local Rigidbody.

    void Awake()
    {
        // Reference local components.
        rigidbody = GetComponent<Rigidbody>();

        // Set the rigidbody max angular velocity to ours.
        rigidbody.maxAngularVelocity = maxAngularVelocity;
    }

    public void Roll(float horizontal, float vertical, float forwardOffset)
    {
        // Calculate the direction of movement.
        Vector3 direction = Quaternion.Euler(0, forwardOffset, 0) * new Vector3(horizontal, 0, vertical).normalized;

        // Convert the direction into angular torque and apply the modifier.
        Vector3 torque = new Vector3(direction.z, 0, -direction.x) * rollTorque;

        // Apply the torque to the rigidbody.
        rigidbody.AddTorque(torque);
    }

    public void Jump()
    {
        // If a cast ray hits the ground...
        if (Physics.Raycast(rigidbody.position, Vector3.down, 1))
        {
            // ... add the jump force to the rigidbody as an impulse.
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
