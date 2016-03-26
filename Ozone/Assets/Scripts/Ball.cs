using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	Rigidbody2D rb;

	void Start()
	{
		if (gameObject.GetComponent<Rigidbody2D> () != null)
			rb = gameObject.GetComponent<Rigidbody2D> ();
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Dot") 
		{
			float scoreToAdd = 0f;

			Vector2 velocity = rb.velocity;
			velocity.y *= -1.5f;
			rb.velocity = velocity;

			scoreToAdd = 100 * Mathf.Abs (GameManager.Instance.gameObject.GetComponent<DotSpawner> ().dotScaleConstraints.y - other.transform.localScale.y)
				* (other.gameObject.GetComponent<Dot> ().speed * 5f);

			GameManager.Instance.Score += scoreToAdd;

			StartCoroutine (GameObject.FindGameObjectWithTag ("GUI").GetComponent<GUI> ().CreateFloatingScoreText ((int)scoreToAdd, other.transform.position));
			GameManager.Instance.activeDots.Remove (other.gameObject);

			Destroy (other.gameObject);
		}
	}
}
