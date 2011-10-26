using UnityEngine;
using System.Collections;

public class TextFirst : UIElementBehaviour<GUIStatics> {
	
	public int targetTextSize;
	public int position;
	
	protected override void showElements(){
		string name = this.gameObject.GetComponent<HighscoreServerConnection>().getNameFromHighscoreList(position);
		string points = this.gameObject.GetComponent<HighscoreServerConnection>().getpointsFromHighscoreList(position);
		// Highscore name
		inactiveStyle.fontSize = targetTextSize;
		GeneralScreenGUI.Label(guiStatics, new Rect(positionX,positionY,128,128), name, inactiveStyle);
		// Pointdisplay
		this.GetComponent<Numbers>().points = points;
		
	}
	
	
	
	protected override void hit(){
	}
}
