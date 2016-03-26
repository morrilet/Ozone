using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DotSpawner : MonoBehaviour 
{
	public GameObject dotPrefab;

	public int dotsToSpawn;

	//Constraint values are (min, max) and determine the values to use when setting up the dots.
	public Vector2 dotRadiusConstraints;
	public Vector2 dotSpeedConstraints;
	public Vector2 dotScaleConstraints;

	[System.Serializable]
	public class OrbitRange
	{
		public Vector2 range;
	}
	List<OrbitRange> usedOrbitRanges;

	void Start()
	{
		usedOrbitRanges = new List<OrbitRange> ();
	}

	public void SetUpDots()
	{
		for (int i = 0; i < 10; i++) 
		{
			SpawnDot ();
		}
		//ChooseOrbitRadius ();
	}

	//Chooses an orbit radius that avoids collisions with other dots. WIP.
	Vector2 ChooseOrbitRadius()
	{
		usedOrbitRanges = usedOrbitRanges.OrderBy (x => x.range.y).ToList ();
		foreach (OrbitRange range in usedOrbitRanges)
			Debug.Log (range.range.x);
		return Vector2.zero;
	}

	GameObject SpawnDot()
	{
		Vector2 dotDirection = new Vector2 (Random.Range (-100, 100), Random.Range (-100, 100));
		float dotRadius = Random.Range (dotRadiusConstraints.x, dotRadiusConstraints.y);
		Vector3 dotPosition = new Vector3(dotDirection.normalized.x * dotRadius, dotDirection.normalized.y * dotRadius, 0);

		float dotSpeed = Random.Range (dotSpeedConstraints.x, dotSpeedConstraints.y);

		float dotScaleValue = Random.Range (dotScaleConstraints.x, dotScaleConstraints.y);
		Vector3 dotScale = new Vector3 (dotScaleValue, dotScaleValue, 1);

		GameObject newDot = Instantiate (dotPrefab, GameObject.FindGameObjectWithTag("Planet").transform.position, Quaternion.identity) as GameObject;
		Dot newDotScript = newDot.GetComponent<Dot> ();

		newDot.transform.position += dotPosition;
		newDot.transform.localScale = dotScale;
		newDotScript.speed = dotSpeed;
		newDotScript.orbitRadius = dotRadius;
		newDotScript.orbitRange = new Vector2 (newDotScript.orbitRadius - newDot.transform.lossyScale.x/1.6f, newDotScript.orbitRadius + newDot.transform.lossyScale.x/1.6f);

		GameManager.Instance.activeDots.Add (newDot.gameObject);

		OrbitRange range = new OrbitRange();
		range.range = new Vector2 (newDotScript.orbitRange.x, newDotScript.orbitRange.y);
		usedOrbitRanges.Add (range);

		//Debug.Log (newDotScript.orbitRadius + " " + newDot.transform.lossyScale.x + " " + newDotScript.orbitRange);
		return newDot.gameObject;
	}

	//Clears all active dots.
	public void ClearDots()
	{
		foreach (GameObject dot in GameManager.Instance.activeDots)
			Destroy (dot.gameObject);
		GameManager.Instance.activeDots.RemoveAll ((o)=> o == null);
	}
}
