using UnityEngine;
using System.Collections;
using System;
using HappyPenguin.Entities;

public class AttackZoneEventArgs : EventArgs{

	public TargetableEntityBehaviour enemy{
		get;
		private set;
	}
	
	public AttackZoneEventArgs(TargetableEntityBehaviour e){
		enemy = e;
	}
}
