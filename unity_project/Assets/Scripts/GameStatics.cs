using UnityEngine;
using System.Collections;

public static class GameStatics {

	private static string penguinHat;
	
	static GameStatics(){
		penguinHat = "Red_Hat";
	}
	public static float Points{
		get; 
		set;
	}
	
	public static void savePlayerHat(string name){
		penguinHat = name;
	}	
	
	public static GameObject loadPlayerHat(){
		Object resource = Resources.Load("Pux_Cloth/" + penguinHat);
		Vector3 position = new Vector3(0,0,0);
		GameObject go = GameObject.Instantiate(resource) as GameObject;
		return go;	
	}
	
	
	
}
