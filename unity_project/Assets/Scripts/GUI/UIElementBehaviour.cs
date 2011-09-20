using UnityEngine;
using System.Collections;

public class UIElementBehaviour<T>  : MonoBehaviour where T : GUIStatics {

	public int guiDepth;
	public T guiStatics;
	
	// Active Rectangle 
	public int textHeight;
	public int textWidth;
	public int positionX;
	public int positionY;
	
	public GUIStyle inactiveStyle;
	public GUIStyle activeStyle;
	public GUIStyle hoverStyle;
	
	protected GUIStyle currentStyle;
	
	protected bool buttonDown;
	protected bool iPhoneTap;
	protected bool androidTap;
	
	private Vector2 startingPosition;
	private Vector2 endingPosition;
	
	
	void Start(){
		resetElement();
		resetButtons("all");
		
	}
	
	void Update(){
		checkForSwipes();
	}
	
	
	void OnGUI(){
		GUI.depth = guiDepth;
		showElements();
		hitTest();
		
	}
	
	private void  checkForSwipes(){
			if (Input.GetKeyDown("return")){
				Debug.Log("Enter gedrückt");
				swipe(GUIManager.Directions.Right);
			}
			else if (Input.GetKeyDown("backspace")) {
				swipe(GUIManager.Directions.Left);
			}
			//iPhone swipe
			else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
				startingPosition = Input.GetTouch(0).position;
				return;
			}
			else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
				endingPosition = Input.GetTouch(0).position;
				
				if (endingPosition.x > startingPosition.x) {
					swipe(GUIManager.Directions.Right);
				}
				else {
					swipe(GUIManager.Directions.Left);
				}
			}
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
		if(cursorInside(mousePos, new Vector3 (positionX, positionY, 0), new Vector3(this.textWidth, this.textHeight, 0))){
			preHover();
			if(Input.GetMouseButton(0)){
				if(!buttonDown){
					buttonDown = true;
					preHit();
				}
			} else if(buttonDown)
				resetButtons("mouse");	
		} else
			resetElement();
	}
	
	private void checkiPhoneTap(){
		Touch[] touches = Input.touches;
		bool down = false;
		foreach(Touch touch in touches){
			down = true;
			if(cursorInside(new Vector3(touch.position.x, touch.position.y, 0) , new Vector3(positionX, positionY, 0), new Vector3(this.textWidth, this.textHeight, 0) )){
				if(!iPhoneTap){
					iPhoneTap = true;
					preHit();
				}
			} else 
				resetElement();
		}
		if(!down && iPhoneTap)
			resetButtons("iPhoneTap");
		
	}
	
	private void resetButtons(string state){
		
		if(state == "mouse" || state == "all"){
			Debug.Log("Reset Buttons");
			buttonDown = false;
		}
			
		if(state == "iPhoneTap" || state == "all")
			iPhoneTap = false;
		if(state == "androidTap" || state == "all")
			androidTap = false;
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
		buttonDown = true;
		hit();
	}
	
	protected virtual void swipe(GUIManager.Directions direction){
		// override in child classes
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
		
		mousePos = GeneralScreenGUI.NormalizeMouse(guiStatics , mousePos);
		
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
