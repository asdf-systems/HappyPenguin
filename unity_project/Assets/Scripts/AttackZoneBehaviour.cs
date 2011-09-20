using UnityEngine;
using System.Collections;
using HappyPenguin.Entities;
using System;

public class AttackZoneBehaviour : MonoBehaviour {

	public event EventHandler<AttackZoneEventArgs> AttackZoneEntered;
	
	// Use this for initialization
	void Awake () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider c){
		//Debug.Log("Trigger AttackZone");
		TargetableEntityBehaviour creature = c.GetComponent<TargetableEntityBehaviour>();
		InvokeAttackZoneEntered(creature);
	}
	
	private void InvokeAttackZoneEntered(TargetableEntityBehaviour creature){
		//Debug.Log("Invoke AttackZone");
		var handler = AttackZoneEntered;
		if (handler == null) {
				return;
		}
		//Debug.Log("Invoke AttackZone");	
		var e = new AttackZoneEventArgs(creature);
		AttackZoneEntered(this, e);
	}
}
