using UnityEngine;
using System.Collections;

public class CreditsText: UIElementBehaviour<GUIStatics>{
		
	public int targetTextSize;
	public GUIStyle textStyle;
	
	public string text{
		get;
		set;
	}
	protected override void showElements(){
		textStyle.fontSize = targetTextSize;
		GeneralScreenGUI.Label(guiStatics, new Rect (positionX,positionY,512,512), text, textStyle);
	}
	
	protected override void hit(){
		
		

	}
}
