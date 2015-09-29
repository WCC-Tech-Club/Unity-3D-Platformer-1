using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public sealed class PlayerController : MonoBehaviour
{
	private Rigidbody rb;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{

	}
}
