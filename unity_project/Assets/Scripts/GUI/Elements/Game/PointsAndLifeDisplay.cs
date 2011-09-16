using UnityEngine;
using System.Collections;

public class PointsAndLifeDisplay: UIElementBehaviour<GUIManager>{
		
	
	protected override void showElements(){
		GeneralScreenGUI.Box(guiStatics, new Rect (positionX,positionY,512,512), "", currentStyle);
	}
	
	protected override void hit(){
		guiStatics.buttonCHit();
		

	}
}
