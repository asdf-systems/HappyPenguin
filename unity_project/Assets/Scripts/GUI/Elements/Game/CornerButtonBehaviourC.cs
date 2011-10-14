using UnityEngine;
using System.Collections;

public class CornerButtonBehaviourC: UIElementBehaviour<GUIManager>{
		
	
	protected override void showElements(){
		GeneralScreenGUI.Box(guiStatics, new Rect (positionX,positionY,128,128), "", currentStyle);
	}
	
	protected override void hit(){
		guiStatics.NotifyButtonCHit();
		

	}
}
