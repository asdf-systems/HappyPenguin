using UnityEngine;
using System.Collections;
using HappyPenguin.Entities;
using System;

public class AttackZoneBehaviour : MonoBehaviour {

	public event EventHandler<AttackZoneEventArgs> EnemyEnteredAttackZone;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider c){
		
		TargetableEntityBehaviour creature = c.GetComponent<TargetableEntityBehaviour>();
		InvokeEnemyEnteredAttackZone(creature);
	}
	
	private void InvokeEnemyEnteredAttackZone(TargetableEntityBehaviour creature){
		var handler = EnemyEnteredAttackZone;
		if (handler == null) {
				return;
		}
			
		var e = new AttackZoneEventArgs(creature);
		EnemyEnteredAttackZone(this, e);
	}
	
	
}
