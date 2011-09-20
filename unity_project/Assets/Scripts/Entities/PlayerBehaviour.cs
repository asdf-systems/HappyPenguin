using System;
using UnityEngine;
using HappyPenguin.Entities;

public sealed class PlayerBehaviour : EntityBehaviour
{
	public float StartLife;
	public float StartPoints;
	
	public float Life{
		get;
		set;
	}
	
	public float Points{
		get;
		set;
	}
	
	void Start(){
		Life = StartLife;
		Points = StartPoints;
	}
	
	public bool IsDead {
		get {return Life == 0;}
	}
}


