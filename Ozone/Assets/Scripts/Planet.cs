using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour 
{
	Collider2D coll;
	Rigidbody2D rb;

	public float maxBounceForce;
	public float minBounceForce;

	GameObject ball;

	void Start()
	{
		if (gameObject.GetComponent<Collider2D> () != null)
			coll = gameObject.GetComponent<Collider2D> ();
		if (gameObject.GetComponent<Rigidbody2D> () != null)
			rb = gameObject.GetComponent<Rigidbody2D> ();

		ball = GameObject.FindGameObjectWithTag ("Ball");
	}

	void Update()
	{
		LookAtMouse ();
	}

	void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.Equals (ball.gameObject)) 
		{
			SetGameTimeModifier ();
			BounceBall ();
		}
	}
		
	void LookAtMouse()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.rotation = Quaternion.LookRotation (Vector3.forward, mousePos - transform.position);
	}

	//Applies a bounce force to the ball.
	void BounceBall()
	{
		if (ball.GetComponent<Rigidbody2D> () != null) 
		{
			ball.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, GetBounceForce ()), ForceMode2D.Impulse);
		}
	}

	//Gets the force of the bounce based on the rotation of the planet.
	float GetBounceForce()
	{
		float bounceForce = 0; //Force of the bounce from min to max (0-1).
		float rotation = transform.eulerAngles.z;

		if (rotation <= 180) 
		{
			bounceForce = Mathf.Abs ((rotation / 180) - 1);
		} 
		else
		{
			bounceForce = Mathf.Abs (((rotation - 180) / 180));
		}

		return Mathf.Lerp (minBounceForce, maxBounceForce, bounceForce);
	}

	//Sets the modifier to use for the GameManagers timer speed.
	void SetGameTimeModifier()
	{
		float gameTimeModifier = 1;
		float rotation = transform.eulerAngles.z;

		if (rotation <= 180) 
		{
			gameTimeModifier = Mathf.Abs ((rotation / 180) - 1);
		} 
		else
		{
			gameTimeModifier = Mathf.Abs (((rotation - 180) / 180));
		}

		gameTimeModifier = Mathf.Clamp (gameTimeModifier, .01f, 1f);
		GameManager.Instance.gameTimeModifier = gameTimeModifier;
	}
}
