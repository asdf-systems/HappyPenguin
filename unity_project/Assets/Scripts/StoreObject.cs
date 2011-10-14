using UnityEngine;
using System.Collections;

public class StoreObject : MonoBehaviour {

	public int price;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Awake(){
		disableObject();
		
	}
	
	public GameObject getGameObject(){
		return gameObject;
		
	}
	
	public string getName(){
		return name;
		
	}
	
	public void disableObject(){
		this.gameObject.active = false;
		 foreach (Transform child in transform) {
            child.gameObject.active = false;
			
        }
		
	}
	
	public void enableObject(){
		this.gameObject.active = true;
		 foreach (Transform child in transform) {
            child.gameObject.active = true;
			
        }
		
	}
	
	
}
