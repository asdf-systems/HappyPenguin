using UnityEngine;
using System.Collections;

public class penguin_animation : MonoBehaviour {

	private int state;
	
	// Use this for initialization
	void Start () {
		state = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(!animation.isPlaying && state == 0){
			animation.Play("idle");
			state = 1;
		} else if(!animation.isPlaying && state == 1){
			animation.Play("happy");
			state = 0;
		}
	}
}
