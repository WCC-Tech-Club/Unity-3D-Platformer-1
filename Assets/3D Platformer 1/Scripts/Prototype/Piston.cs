using UnityEngine;

public class Piston : MonoBehaviour {

    [SerializeField]
    private Rigidbody pistonHead;
    [SerializeField]
    private float force;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            pistonHead.AddRelativeForce(Vector3.up * force, ForceMode.Impulse);
        }
    }
}
