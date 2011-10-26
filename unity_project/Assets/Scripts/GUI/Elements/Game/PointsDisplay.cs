using UnityEngine;
using System.Collections;

public class PointsDisplay : UIElementBehaviour<GUIManager>{
		
	public int targetFontSize = 24;
	public GUIStyle textStyle;
	public float Points{
		get;
		set;
	}
	
	public float Life{
		get;
		set;
	}
	
	protected override void showElements(){
		textStyle.fontSize = targetFontSize;
		GeneralScreenGUI.Box(guiStatics, new Rect (positionX,positionY,256,256), "", currentStyle);
		GeneralScreenGUI.Label(guiStatics, new Rect (positionX+50, positionY+20, 512,512), "Points: " + Points, textStyle );
	}
	
	protected override void hit(){
		guiStatics.NotifyButtonCHit();
		

	}
}
