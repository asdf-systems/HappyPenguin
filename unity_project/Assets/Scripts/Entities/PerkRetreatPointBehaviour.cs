using UnityEngine;
using System.Collections;
using HappyPenguin.Entities;
using System;

public class PerkRetreatPointBehaviour : MonoBehaviour {

	public event EventHandler<AttackZoneEventArgs> PerkRetreatPointReached;
	
	// Use this for initialization
	void Awake () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider c){
		TargetableEntityBehaviour perk = c.GetComponent<TargetableEntityBehaviour>();
		InvokePerkRetreatPointReached(perk);
	}
	
	private void InvokePerkRetreatPointReached(TargetableEntityBehaviour perk){
		var handler = PerkRetreatPointReached;
		if (handler == null) {
				return;
		}
		var e = new AttackZoneEventArgs(perk);
		PerkRetreatPointReached(this, e);
	}
}
