using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * This class Holds all available GameObjects that can be afford in the game Store
 * If something is Buy, it will be copied to the Storage.
 */
public class Store : MonoBehaviour{
	
	private List<Transform> mStorage;
	
	public Store(){
		
		
	}
	
	public void add(Transform prefab){
		
		mStorage.Add(prefab);
	}
	
	public Transform remove(Transform prefab){
		
		return null;
	}
	
	public GameObject buy(string objectName){
		
		return getComponentByName(objectName);
	}
	
	public int sell(Transform objectName){
		return 0;
		
	}
	
	private GameObject getComponentByName(string name){
		Debug.Log("Search for Component: " + name);
		StoreObject[] gameObjects = GetComponentsInChildren<StoreObject>(true);
		Debug.Log("Store Size: " + gameObjects.Length);
		foreach(StoreObject obj in gameObjects){
			Debug.Log("Found component: " + obj.getName());
			if(obj.getName() == name)
				return obj.getGameObject();
			
		}
		
		return null;
		
	}
	
}

