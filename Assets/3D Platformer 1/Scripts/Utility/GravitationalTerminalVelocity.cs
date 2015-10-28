using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravitationalTerminalVelocity : MonoBehaviour
{
    private new Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        rigidbody.useGravity = false;
    }

    void OnDisable()
    {
        rigidbody.useGravity = true;
    }

    void FixedUpdate()
    {
        Vector3 velocity = rigidbody.velocity;

        float fg = Physics.gravity.y * rigidbody.mass;
        float fd = (velocity.y < 0 ? 0.5f : velocity.y > 0 ? -0.5f : 0) * rigidbody.drag * velocity.y * velocity.y;
        velocity.y += ((fg + fd) / rigidbody.mass) * Time.deltaTime;

        rigidbody.velocity = velocity;
    }
}
