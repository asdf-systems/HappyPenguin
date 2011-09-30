using UnityEngine;
using System.Collections;

public class GameStaticsBehaviour : MonoBehaviour {

	static public float Points{
		get; 
		set;
	}
	void Awake(){
		 DontDestroyOnLoad(transform.gameObject);
	}
	// Use this for initialization
	void Start () {
		//points = 0 ;
	}
	
	
}
