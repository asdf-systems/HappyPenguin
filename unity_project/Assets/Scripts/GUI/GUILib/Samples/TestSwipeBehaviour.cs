using UnityEngine;
using System.Collections;

public class TestSwipeBehaviour : InteractionBehaviour {

	
	
	public override void Swipe(MouseEventArgs mouse){
		Debug.Log("Swipe Element: " + gameObject.name);
	}
}
