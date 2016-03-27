using UnityEngine;
using System.Collections;

public class FauxGravityBody : MonoBehaviour 
{
	public FauxGravityAttractor attractor;

	private Transform myTransform;
	private Rigidbody2D myRigidbody2D;

	void Start () 
	{
		myRigidbody2D = GetComponent<Rigidbody2D> ();
		myTransform = transform;

		myRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
		myRigidbody2D.gravityScale = 0;
	}

	void Update () 
	{
		attractor.Attract (myTransform);
	}
}
