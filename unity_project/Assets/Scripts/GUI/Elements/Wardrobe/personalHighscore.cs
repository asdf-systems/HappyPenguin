using UnityEngine;
using System.Collections;
using System;

public class personalHighscore : UIElementBehaviour<GUIStatics> {
	
	private string points;
	public int targetTextSize;
	
	public int textOffsetX;
	public int textOffsetY;
	
	public GUIStyle textStyle;
	
	protected override void showElements(){
		textStyle.fontSize = targetTextSize;
		GeneralScreenGUI.Box(guiStatics, new Rect(positionX,positionY,350,350), "", inactiveStyle);
		
		GeneralScreenGUI.Label(guiStatics, new Rect(positionX+textOffsetX,positionY+textOffsetY,350,350), "Personal Score", textStyle);
		textStyle.fontSize = targetTextSize;
		GeneralScreenGUI.Label(guiStatics, new Rect(positionX+textOffsetX,positionY+textOffsetY+targetTextSize,300,300), points, textStyle);
	}

	void Start(){
		loadPoints();
	}
	
	private void loadPoints(){
		try{
			points = LocalStorage.ReadUTF8File("personal_highScore");
		} catch(Exception ex){
			Debug.Log(ex.Message);
			points = "0000000";
		}
	}
	
	protected override void hit(){
		
	}

	
}
