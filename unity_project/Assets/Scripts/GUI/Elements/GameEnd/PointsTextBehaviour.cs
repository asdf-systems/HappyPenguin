using UnityEngine;
using System.Collections;

public class PointsTextBehaviour : UIElementBehaviour<GUIStatics>{
	public int targetTextSize;
	protected override void showElements (){
		currentStyle.fontSize = targetTextSize;
		GeneralScreenGUI.Box(new Rect(positionX, positionY, 500, 200), "Points: " + GameStatics.Points, currentStyle);
	}
	
}
