using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverState : MonoBehaviour {

	/// <summary>
	/// The static instance of the Singleton for external access
	/// </summary>
	public static GameoverState instance = null;

	/// <summary>
	/// Enforce Singleton properties
	/// </summary>
	void Awake()
	{
		//Check if instance already exists and set it to this if not
		if (instance == null)
		{
			instance = this;
		}

		//Enforce the unicity of the Singleton
		else if (instance != this)
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	[SerializeField]
	PlayerGroupModel playerGroup;

	public PlayerGroupModel PlayerGroup {
		get {
			return playerGroup;
		}
		set {
			playerGroup = value;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
