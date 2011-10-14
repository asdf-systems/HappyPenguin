using UnityEngine;
using System;
using System.Collections;
using HappyPenguin;

public class SnowballDropZoneBehaviour : MonoBehaviour {	
	void OnTriggerEnter(Collider c) {
		var ball = c.GetComponent<SnowballBehaviour>();
		ball.InvokeDetachZoneReached();
	}
}
