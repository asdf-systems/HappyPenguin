using UnityEngine;
using System.Collections;

public class CornerButtonBehaviourE : UIElementBehaviour<GUIManager>{
	protected override void showElements(){
		GeneralScreenGUI.Box(guiStatics, new Rect (positionX,positionY,128,128), "", currentStyle);
	}
	
	protected override void hit(){
		guiStatics.NotifyButtonEHit();
	}
}
