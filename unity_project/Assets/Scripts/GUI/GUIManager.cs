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
	
	private bool restore;
	private bool isSliding;
	public CornerButton buttonC;
	public CornerButton buttonE;
	public CornerButton buttonQ;
	public CornerButton buttonY;
	
	public Button ResumeGameButton;
	public Button CancelGameButton;
	public Panel CancelResumeText;
	
	public TextPanel PointsDisplay;
	
	public Panel DarkScreen;
	public Panel NightEffectScreen;

	private string symbolChain;
	private Time textTimer;
	private int poorMansBarrier;
	private bool firstUpdate = true;
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
	public event EventHandler GameCancelled;
	
	
	public static GUIManager Instance{
		get; 
		private set;
	}
	
	private void Update(){
		if(firstUpdate){
			HideExtraElements();
			firstUpdate = false;
		}
		if (isSliding) {
			buttonC.UpdateElement();
			buttonE.UpdateElement();
			buttonQ.UpdateElement();
			buttonY.UpdateElement();
		}
	}
	
	private void Awake() {
		Instance = this;
		InitButtons();
		CheckAssertions();
		ButtonSlideDistance = 181; // magnitude of Vector2(128,128) 
	}
	
	void Start() {
		HideExtraElements();
	}
	
	public void HideExtraElements(){
		ResumeGameButton.Visibility = false;
		CancelGameButton.Visibility = false;
		CancelResumeText.Visibility = false;
		NightEffectScreen.Visibility = false;
		DarkScreen.Visibility = false;
	}
	
	public void NightEffect(bool nihgt){
		NightEffectScreen.Visibility = nihgt;
	}
	public void DarkenScreen(bool darken){
		DarkScreen.Visibility = darken;
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
		positions = new List<Vector2>(){ buttonC.Position, buttonE.Position, buttonQ.Position, buttonY.Position };
	}
	
	private void StorePositions(){
		positions.Clear();
		positions.AddRange(new [] { buttonC.Position, buttonE.Position, buttonQ.Position, buttonY.Position });
	}
	
	private void OnButtonsSlidOut(Action action, ClockRotations rotation)
	{
		poorMansBarrier ++;
		if (poorMansBarrier < 4) {
			return;
		}
		
		SetRotation(rotation);
		if (action != null) {
			action();
		}
	}
	

	private void SetRotation(ClockRotations rotation)
	{
		var degrees = restore ? 0 : -90;
		if (rotation == ClockRotations.Clockwise) {
			RotateSingleButton(buttonC, degrees);
			RotateSingleButton(buttonE, degrees);
			RotateSingleButton(buttonQ, degrees);
			RotateSingleButton(buttonY, degrees);
			return;
		}
		
		RotateSingleButton(buttonC, -degrees);
		RotateSingleButton(buttonE, -degrees);
		RotateSingleButton(buttonQ, -degrees);
		RotateSingleButton(buttonY, -degrees);
	}
	
	public void RotateSingleButton(Button button, float degrees){
		button.SetRotationTransformations(new Vector2(0.5f,0.5f), degrees);	
	}

	public void PerformUIRotation(ClockRotations clockRotation, bool restore)
	{	
		isSliding = true;
		poorMansBarrier = 0;
		this.restore = restore;
		if (clockRotation == ClockRotations.Clockwise) {
			SlideButtonsOut(() => OnButtonsSlidOut(MoveClockwise, clockRotation));
		} else{
			SlideButtonsOut(() => OnButtonsSlidOut(MoveCounterClockwise, clockRotation));
		}
	}
	
	private void OnButtonsRotated()
	{
		SlideButtonsIn();
	}

	private void MoveCounterClockwise() {
		StorePositions();
		var tmp = positions[positions.Count - 1];
		positions.Insert(0, tmp);
		positions.RemoveAt(positions.Count - 1);
		UpdatePositions();
		SetRotation(ClockRotations.CounterClockwise);
		OnButtonsRotated();
	}
	
	private void MoveClockwise() {
		StorePositions();
		positions.Add(positions[0]);
		positions.RemoveAt(0);
		UpdatePositions();
		SetRotation(ClockRotations.Clockwise);
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
		poorMansBarrier = 0;
		PerformButtonSlide(buttonC, SlideDirections.In, OnButtonsSlidIn);
		PerformButtonSlide(buttonE, SlideDirections.In, OnButtonsSlidIn);
		PerformButtonSlide(buttonQ, SlideDirections.In, OnButtonsSlidIn);
		PerformButtonSlide(buttonY, SlideDirections.In, OnButtonsSlidIn);
	}
	
	private void OnButtonsSlidIn(){
		poorMansBarrier ++;
		if (poorMansBarrier < 4) {
			return;
		}
		
		isSliding = false;
	}
	
	private Vector2 GetSnapPositionForButton(CornerButton button)
	{
		EditorDebug.LogWarning("GetSnapPositionsForButton need to be implemented again");
		// left
		if (button.Position.x < 480) {
			if (button.Position.y > 320) {
				button.verticalFloat = Frame.VerticalFloatPositions.bottom;
				button.horizontalFloat = Frame.HorizontalFloatPositions.left;
				button.UpdateElement();
				button.removeFloat();
				//return new Vector2(0, 640 - button.VirtualRegionOnScreen.height);
				
			} else {
				button.verticalFloat = Frame.VerticalFloatPositions.top;
				button.horizontalFloat = Frame.HorizontalFloatPositions.left;
				button.UpdateElement();
				button.removeFloat();
				
			}
		} else {
			if (button.Position.y > 320) {
				button.verticalFloat = Frame.VerticalFloatPositions.bottom;
				button.horizontalFloat = Frame.HorizontalFloatPositions.right;
				button.UpdateElement();
				button.removeFloat();
				//return new Vector2(960 - button.VirtualRegionOnScreen.width, 640 - button.VirtualRegionOnScreen.height);
			} else {
				button.verticalFloat = Frame.VerticalFloatPositions.top;
				button.horizontalFloat = Frame.HorizontalFloatPositions.right;
				button.UpdateElement();
				button.removeFloat();
				//return new Vector2(960 - button.VirtualRegionOnScreen.width, 0);
			}
		}
		return button.Position;
	}
	
	private void PerformButtonSlide(CornerButton button, SlideDirections direction, Action postAction)
	{
		var retinaCenter = new Vector2(480, 320);
		var buttonCenter = button.Position + new Vector2((float) button.VirtualRegionOnScreen.width / 2.0f, (float) button.VirtualRegionOnScreen.height / 2.0f );

		var directingVector = buttonCenter - retinaCenter;
		directingVector.Normalize();
		
		var sign = direction == SlideDirections.Out ? 1 : -1;
		var targetPosition =  buttonCenter + sign * directingVector * ButtonSlideDistance;
		Debug.Log(string.Format("{0}: {1}", button.Symbol, targetPosition));
		
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
			easingFunc = (x) => (float)(Math.Log(x) + 2.0f) / 2.0f;
		}
		
		var controller = new UIElementSlideController(easingFunc) {
			StartPosition = button.Position,
			TargetPosition = targetPosition,
			Duration = TimeSpan.FromMilliseconds(250)
		};
		
		if (postAction != null) {
			controller.ControllerFinished += (sender, e) => postAction.Invoke();
		}
		
		button.RemoveController(button.name);
		button.QueueController(button.name, controller);
	}

	public void ClearSymbols() {
		symbolChain = string.Empty;
		InvokeSymbolsChanged();
	}

	public void Alert(string value) {
		AlertTextEntity.ShowText(value);
	}
	
	public void Alert(string value, float time){
		AlertTextEntity.ShowText(value,time);
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
		HideExtraElements();
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
		GameCancelled(this, EventArgs.Empty);
		
	}
	public void InvokeGamePaused(){
		CancelGameButton.Visibility = true;
		ResumeGameButton.Visibility = true;
		CancelResumeText.Visibility = true;
		var handler = GameResumed;
		if (handler == null) {
			return;
		}
		GamePaused(this, EventArgs.Empty);
		
	}
}
