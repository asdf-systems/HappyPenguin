using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * This class is for holding all Elements the Player has already buy, but not equiped.
 * If some stuff from the Store is put on the car - the storage will create the instance of the gameObject and 
 * add it to the Car 
 */
public class Storage : MonoBehaviour{
	
	public void add(string objectName){
		
	}
	
	public Transform remove(string objectName){
		
		return null;
	}
	
	private int find(string name){
		return 0;	
	}
	
}

