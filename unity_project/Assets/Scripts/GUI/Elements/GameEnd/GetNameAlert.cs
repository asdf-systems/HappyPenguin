using UnityEngine;
using System.Collections;
using System;

public class GetNameAlert : AlertTextPanel {

	private string username;
	//public bool showText{get;set;}
	
	private string highscoreText = "New Highscore!! Enter";
	
	
	/*protected override void showElements(){
		if(showText){
			
			GeneralScreenGUI.Box(guiStatics, new Rect(positionX,positionY,800,800), "", inactiveStyle);
			
			textStyle.fontSize = targetFontSize;
			GeneralScreenGUI.Label(guiStatics, new Rect(positionX+textOffsetX,positionY+textOffsetY,350,350), highscoreText, textStyle);
			
			textStyle.fontSize = targetFontSize;
			string oldname = username;
			username = GeneralScreenGUI.TextField(guiStatics, new Rect(positionX+textOffsetX,positionY+textOffsetY+targetFontSize,300,300), username,10, textStyle);
			if(username != oldname){
				GameStatics.Username = username;
				highscoreText = "New Highscore!! ";
			}
			
		}
	}*/
	
	void Start(){
		StartOverride();
	}
	
	protected override void StartOverride(){
		username = GameStatics.Username;
		Text = username;
	}
	
	public new void ShowText(string value){
		//initTimer();
		textShow = true;
		Text = value;
		//timer1.StartTimer();
	}
	
	/*void Start(){
		showText = false;
		username = GameStatics.Username;
		if(username == string.Empty)
				username = "Your name";
	}*/
	

	
	
	
	
}
