using UnityEngine;
using System.Collections;

public class PointsDisplayGameEnd: UIElementBehaviour<GUIStatics>{
		
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
	}
	
	protected override void hit(){
		
		

	}
}
