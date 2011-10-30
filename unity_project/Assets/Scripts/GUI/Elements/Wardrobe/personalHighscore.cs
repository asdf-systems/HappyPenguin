using UnityEngine;
using System.Collections;
using System;

public class personalHighscore : UIElementBehaviour<GUIStatics> {
	
	private string points;
	public int targetTextSize;
<<<<<<< HEAD
=======
	private string username;
>>>>>>> origin/feature/wardrobeCleanup
	
	public int textOffsetX;
	public int textOffsetY;
	
	public GUIStyle textStyle;
	
	protected override void showElements(){
		textStyle.fontSize = targetTextSize;
		GeneralScreenGUI.Box(guiStatics, new Rect(positionX,positionY,350,350), "", inactiveStyle);
<<<<<<< HEAD
		
		GeneralScreenGUI.Label(guiStatics, new Rect(positionX+textOffsetX,positionY+textOffsetY,350,350), "Personal Score", textStyle);
		textStyle.fontSize = targetTextSize;
		GeneralScreenGUI.Label(guiStatics, new Rect(positionX+textOffsetX,positionY+textOffsetY+targetTextSize,300,300), points, textStyle);
=======
		string oldname = username;
		username = GeneralScreenGUI.TextField(guiStatics, new Rect(positionX+textOffsetX,positionY+textOffsetY,350,350), username,10, textStyle);
		textStyle.fontSize = targetTextSize;
		if(username != oldname)	
			GameStatics.username = username;
		GeneralScreenGUI.Label(guiStatics, new Rect(positionX+textOffsetX+170,positionY+textOffsetY,350,350), "Score", textStyle);
		textStyle.fontSize = targetTextSize;
		GeneralScreenGUI.Label(guiStatics, new Rect(positionX+textOffsetX,positionY+textOffsetY+targetTextSize,300,300), points, textStyle);
		
>>>>>>> origin/feature/wardrobeCleanup
	}

	void Start(){
		loadPoints();
<<<<<<< HEAD
	}
=======
		username = GameStatics.username;
		if(username == string.Empty)
			username = "Your name";
	}

	
>>>>>>> origin/feature/wardrobeCleanup
	
	private void loadPoints(){
		try{
			points = LocalStorage.ReadUTF8File("personal_highScore");
<<<<<<< HEAD
		} catch(Exception ex){
			Debug.Log(ex.Message);
			points = "0000000";
		}
	}
=======
		} catch(Exception e){
			Debug.LogWarning(e.Message);
			points = "0000000";
		}
	}

	
>>>>>>> origin/feature/wardrobeCleanup
	
	protected override void hit(){
		
	}

	
}
