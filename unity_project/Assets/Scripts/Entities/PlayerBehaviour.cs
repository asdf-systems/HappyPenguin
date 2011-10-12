using System;
using UnityEngine;
using HappyPenguin.Entities;

public sealed class PlayerBehaviour : EntityBehaviour
{
	public float StartLife;
	public float StartPoints;
	public GameObject mesh;
	public GameObject headPoint;
	
	public override GameObject gameObject{
		get{
			return mesh;
		}
	}
	
	public float Life {
		get;
		set;
	}
	
	public float Points {
		get;
		set;
	}
	
	public static float FinalPoints {
		get;
		set;
	}
	
	protected override void AwakeOverride() {
		base.AwakeOverride();
		Life = StartLife;
		Points = StartPoints;
	}
	
	public bool IsDead {
		get {return Life < 1;}
	}
}


