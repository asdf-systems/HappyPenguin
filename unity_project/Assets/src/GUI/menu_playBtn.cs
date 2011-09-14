using UnityEngine;
using System.Collections;

public class menu_playBtn : GUIBaseElement {
		
	
	protected override void showElements(){
		GeneralScreenGUI.Box(guiStatics, new Rect (positionX,positionY,256,256), "", currentStyle);
	}
	
	protected override void hit(){
		// start Game
		//Debug.Log("Start Game");
		Application.LoadLevel(1);
	}
}
