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
	
	public static string PlayerSkin{
		get{
			return loadValue("player_skin");
		}
		set{
			saveValue("player_skin", value);
		}
	}
	
	private static bool saveValue(string key, string value){
		try{
			LocalStorage.WriteUTF8File(key, value);
			return true;
		} catch(Exception e){
			Debug.LogWarning(e.Message);
			return false;
		}
	}
	
	private static string loadValue(string key){
		try{
				string tmp = LocalStorage.ReadUTF8File(key);
				return tmp;
		} catch(Exception e){
				Debug.LogWarning(e.Message);
				return string.Empty;
		}	
	}
	
	public static float PersonalHighscore{
		get{
			string tmp = loadValue("personal_highscore");
			if(tmp == string.Empty)
				return 0.0f;
			return float.Parse(tmp);
		}
		set{
			string sPoint = FormatPoints(value);
			saveValue("personal_highscore", sPoint);
		}
	}
	
	private static string FormatPoints(float points){
		int iPoints = (int)points;
		string sPoints = iPoints.ToString();
		while(sPoints.Length < 7)
			sPoints = "0" + sPoints;
		return sPoints;
		
	}
	
	public static string username{
		get{
			return loadValue("player_name");
			
		}
		set{
			saveValue("player_name", value);
		}
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
