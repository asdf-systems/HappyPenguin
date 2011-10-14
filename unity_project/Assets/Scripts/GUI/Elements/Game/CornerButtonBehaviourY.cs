using UnityEngine;
using System.Collections;

public sealed class CornerButtonBehaviourY: UIElementBehaviour<GUIManager> {
	
	protected override void showElements(){
		GeneralScreenGUI.Box(guiStatics, new Rect (positionX,positionY,128,128), "", currentStyle);
	}
	
	protected override void hit(){
		guiStatics.NotifyButtonYHit();
	}
}
