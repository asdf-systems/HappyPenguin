using UnityEngine;
using System;
using System.Collections;
using HappyPenguin;

public sealed class SnowballDetachZoneBehaviour : MonoBehaviour {	
	void OnTriggerEnter(Collider c) {
		var ball = c.GetComponentInChildren<SnowballBehaviour>();
		ball.InvokeDetachZoneReached();
	}
}
