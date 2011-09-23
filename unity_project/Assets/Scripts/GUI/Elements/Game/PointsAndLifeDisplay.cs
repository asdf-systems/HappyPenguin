using UnityEngine;
using System.Collections;

public class PointsAndLifeDisplay: UIElementBehaviour<GUIManager>{
		
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
		GeneralScreenGUI.Box(guiStatics, new Rect (positionX,positionY,512,512), "", currentStyle);
		GeneralScreenGUI.Box(guiStatics, new Rect (positionX+120, positionY+10, 512,512), "Points: " + Points + " Life: " + Life, textStyle );
	}
	
	protected override void hit(){
		guiStatics.buttonCHit();
		

	}
}
