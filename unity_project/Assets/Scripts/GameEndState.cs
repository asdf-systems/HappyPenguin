using UnityEngine;
using System.Collections;

public class GameEndState : MonoBehaviour {

	private float time;
	// Use this for initialization
	void Start () {
		time = 0.0f;
	}

	// Update is called once per frame
	void Update () {

		time+= Time.deltaTime;
		//Debug.Log("Update GameEndState, Time: " + time);
		if(time > 10){
			Application.LoadLevel(0);
		}
	}

	public addEntry(string name, int points) {
		StartCoroutine(
			HighscoreServer.AddEntry(name, points)
			);
	}
}
