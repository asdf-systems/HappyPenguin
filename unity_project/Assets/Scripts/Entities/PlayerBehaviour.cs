
using System;
using UnityEngine;
using HappyPenguin.Entities;

public sealed class PlayerBehaviour : EntityBehaviour
{
	public float StartLife;
	public float StartPoints;
	public GameObject mesh;
	public GameObject hatPoint;
	public GameObject rightHandPoint;
	public GameObject beltPoint;
	public GameObject amulettPoint;
	
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
		updateCloth();
		
	}
	
	
	public bool IsDead {
		get {return Life < 1;}
	}
	
	public void updateCloth(){
		
		changeHat();
		changeBelt();
		changeAmulett();
		gameObject.animation.PlayQueued("happy");
		gameObject.animation.PlayQueued("show01");
	}
	private void changeHat(){
		
		var obj = GameStatics.loadPlayerHat();
		changeCloth(hatPoint, obj);
		Debug.Log("Hat: " + obj.name);
	}
	
	private void changeBelt(){
		//! NOT IMPLEMENTED YET
	}
	
	private void changeAmulett(){
		//! NOT IMPLEMENTED YET
	}
	
	private void changeCloth(GameObject hingePoint, GameObject newObject){
		
		for (int i = 0; i < hingePoint.transform.GetChildCount(); i++){
			Destroy(hingePoint.transform.GetChild(i).gameObject);
		}
		
		newObject.transform.parent = hingePoint.transform;
		newObject.transform.localPosition = Vector3.zero;
		newObject.transform.localRotation = Quaternion.identity;
	
		
		
	}
	
	
}



