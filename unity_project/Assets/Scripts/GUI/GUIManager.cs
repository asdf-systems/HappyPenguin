using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pux;
using Pux.Effects;
using Pux.UI;
using Pux.Resources;
using System;

/**
 * Singleton Class to handle all GUI Actions
 */
public class GUIManager : MonoBehaviour {
	private static GUIManager instance;
	
	public CornerButton buttonC;
	public CornerButton buttonE;
	public CornerButton buttonQ;
	public CornerButton buttonY;
	
	public Button ResumeGameButton;
	public Button CancelGameButton;
	
	public TextPanel PointsDisplay;

	private string symbolChain;
	private Time textTimer;
	private int poorMansBarrier;
	
	public float ButtonSlideDistance {
		get;
		set;
	}

	public AlertTextPanel AlertTextEntity;

	
	public enum Directions{
		Left,
		Right
	}
	
	private List<Vector2> positions;

	public event EventHandler<SymbolEventArgs> SymbolsChanged;
	public event EventHandler<SwipeEventArgs> SwipeCommitted;
	public event EventHandler GameResumed;
	public event EventHandler GamePaused;
	public event EventHandler GameCanceld;
	
	
	public static GUIManager Instance{
		get; 
		private set;
	}
	
	private void Awake(){
		Instance = this;
		InitButtons();
		CheckAssertions();
		ButtonSlideDistance = 181; // magnitude of Vector2(128,128) 
	}
	
	void Start(){
		ResumeGameButton.Visibility = false;
		CancelGameButton.Visibility = false;
	}
	
	private void CheckAssertions(){
		if(PointsDisplay == null){
			EditorDebug.LogError("GUI_Manger has no PointsDisplay Assigned");
		}
		if(buttonC == null){
			EditorDebug.LogError("GUIManager has no buttonC assigned");
		}
		if(buttonY == null){
			EditorDebug.LogError("GUIManager has no buttonY assigned");
		}
		if(buttonQ == null){
			EditorDebug.LogError("GUIManager has no buttonQ assigned");
		}
		if(buttonE == null){
			EditorDebug.LogError("GUIManager has no buttonE assigned");
		}
		if(AlertTextEntity == null){
			EditorDebug.LogError("GUIManager has no AlertTextPanel assigned");
		}
	}
	
	private CornerButton FindComponent(GameObject gObject){
		return gObject.GetComponent<CornerButton>();
	}

//	private void Reset() {
//		buttonC.positionX = (int)buttonCpos.x;
//		buttonC.positionY = (int)buttonCpos.y;
//		
//		buttonE.positionX = (int)buttonEpos.x;
//		buttonE.positionY = (int)buttonEpos.y;
//		
//		buttonQ.positionX = (int)buttonQpos.x;
//		buttonQ.positionY = (int)buttonQpos.y;
//		
//		buttonY.positionX = (int)buttonYpos.x;
//		buttonY.positionY = (int)buttonYpos.y;
//		
//	}
	private void InitButtons() {
		/*var buttons = GameObject.FindGameObjectsWithTag("corner_button").Select(x => FindComponent(x));
		buttonC = buttons.First(x => x.Symbol == "C");
		buttonQ = buttons.First(x => x.Symbol == "Q");
		buttonE = buttons.First(x => x.Symbol == "E");
		buttonY = buttons.First(x => x.Symbol == "Y");*/
		positions = new List<Vector2>(){buttonC.Position,buttonE.Position,buttonQ.Position, buttonY.Position};
	}
	
	private void OnButtonsSlidOut(Action action, ClockRotations rotation)
	{
		EditorDebug.LogWarning("On ButtonsSlideOut need to be implemented again");
		poorMansBarrier ++;
		if (poorMansBarrier < 4) {
			return;
		}
		
		RotateCornerButtons(rotation);
		if (action != null) {
			action();
		}
	}
	
	private void RotateCornerButtons(ClockRotations rotation)
	{
		if (rotation == ClockRotations.Clockwise) {
			RotateSingleButton(buttonC, 90);
			RotateSingleButton(buttonE, 90);
			RotateSingleButton(buttonQ, 90);
			RotateSingleButton(buttonY, 90);
			return;
		}
		
		RotateSingleButton(buttonC, -90);
		RotateSingleButton(buttonE, -90);
		RotateSingleButton(buttonQ, -90);
		RotateSingleButton(buttonY, -90);
	}
	
	private void RotateSingleButton(Button button, float angle){
		button.transform.RotateAroundLocal(Vector3.up, angle);	
	}

	public void PerformUIRotation(ClockRotations clockRotation)
	{
		poorMansBarrier = 0;
		if (clockRotation == ClockRotations.Clockwise) {
			SlideButtonsOut(() => OnButtonsSlidOut(MoveRight, clockRotation));
		} else{
			SlideButtonsOut(() => OnButtonsSlidOut(MoveLeft, clockRotation));
		}
	}
	
	private void OnButtonsRotated()
	{
		SlideButtonsIn();
	}

	private void MoveLeft() {
		var tmp = positions[positions.Count - 1];
		positions.Insert(0, tmp);
		positions.RemoveAt(positions.Count - 1);
		UpdatePositions();
		OnButtonsRotated();
	}
	
	private void MoveRight() {
		positions.Add(positions[0]);
		positions.RemoveAt(0);
		UpdatePositions();
		OnButtonsRotated();
	}
	
	private void UpdatePositions() {
		buttonC.Position = positions[0];
		buttonE.Position = positions[1];
		buttonQ.Position = positions[2];
		buttonY.Position = positions[3];		
	}
	
	private void SlideButtonsOut(Action postAction)
	{
		PerformButtonSlide(buttonC, SlideDirections.Out, postAction);
		PerformButtonSlide(buttonE, SlideDirections.Out, postAction);
		PerformButtonSlide(buttonQ, SlideDirections.Out, postAction);
		PerformButtonSlide(buttonY, SlideDirections.Out, postAction);
	}
	
	private void SlideButtonsIn()
	{
		PerformButtonSlide(buttonC, SlideDirections.In, null);
		PerformButtonSlide(buttonE, SlideDirections.In, null);
		PerformButtonSlide(buttonQ, SlideDirections.In, null);
		PerformButtonSlide(buttonY, SlideDirections.In, null);
	}
	
	private Vector2 GetSnapPositionForButton(CornerButton button)
	{
		EditorDebug.LogWarning("GetSnapPositionsForButton need to be implemented again");
		// left
		if (button.Position.x < 480) {
			if (button.Position.y > 320) {
				return new Vector2(0, 640 - button.VirtualRegionOnScreen.height);
			} else {
				return new Vector2(0, 0);
			}
		} else {
			if (button.Position.y > 320) {
				return new Vector2(960 - button.VirtualRegionOnScreen.width, 640 - button.VirtualRegionOnScreen.height);
			} else {
				return new Vector2(960 - button.VirtualRegionOnScreen.width, 0);
			}
		}
	}
	
	private void PerformButtonSlide(CornerButton button, SlideDirections direction, Action postAction)
	{
		var retinaCenter = new Vector2(480, 320);
		var buttonCenter = button.Position + new Vector2((float) button.VirtualRegionOnScreen.width / 2.0f, button.VirtualRegionOnScreen.y + (float) button.VirtualRegionOnScreen.x / 2.0f );

		var directingVector = buttonCenter - retinaCenter;
		directingVector.Normalize();
		
		var sign = direction == SlideDirections.Out ? 1 : -1;
		var targetPosition =  buttonCenter + sign * directingVector * ButtonSlideDistance;
		
		//decentralize position
		targetPosition -= new Vector2((float)button.VirtualRegionOnScreen.width / 2.0f, (float)button.VirtualRegionOnScreen.height / 2.0f);
		if (direction == SlideDirections.In) {
			// need to deal with rounding errors
			targetPosition = GetSnapPositionForButton(button);
		}
		
		// set different easing functions for sliding in and out
		Func<float,float> easingFunc = null;
		if (direction == SlideDirections.Out) {
			easingFunc = (x) => x * x;
		} else {
			easingFunc = (x) => (float)(Math.Log(x) + 6.0f) / 6.0f;
		}
		
		var controller = new UIElementSlideController(easingFunc) {
			StartPosition = button.Position,
			TargetPosition = targetPosition,
			Duration = TimeSpan.FromMilliseconds(250)
		};
		
		if (postAction != null) {
			controller.ControllerFinished += (sender, e) => postAction.Invoke();
		}
		
		button.QueueController(button.name, controller);
	}

	public void ClearSymbols() {
		symbolChain = string.Empty;
		InvokeSymbolsChanged();
	}

	public void Alert(string value) {
		EditorDebug.Log("Alert");
		AlertTextEntity.ShowText(value);
	}

	public void NotifyButtonHit(string symbol) {
		symbolChain += symbol;
		InvokeSymbolsChanged();
	}

	public void DisplayPoints(float points) {
		/*if (PointsDisplay == null)
			PointsDisplay = gameObject.GetComponentInChildren<PointsDisplay>();*/
		PointsDisplay.Text = points.ToString();
	}

	/* Not Needed any more -> Balloons
	public void DisplayLife(float life) {
		if (pointsDisplay == null)
			pointsDisplay = gameObject.GetComponentInChildren<PointsDisplay>();
		pointsDisplay.Life = life;
	}*/

// Invoke Events
	public void PreSwipeCommitted(Vector2 direction) {
		InvokeSwipeCommitted(direction);
	}

	private void InvokeSymbolsChanged() {
		var handler = SymbolsChanged;
		if (handler == null) {
			return;
		}
		
		var e = new SymbolEventArgs(symbolChain);
		SymbolsChanged(this, e);
	}

	private void InvokeSwipeCommitted(Vector2 direction) {
		var handler = SwipeCommitted;
		if (handler == null) {
			return;
		}
		
		var e = new SwipeEventArgs(direction, symbolChain);
		SwipeCommitted(this, e);
	}
	
	public void InvokeGameResumed(){
		CancelGameButton.Visibility = false;
		ResumeGameButton.Visibility = false;
		var handler = GameResumed;
		if (handler == null) {
			return;
		}
		GameResumed(this, EventArgs.Empty);
		
	}
	public void InvokeGameCanceld(){
		var handler = GameResumed;
		if (handler == null) {
			return;
		}
		GameCanceld(this, EventArgs.Empty);
		
	}
	public void InvokeGamePaused(){
		CancelGameButton.Visibility = true;
		ResumeGameButton.Visibility = true;
		var handler = GameResumed;
		if (handler == null) {
			return;
		}
		GamePaused(this, EventArgs.Empty);
		
	}
}
