using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	Rigidbody2D rb;

	GameObject planet;

	float gravity; //The force of gravity on the ball.
	float rotationSpeed; //How quickly the ball rotates around the planet.

	[HideInInspector]
	public Vector2 velocity;

	void Start()
	{
		if (gameObject.GetComponent<Rigidbody2D> () != null)
			rb = gameObject.GetComponent<Rigidbody2D> ();

		planet = GameObject.FindGameObjectWithTag ("Planet");
		gravity = .01f;
		velocity = Vector2.zero;

		rotationSpeed = .1f;
	}

	void Update()
	{
		//RotateWithMousePosition ();

		//RotateWithInput ();
		//if(!GetComponent<Collider2D>().IsTouching(planet.GetComponent<Collider2D>()))
			//ApplySphericalGravity();

		//RotateAround (planet.transform.position, Vector3.back, .1f);

		//transform.position += new Vector3(velocity.x, velocity.y, 0);
		//rb.AddRelativeForce(velocity, ForceMode2D.Impulse);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Dot") 
		{
			float scoreToAdd = 0f;

			Vector2 velocity = rb.velocity;
			velocity.y *= -1.5f;
			//rb.velocity = velocity;

			scoreToAdd = 100 * Mathf.Abs (GameManager.Instance.gameObject.GetComponent<DotSpawner> ().dotScaleConstraints.y - other.transform.localScale.y)
				* (other.gameObject.GetComponent<Dot> ().speed * 5f);

			GameManager.Instance.Score += scoreToAdd;

			StartCoroutine (GameObject.FindGameObjectWithTag ("GUI").GetComponent<GUI> ().CreateFloatingScoreText ((int)scoreToAdd, other.transform.position));
			GameManager.Instance.activeDots.Remove (other.gameObject);

			Destroy (other.gameObject);
		}
	}

	void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.Equals(planet)) 
		{
			//Bounce (.1f);
		}
	}

	public void Bounce(float bounceForce)
	{
		Vector2 directionToPlanet = (Vector2)planet.transform.position - (Vector2)transform.position;
		directionToPlanet.Normalize ();
		velocity += bounceForce * directionToPlanet;
	}

	void ApplySphericalGravity()
	{
		Vector2 directionToPlanet = (Vector2)planet.transform.position - (Vector2)transform.position;
		directionToPlanet.Normalize ();
		velocity += gravity * directionToPlanet;

		//Quaternion targetRotation = Quaternion.FromToRotation (transform.up, -directionToPlanet);
		//transform.rotation = Quaternion.Euler(0, 0, Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 50).eulerAngles.z);//
		//transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, 50 * Time.deltaTime);
		//transform.rotation = Quaternion.Euler(new Vector3 (0, 0, transform.rotation.eulerAngles.z));

		//Quaternion myRot = transform.rotation;
		//transform.rotation *= Quaternion.Inverse (myRot) * Quaternion.AngleAxis(, Vector3.forward) * myRot;
	}

	void RotateWithInput()
	{
		transform.RotateAround(planet.transform.position, Vector3.forward, -rotationSpeed * Input.GetAxis("Horizontal"));
	}

	//Rotates around the planet based on the mouse location.
	void RotateWithMousePosition()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 dirMouse = mousePos - GameObject.FindGameObjectWithTag("Planet").transform.position;
		float planetMouseAngle = Mathf.Atan2 (dirMouse.x, dirMouse.y) * Mathf.Rad2Deg;

		Vector3 dirBall = transform.position - planet.transform.position;
		float planetBallAngle = Mathf.Atan2 (dirBall.x, dirBall.y) * Mathf.Rad2Deg;

		//Set the rotation values so that they aren't -180 to 180.
		if (planetMouseAngle <= 0)
			planetMouseAngle = 360 - Mathf.Abs (planetMouseAngle);
		if (planetBallAngle <= 0)
			planetBallAngle = 360 - Mathf.Abs (planetBallAngle);

		float angleDifference = Mathf.DeltaAngle(planetBallAngle, planetMouseAngle);

		if (Mathf.Abs (angleDifference) > .1f)
			rotationSpeed = Mathf.Lerp (Mathf.Abs (angleDifference), 0, Time.deltaTime);
		else
			rotationSpeed = Mathf.InverseLerp (0, Mathf.Abs (angleDifference), Time.deltaTime);

		if (angleDifference > .1f)
			transform.RotateAround (planet.transform.position, Vector3.forward, -rotationSpeed);
		if (angleDifference < -.1f)
			transform.RotateAround (planet.transform.position, Vector3.forward, rotationSpeed);
	}
}
