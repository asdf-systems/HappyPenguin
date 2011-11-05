using UnityEngine;
using System.Collections;

public class infoTafel : UIElementBehaviour<GUIStatics> {

	public int targetFontSize;
	public int textOffsetX;
	public int textOffsetY;
	public string infoText{get;set;}
	public GUIStyle textStyle;
	
	protected override void showElements(){
		GeneralScreenGUI.Box(guiStatics, new Rect(positionX,positionY,700,700), "", inactiveStyle);
		textStyle.fontSize = targetFontSize;
		GeneralScreenGUI.Label(guiStatics, new Rect(positionX+textOffsetX,positionY+textOffsetY,350,350), infoText, textStyle);
	}
	
	protected override void hit(){
		
	}
}
