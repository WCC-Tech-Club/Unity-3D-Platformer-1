using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class ConveyorSegment : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }

    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
}
