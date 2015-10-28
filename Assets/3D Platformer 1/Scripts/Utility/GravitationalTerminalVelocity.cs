using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravitationalTerminalVelocity : MonoBehaviour
{
    [SerializeField, Range(0.1f, 10f)]
    private float massMultiplier = 1;

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

        float mass = rigidbody.mass * massMultiplier;

        float forceGravity = Physics.gravity.y * mass;
        float forceDrag = (velocity.y < 0 ? 0.5f : velocity.y > 0 ? -0.5f : 0) * rigidbody.drag * velocity.y * velocity.y;
        velocity.y += ((forceGravity + forceDrag) / mass) * Time.deltaTime;

        rigidbody.velocity = velocity;
    }
}
