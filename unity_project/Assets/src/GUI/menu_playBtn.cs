using UnityEngine;
using System.Collections;

public class menu_playBtn : GUIBaseElement {
		
	
	protected override void showElements(){
		GeneralScreenGUI.Box(guiManager, new Rect (positionX,positionY,256,256), "", currentStyle);
	}
	
	protected override void hit(){
		// start Game
		Debug.Log("Start Game");
		Application.LoadLevel(1);
	}
}
