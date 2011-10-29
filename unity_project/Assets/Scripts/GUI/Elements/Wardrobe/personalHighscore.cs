using UnityEngine;
using System.Collections;
using System;

public class personalHighscore : UIElementBehaviour<GUIStatics> {
	
	private string points;
	public int targetTextSize;
	private string username = "(your...)";
	
	public int textOffsetX;
	public int textOffsetY;
	
	public GUIStyle textStyle;
	
	protected override void showElements(){
		textStyle.fontSize = targetTextSize;
		GeneralScreenGUI.Box(guiStatics, new Rect(positionX,positionY,350,350), "", inactiveStyle);
		string oldname = username;
		username = GeneralScreenGUI.TextField(guiStatics, new Rect(positionX+textOffsetX,positionY+textOffsetY,350,350), username,10, textStyle);
		textStyle.fontSize = targetTextSize;
		if(username != oldname)	
			saveUsername();
		GeneralScreenGUI.Label(guiStatics, new Rect(positionX+textOffsetX+170,positionY+textOffsetY,350,350), "Score", textStyle);
		textStyle.fontSize = targetTextSize;
		GeneralScreenGUI.Label(guiStatics, new Rect(positionX+textOffsetX,positionY+textOffsetY+targetTextSize,300,300), points, textStyle);
		
	}

	void Start(){
		loadPoints();
		loadName();
	}

	
	private void saveUsername(){
	
		try{
		      LocalStorage.WriteUTF8File("player_name", username); 
		} catch(Exception e){
			Debug.LogWarning(e.Message);
		}
	
			
	}
	private void loadPoints(){
		try{
			points = LocalStorage.ReadUTF8File("personal_highScore");
		} catch(Exception e){
			Debug.LogWarning(e.Message);
			points = "0000000";
		}
	}

	
	private void loadName(){
		try{
			username = LocalStorage.ReadUTF8File("player_name");
		} catch(Exception e){
			Debug.LogWarning(e.Message);
			username = "Type name";
		}
	}
	protected override void hit(){
		
	}

	
}