using UnityEngine;
using System.Collections;
using System;
using HappyPenguin.Entities;

public class AttackZoneEventArgs : EventArgs{

	public TargetableEntityBehaviour Creature{
		get;
		private set;
	}
	
	public AttackZoneEventArgs(TargetableEntityBehaviour e){
		Creature = e;
	}
}
