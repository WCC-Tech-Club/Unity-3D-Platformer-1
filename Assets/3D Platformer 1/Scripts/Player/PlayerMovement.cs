using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public sealed class PlayerMovement : MonoBehaviour
{
    private new Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Roll(float horizontal, float vertical, float forwardOffset)
    {
        Vector3 direction = Quaternion.Euler(0, forwardOffset, 0) * new Vector3(horizontal, 0, vertical).normalized;
        Vector3 torque = new Vector3(-direction.z, 0, direction.x);
    }

    public void Push(float horizontal, float vertical, float forwardOffset)
    {
        Vector3 direction = Quaternion.Euler(0, forwardOffset, 0) * new Vector3(horizontal, 0, vertical).normalized;
    }
}
