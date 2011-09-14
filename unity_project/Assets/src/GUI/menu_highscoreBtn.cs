using UnityEngine;
using System.Collections;

public class menu_highscoreBtn: GUIBaseElement {
		
	
	protected override void showElements(){
		GeneralScreenGUI.Box(guiManager, new Rect (positionX,positionY,256,256), "", currentStyle);
	}
	
	protected override void hit(){
		Debug.Log("Highscore not implemented yet!");
	}
}
