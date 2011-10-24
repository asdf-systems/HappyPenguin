using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HappyPenguin.UI;

public class UIElementBehaviour<T> : MonoBehaviour where T : GUIStatics
{
	public int guiDepth;
	public T guiStatics;

	// Active Rectangle 
	public int textHeight;
	public int textWidth;
	public int positionX;
	public int positionY;
	
	public float Speed;

	public GUIStyle inactiveStyle;
	public GUIStyle activeStyle;
	public GUIStyle hoverStyle;

	protected GUIStyle currentStyle;

	protected bool buttonDown;
	protected bool iPhoneTap;
	protected bool androidTap;

	private Vector2 startingPosition;
	private Vector2 endingPosition;
	
	public IList<UIElementController<T>> Controllers {
		get;
		private set;
	}
	
	private List<UIElementController<T>> queuedControllers;

	void Start() {
		resetElement();
		resetButtons("all");
	}

	protected virtual void Update() {
		checkForSwipes();
		hitTest();
		UpdateControllers();
	}
	
	private void UpdateControllers()
	{
		if (Controllers == null) {
			// no clue why this happens
			return;
		}
		
		var obs = new List<UIElementController<T>>();
		foreach (var c in Controllers) {
			if (c.IsFinished) {
				obs.Add(c);
				continue;
			}
			c.Update(this);
		}
		
		foreach (var o in obs) {
			Controllers.Remove(o);
		}
	
		foreach (var c in queuedControllers) {
			Controllers.Add(c);
		}
		queuedControllers.Clear();
	}
	
	private void Awake() {
		queuedControllers = new List<UIElementController<T>>();
		Controllers = new List<UIElementController<T>>();
		Speed = 30;
		Width = 128;
		Height = 128;
	}

	private void OnGUI() {
		GUI.depth = guiDepth;
		showElements();
		//hitTest();
		
	}

	private void checkForSwipes() {
		if (Input.GetKeyDown("return")) {
			Debug.Log("Enter gedrückt");
			swipe(GUIManager.Directions.Right);
		} else if (Input.GetKeyDown("backspace")) {
			swipe(GUIManager.Directions.Left);
		
		} else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
			startingPosition = Input.GetTouch(0).position;
			return;
		} else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
			endingPosition = Input.GetTouch(0).position;
			
			var treshold = 50;
			var dif = endingPosition.x - startingPosition.x;
			Debug.Log("Difference: " + dif);
			if (dif > treshold) {
				swipe(GUIManager.Directions.Right);
			}
			if (dif < -treshold) {
				swipe(GUIManager.Directions.Left);
			}
		}
	}
	
	void hitTest() {
		checkMouse();
		// check iPhone Tap against size
		checkiPhoneTap();
		// check Android Tap against size
		checkAndroidTap();
		// check Mouse Position against size
	}
	
	public Vector2 Position {
		get { return new Vector2(positionX, positionY); }
		set {
			positionX = (int) value.x;
			positionY = (int) value.y;
		}
	}

	private void checkMouse() {
		if(Input.touches.Length > 0)
			return;
		Vector3 mousePos = Input.mousePosition;
		if (cursorInside(mousePos, new Vector3(positionX, positionY, 0), new Vector3(this.textWidth, this.textHeight, 0))) {
			preHover();
			if (Input.GetMouseButton(0)) {
				if (!buttonDown) {
					Debug.Log("ButtonDown");
					buttonDown = true;
					preHit();
				}
			} else if (buttonDown)
				resetButtons("mouse");
		} else
			resetElement();
	}

	private void checkiPhoneTap() {
		Touch[] touches = Input.touches;
		//bool down = false;
		//foreach (Touch touch in touches) {
		if(touches.Length > 0 ){
			Touch touch = touches[0];
			if(touch.phase != TouchPhase.Ended)
				return;
			Debug.Log("Touch: " + touch.phase.ToString());
			
			if (cursorInside(new Vector3(touch.position.x, touch.position.y, 0), new Vector3(positionX, positionY, 0), new Vector3(this.textWidth, this.textHeight, 0))) {
					if (!iPhoneTap) {
						iPhoneTap = true;
						Debug.Log("Button Hit");
						preHit();
					}
				} else {
					resetElement();
				}
		} else if(iPhoneTap){
			
			Debug.Log("reset Buttons - iPhoneTap");
			resetButtons("iPhoneTap");
		}
		
	}

	private void resetButtons(string state) {
		if (state == "mouse" || state == "all") {
			Debug.Log("Reset Buttons");
			buttonDown = false;
		}
		
		if (state == "iPhoneTap" || state == "all")
			iPhoneTap = false;
		if (state == "androidTap" || state == "all")
			androidTap = false;
	}

	protected void resetElement() {
		currentStyle = inactiveStyle;
	}
	private void checkAndroidTap() {
		//! TODO Android Tap
	}

	private void preHover() {
		currentStyle = hoverStyle;
		hover();
	}
	private void preHit() {
		currentStyle = activeStyle;
		buttonDown = true;
		hit();
	}
	
	public Vector2 StartPosition {
		get;
		set;
	}
	
	public int Width {
		get;
		set;
	}

	public int Height {
		get;
		set;
	}
	
	public void QueueController(UIElementController<T> controller)
	{
		queuedControllers.Add(controller);
	}

	protected virtual void swipe(GUIManager.Directions direction) {
		// override in child classes
	}

	protected virtual void showElements() {
		// overwrite in child classes
	}

	protected virtual void hit() {
		// overwrite in child classes
	}

	protected virtual void hover() {
		// overwrite in child classes - only on PC and MAC
	}

	protected bool cursorInside(Vector3 mousePos, Vector3 elemPos, Vector3 elemSize) {
		bool flagX = false;
		bool flagY = false;
		bool flagZ = false;
		
		mousePos = GeneralScreenGUI.NormalizeMouse(guiStatics, mousePos);
		
		if (mousePos.x >= elemPos.x && (mousePos.x <= (elemPos.x + elemSize.x)))
			flagX = true;
		if (mousePos.y >= elemPos.y && (mousePos.y <= (elemPos.y + elemSize.y)))
			flagY = true;
		if (mousePos.z >= elemPos.z && (mousePos.z <= (elemPos.z + elemSize.z)))
			flagZ = true;
		
		//Debug.Log(mousePos.x + " " + mousePos.y + " " +elemPos.x + " " +elemPos.y +" " + elemSize.x + " " +elemSize.y + " " + (flagX && flagY && flagZ));
		
		return (flagX && flagY && flagZ);
	}
}
