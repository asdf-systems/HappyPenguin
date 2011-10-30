using UnityEngine;
using System.Collections;

public class forward_toWardrobe : UIElementBehaviour<GUIStatics> {

	public bool showElement {get;set;}
	void Start(){
		showElement = true;
	}
	protected override void showElements(){
		if(showElement)
			GeneralScreenGUI.Box(guiStatics, new Rect(positionX,positionY,256,256), "", currentStyle);
		
	}
	
	protected override void hit(){
		Application.LoadLevel(4);
	}
}
