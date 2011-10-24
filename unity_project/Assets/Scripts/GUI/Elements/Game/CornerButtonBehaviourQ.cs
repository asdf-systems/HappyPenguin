using UnityEngine;
using System.Collections;

public class CornerButtonBehaviourQ: UIElementBehaviour<GUIManager>{
		
	
	protected override void showElements(){
		GeneralScreenGUI.Box(guiStatics, new Rect (positionX,positionY,Width,Height), "", currentStyle);
	}
	
	protected override void hit(){
		guiStatics.NotifyButtonQHit();
		

	}
}
