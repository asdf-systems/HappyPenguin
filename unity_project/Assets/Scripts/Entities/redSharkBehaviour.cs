using UnityEngine;
using System.Collections;

public class redSharkBehaviour : UVMoveBehaviour {

	// Use this for initialization
	protected override void StartOverride (){
		base.StartOverride ();
		newUvs = new Rect(0,-256,0,0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
