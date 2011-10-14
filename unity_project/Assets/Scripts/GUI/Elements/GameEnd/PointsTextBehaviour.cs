using UnityEngine;
using System.Collections;

public class PointsTextBehaviour : UIElementBehaviour<GUIStatics>{

	protected override void showElements (){
		GeneralScreenGUI.Box(new Rect(positionX, positionY, 500, 200), "Points: " + GameStatics.Points, currentStyle);
	}
	
}
