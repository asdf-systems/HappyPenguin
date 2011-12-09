using UnityEngine;
using System.Collections;
using System;
using Pux.Resources;

public static class GameStatics {

	public static string UsernameDefault = "Your Name";
	public static string PuxHatDefault = "Red_Hat";
	public static string PuxSkinDefault = "pux_normal_skin";
	public static string PersonalHighscoreDefault = "0000000";
	
	private static string penguinHat;

	private static bool saveValue(string key, string value){
		try{
			LocalStorage.WriteUTF8File(key, value);
			return true;
		} catch(Exception e){
			EditorDebug.LogWarning(e.Message);
			return false;
		}
	}
	
	private static string loadValue(string key, string default_val){
		try{
				string tmp = LocalStorage.ReadUTF8File(key);
				if(tmp == string.Empty)
					tmp =  default_val;
				return tmp;
		} catch(Exception e){
				EditorDebug.LogWarning(e.Message);
				return default_val;
		}	
	}
	private static void loadSavedValues() {
		savePlayerHat(loadValue("penguinHat", PuxHatDefault));
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
			return loadValue("player_skin", PuxSkinDefault);
		}
		set{
			saveValue("player_skin", value);
		}
	}
	

	
	public static float PersonalHighscore{
		get{
			string tmp = loadValue("personal_highscore", PersonalHighscoreDefault);
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
			return loadValue("player_name", UsernameDefault);
			
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
		EditorDebug.Log("PenguinHat" + penguinHat);
		GameObject go = ResourceManager.CreateInstance<GameObject>("Pux_Cloth/" + penguinHat);
		return go;
	}
}
