using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/*
 * This class Holds all available GameObjects that can be afford in the game Store
 * If something is Buy, it will be copied to the Storage.
 */
public class Store : MonoBehaviour{
	

	public PlayerBehaviour player;
	
	void Start(){
		WardrobeBoxBehaviour[] boxes = gameObject.GetComponentsInChildren<WardrobeBoxBehaviour>() as WardrobeBoxBehaviour[];
		foreach(WardrobeBoxBehaviour box in boxes){
			EditorDebug.Log("Found Object " + box.name);
			box.PlayerClothChanged += OnClothChanged;
		}

	}
	
	private void OnClothChanged(object sender, EventArgs e){
		
		if(player != null)
			player.updateCloth();
	}
	
}

