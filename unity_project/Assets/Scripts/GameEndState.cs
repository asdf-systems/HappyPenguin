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
		if(time > 100){
			Application.LoadLevel(0);
		}
	}
}
