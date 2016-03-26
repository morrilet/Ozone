using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	[HideInInspector]
	public List<GameObject> activeDots;
	DotSpawner dotSpawner;

	int dotsToSpawn;

	public float gameTime; //Time limit for the game.
	[HideInInspector]
	public float gameTimeCount;
	[HideInInspector]
	public float gameTimeModifier; //Helps slow down the timer if the bounces are low.

	float score;
	public float Score
	{
		get { return score; }
		set { score = value; }
	}

	public static GameManager Instance { get; private set; }

	//Singleton stuff.
	void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy (gameObject);

		Instance = this;

		DontDestroyOnLoad (Instance);
	}

	void Start()
	{

		activeDots = new List<GameObject> ();
		dotSpawner = GetComponent<DotSpawner> ();

		gameTimeCount = gameTime;
	}

	void Update()
	{
		//Start a new wave if applicable.
		if (activeDots.Count == 0)
			dotSpawner.SetUpDots ();

		if (gameTimeCount <= 0) 
		{
			StopGame ();
			gameTimeCount = 0;
		}
		else
			gameTimeCount -= Time.deltaTime * gameTimeModifier;

		if (Input.GetKeyDown (KeyCode.R))
			StartGame ();
	}

	//Starts the game over.
	void StartGame()
	{
		score = 0;
		gameTimeCount = gameTime;
		dotSpawner.ClearDots ();
		dotSpawner.SetUpDots ();
	}

	//Stops the game and shows final score and menu.
	void StopGame()
	{
		dotSpawner.ClearDots ();
	}
}
