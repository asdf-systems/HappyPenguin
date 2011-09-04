using UnityEngine;
using System.Collections;

public class GUIBaseElement : MonoBehaviour {

	public int guiDepth;
	public GUIManager guiManager;
	
	// Active Rectangle 
	public int height;
	public int width;
	public int positionX;
	public int positionY;
	
	public GUIStyle inactiveStyle;
	public GUIStyle activeStyle;
	public GUIStyle hoverStyle;
	
	protected GUIStyle currentStyle;
	
	void Start(){
		resetElement();
		
	}
	
	void OnGUI(){
		GUI.depth = guiDepth;
		showElements();
		hitTest();
	}
	
	void hitTest(){
		// check Mouse Position against size
		checkMouse();
		// check iPhone Tap against size
		checkiPhoneTap();
		// check Android Tap against size
		checkAndroidTap();
		
	}
	
	private void checkMouse(){
		Vector3 mousePos = Input.mousePosition;
		if(cursorInside(mousePos, new Vector3 (positionX, positionY, 0), new Vector3(this.width, this.height, 0))){
			preHover();
			//! TODO entprellen
			if(Input.GetMouseButton(0))
				preHit();
		} else
			resetElement();
	}
	
	private void checkiPhoneTap(){
		//! TODO  entprellen
		Touch[] touches = Input.touches;
		foreach(Touch touch in touches){
			if(cursorInside(new Vector3(touch.position.x, touch.position.y, 0) , new Vector3(positionX, positionY, 0), new Vector3(this.width, this.height, 0) ))
				preHit();
			else 
				resetElement();
		}
		
	}
	
	protected void resetElement(){
		currentStyle = inactiveStyle;
	}
	private void checkAndroidTap(){
		//! TODO Android Tap
	}
	
	private void preHover(){
		currentStyle = hoverStyle;
		hover();
	}
	private void preHit(){
		currentStyle = activeStyle;
		hit();
	}
	
	protected virtual void showElements(){
		// overwrite in child classes
	}
	
	protected virtual void hit(){
		// overwrite in child classes
	}
	
	protected virtual void hover(){
		// overwrite in child classes - only on PC and MAC
	}
	
	protected bool cursorInside(Vector3 mousePos, Vector3 elemPos, Vector3 elemSize){
		bool flagX = false;
		bool flagY = false;
		bool flagZ = false;
		
		mousePos = GeneralScreenGUI.normalizeMouse(guiManager , mousePos);
		
		if(mousePos.x >= elemPos.x && ( mousePos.x <= ( elemPos.x + elemSize.x ) ) )
			flagX = true;
		if(mousePos.y >= elemPos.y && ( mousePos.y <= ( elemPos.y + elemSize.y ) ) )
			flagY = true;
		if(mousePos.z >= elemPos.z && ( mousePos.z <= ( elemPos.z+ elemSize.z ) ) )
			flagZ = true;
		
		//Debug.Log(mousePos.x + " " + mousePos.y + " " +elemPos.x + " " +elemPos.y +" " + elemSize.x + " " +elemSize.y + " " + (flagX && flagY && flagZ));
		
		return (flagX && flagY && flagZ);
	}
	
}
