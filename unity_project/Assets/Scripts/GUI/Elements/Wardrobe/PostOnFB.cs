using UnityEngine;
using System.Collections;

public class PostOnFB : UIElementBehaviour<GUIStatics> {

	protected override void showElements(){
		GeneralScreenGUI.Box(guiStatics, new Rect(positionX,positionY,128,128), "Post on Facebook", currentStyle);
		
	}
	
	protected override void hit(){
		// TODO
	}
}
