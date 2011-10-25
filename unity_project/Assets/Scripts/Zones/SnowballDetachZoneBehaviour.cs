using UnityEngine;
using System;
using System.Collections;
using Pux;

public sealed class SnowballDetachZoneBehaviour : MonoBehaviour {	
	void OnTriggerEnter(Collider c) {
		var ball = c.GetComponentInChildren<SnowballBehaviour>();
		ball.InvokeDetachZoneReached();
	}
}
