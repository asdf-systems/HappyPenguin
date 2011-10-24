using UnityEngine;
using System.Collections;

public class CornerButtonBehaviourE : UIElementBehaviour<GUIManager>{
	protected override void showElements(){
		GeneralScreenGUI.Box(guiStatics, new Rect (positionX,positionY,Width,Height), "", currentStyle);
	}
	
	protected override void hit(){
		guiStatics.NotifyButtonEHit();
	}
}
