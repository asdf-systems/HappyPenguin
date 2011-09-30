using UnityEngine;
using System.Collections;

public class CreditsText: UIElementBehaviour<GUIStatics>{
		
	public GUIStyle textStyle;
	
	public string text{
		get;
		set;
	}
	protected override void showElements(){
		GeneralScreenGUI.Box(guiStatics, new Rect (positionX,positionY,512,512), text, currentStyle);
	}
	
	protected override void hit(){
		
		

	}
}
