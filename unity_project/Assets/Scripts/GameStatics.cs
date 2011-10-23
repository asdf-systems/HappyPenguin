using UnityEngine;
using System.Collections;
using System;

public static class GameStatics {

	private static string penguinHat;

	private static string getFromLocalStorage(string key, string default_val) {
		try {
			return LocalStorage.ReadUTF8File(key);
		} catch {}
		return default_val;
	}

	private static void loadSavedValues() {
		savePlayerHat(getFromLocalStorage("penguinHat", "Red_Hat"));
	}
	static GameStatics(){
		loadSavedValues();
	}

	public static float Points{
		get;
		set;
	}

	public static void savePlayerHat(string name){
		penguinHat = name;
		LocalStorage.WriteUTF8File("penguinHat", name);
	}

	public static string getPlayerHat() {
		return penguinHat;
	}

	public static GameObject loadPlayerHat(){
		Debug.Log("PenguinHat" + penguinHat);
		UnityEngine.Object resource = Resources.Load("Pux_Cloth/" + penguinHat);
		
		GameObject go = GameObject.Instantiate(resource) as GameObject;
		return go;
	}
}
