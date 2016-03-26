using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUI : MonoBehaviour 
{
	public Text scoreText;
	public Text timeText;

	public GameObject floatingScoreText; //The prefab that floats off of a dot when it's been hit.

	void Update()
	{
		scoreText.text = "Score: " + (int)GameManager.Instance.Score; //Score as an integer.
		timeText.text = "Time: " + Mathf.Round(GameManager.Instance.gameTimeCount * 100f) / 100f; //Time rounded to the hundreths place.
	}

	public IEnumerator CreateFloatingScoreText(int scoreToDisplay, Vector3 position)
	{
		GameObject floatingText = Instantiate (floatingScoreText, Camera.main.WorldToScreenPoint (position), Quaternion.identity) as GameObject;
		floatingText.transform.SetParent (transform);
		floatingText.GetComponent<Text> ().text = "+" + scoreToDisplay;
		for (int i = 0; i < 25; i += 1) 
		{
			Vector3 pos = floatingText.GetComponent<RectTransform>().position;
			pos.y += 1.25f;
			floatingText.GetComponent<RectTransform>().position = pos;
			yield return null;
		}
		Destroy (floatingText, .25f);
	}
}
