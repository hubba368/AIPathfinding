using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{

	public float moveSpeed = 6;

	Rigidbody myRigidbody;

	Vector3 velocity;

	void Start ()
    {
		myRigidbody = GetComponent<Rigidbody> ();
	}

	void Update ()
    {

	}

	void FixedUpdate()
    {
		myRigidbody.MovePosition (myRigidbody.position + velocity * Time.fixedDeltaTime);
	}
}