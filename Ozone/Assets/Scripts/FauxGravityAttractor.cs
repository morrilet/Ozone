using UnityEngine;
using System.Collections;

public class FauxGravityAttractor : MonoBehaviour {

	float gravity = -10.1f;

	Vector3 gravityUp;

	void Update()
	{
		RotateWithInput ();
	}

	public void Attract(Transform body)
	{
		gravityUp = (body.position - transform.position).normalized;
		Vector3 bodyUp = body.up;

		body.GetComponent<Rigidbody2D> ().AddForce ((Vector2)gravityUp * gravity);

		if (body.GetComponent<Collider2D> ().IsTouching (GetComponent<Collider2D> ()))
			body.GetComponent<Rigidbody2D> ().AddForce ((Vector2)gravityUp * 10f, ForceMode2D.Impulse);

		Quaternion targetRotation = Quaternion.FromToRotation (bodyUp, gravityUp) * body.rotation;
		body.rotation = Quaternion.Slerp (body.rotation, targetRotation, 50 * Time.deltaTime);
		body.RotateAround(transform.position, Vector3.forward, -1 * Input.GetAxis("Horizontal"));
	}

	void RotateWithInput()
	{
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine (transform.position, gravityUp);
	}
}
