using UnityEngine;
using System.Collections;

public class Dot : MonoBehaviour 
{
	CircleCollider2D coll;
	Rigidbody2D rb;

	public float speed;

	[HideInInspector]
	public float orbitRadius; //Radius of the orbit.
	[HideInInspector]
	public float dotExtent; //Radius of the dot.
	[HideInInspector]
	public Vector2 orbitRange; //The range of space around the planet occupied by the dots orbit.

	GameObject planet;
	float rotation;

	void Start()
	{
		if (gameObject.GetComponent<CircleCollider2D> () != null)
			coll = gameObject.GetComponent<CircleCollider2D> ();
		if (gameObject.GetComponent<Rigidbody2D> () != null)
			rb = gameObject.GetComponent<Rigidbody2D> ();

		planet = GameObject.FindGameObjectWithTag("Planet");
		rotation = 0f;

		dotExtent = gameObject.GetComponent<SpriteRenderer> ().bounds.extents.x;
		//Debug.Log (dotExtent + ", " + transform.lossyScale.x / 1.6f);
	}

	void Update()
	{
		transform.RotateAround (planet.transform.position, Vector3.back, speed);
	}
}
