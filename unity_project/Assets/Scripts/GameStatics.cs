using UnityEngine;
using System.Collections;
using System;
using Pux.Resources;

public static class GameStatics {

	private static string penguinHat;

	private static bool saveValue(string key, string value){
		try{
			LocalStorage.WriteUTF8File(key, value);
			return true;
		} catch(Exception e){
			Debug.LogWarning(e.Message);
			return false;
		}
	}
	
	private static string loadValue(string key, string default_val){
		try{
				string tmp = LocalStorage.ReadUTF8File(key);
				return tmp;
		} catch(Exception e){
				Debug.LogWarning(e.Message);
				return default_val;
		}	
	}
	private static void loadSavedValues() {
		savePlayerHat(loadValue("penguinHat", "Red_Hat"));
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
			return loadValue("player_skin", "pux_normal_skin");
		}
		set{
			saveValue("player_skin", value);
		}
	}
	

	
	public static float PersonalHighscore{
		get{
			string tmp = loadValue("personal_highscore", "0000000");
			if(tmp == string.Empty)
				return 0.0f;
			return float.Parse(tmp);
		}
		set{
			string sPoint = FormatPoints(value);
			saveValue("personal_highscore", sPoint);
		}
	}
	
	public static string FormatPoints(float points){
		int iPoints = (int)points;
		string sPoints = iPoints.ToString();
		while(sPoints.Length < 7)
			sPoints = "0" + sPoints;
		return sPoints;
		
	}
	
	public static string Username{
		get{
			return loadValue("player_name", "YOUR  NAME");
			
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
		GameObject go = ResourceManager.CreateInstance<GameObject>("Pux_Cloth/" + penguinHat);
		return go;
	}
}
