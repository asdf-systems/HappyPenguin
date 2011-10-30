using UnityEngine;
using System.Collections;
using System;

public class personalHighscore : UIElementBehaviour<GUIStatics> {
	
	private string points;
	public int targetTextSize;
	private string username;
	
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
			GameStatics.username = username;
		GeneralScreenGUI.Label(guiStatics, new Rect(positionX+textOffsetX+170,positionY+textOffsetY,350,350), "Score", textStyle);
		textStyle.fontSize = targetTextSize;
		GeneralScreenGUI.Label(guiStatics, new Rect(positionX+textOffsetX,positionY+textOffsetY+targetTextSize,300,300), points, textStyle);
		
	}

	void Start(){
		loadPoints();
		username = GameStatics.username;
		if(username == string.Empty)
			username = "Your name";
	}

	
	
	private void loadPoints(){
		try{
			points = LocalStorage.ReadUTF8File("personal_highScore");
		} catch(Exception e){
			Debug.LogWarning(e.Message);
			points = "0000000";
		}
	}

	
	
	protected override void hit(){
		
	}

	
}
