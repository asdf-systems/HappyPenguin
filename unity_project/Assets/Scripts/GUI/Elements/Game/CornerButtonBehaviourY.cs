using UnityEngine;
using System.Collections;

public sealed class CornerButtonBehaviourY: UIElementBehaviour<GUIManager> {
	
	protected override void showElements(){
		GeneralScreenGUI.Box(guiStatics, new Rect (positionX,positionY,Width,Height), "", currentStyle);
	}
	
	protected override void hit(){
		guiStatics.NotifyButtonYHit();
	}
}
