using System;
using UnityEngine;
using HappyPenguin.Entities;

public sealed class PlayerBehaviour : EntityBehaviour
{
	public float startLife;
	public float startPoints;
	
	public float life{
		get;
		set;
	}
	
	public float points{
		get;
		set;
	}
	

	public PlayerBehaviour() {
	}
	
	void Start(){
		life = startLife;
		points = startPoints;
	}
	
	public bool isDead(){
		return ( life <= 0 ) ;
	}
}


