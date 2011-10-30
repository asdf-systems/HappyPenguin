using UnityEngine;
using System.Collections;

public class PointsTextBehaviour : UIElementBehaviour<GUIStatics>{
<<<<<<< HEAD
	public int targetTextSize;
=======
	public int targetTextSize; 
	public GUIStyle textStyle;
>>>>>>> origin/feature/wardrobeCleanup
	protected override void showElements (){
		
		textStyle.fontSize = targetTextSize;
		GeneralScreenGUI.Label(guiStatics, new Rect(positionX, positionY, 200, 200), "Points: " + GameStatics.Points, textStyle);
	}
	
}
