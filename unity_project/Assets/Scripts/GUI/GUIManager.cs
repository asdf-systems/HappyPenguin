using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pux;
using Pux.Effects;
using Pux.UI;
using Pux.Resources;
using System;

public class GUIManager : GUIStatics
{
	private CornerButtonBehaviourC buttonC;
	private CornerButtonBehaviourE buttonE;
	private CornerButtonBehaviourQ buttonQ;
	private CornerButtonBehaviourY buttonY;

	private PointsDisplay pointsDisplay;

	private string symbolChain;

	private Vector2 buttonCpos;
	private Vector2 buttonEpos;
	private Vector2 buttonQpos;
	private Vector2 buttonYpos;
	
	public float ButtonSlideDistance {
		get;
		set;
	}

	public AlertTextBehaviour TextEntity;

	private Time textTimer;

	public enum Directions
	{
		Left,
		Right
	}
	
	private List<Vector2> positions;

	public event EventHandler<SymbolEventArgs> SymbolsChanged;
	public event EventHandler<SwipeEventArgs> SwipeCommitted;
	
	private void Awake() {
		InitComponents();
		InitButtons();
		ButtonSlideDistance = 181; // magnitude of Vector2(128,128) 
	}
	
	private void InitComponents() {
		positions = new List<Vector2>();
		TextEntity = gameObject.GetComponentInChildren<AlertTextBehaviour>();
		pointsDisplay = gameObject.GetComponentInChildren<PointsDisplay>();
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
		buttonC = gameObject.GetComponentInChildren<CornerButtonBehaviourC>();
		buttonY = gameObject.GetComponentInChildren<CornerButtonBehaviourY>();
		buttonQ = gameObject.GetComponentInChildren<CornerButtonBehaviourQ>();
		buttonE = gameObject.GetComponentInChildren<CornerButtonBehaviourE>();
		
		StorePositions();
	}
	
	private void StorePositions()
	{
		positions.Clear();
		positions.Add(buttonC.Position);
		positions.Add(buttonE.Position);
		positions.Add(buttonQ.Position);
		positions.Add(buttonY.Position);
	}
	
	private int poorMansBarrier;
	private void OnButtonsSlidOut(Action action, ClockRotations rotation)
	{
		poorMansBarrier ++;
		if (poorMansBarrier < 4) {
			return;
		}
		
		StorePositions();
		SwapTextures(rotation);
		if (action != null) {
			action();
		}
	}
	
	private void SwapTextures(ClockRotations rotation)
	{
		SwapTexture(buttonC, "green", rotation);
		SwapTexture(buttonE, "red", rotation);
		SwapTexture(buttonQ, "yellow", rotation);
		SwapTexture(buttonY, "purple", rotation);
	}
	
	private void SwapTexture(UIElementBehaviour<GUIManager> button, string color, ClockRotations rotation)
	{
		var path = "iPhone/UI/";
		var ns = string.Empty;
		if (button.Position.x < 480) {
			ns = rotation == ClockRotations.Clockwise ? "bottom" : "top";
		} else {
			ns = rotation == ClockRotations.Clockwise ? "top" : "bottom";
		}
		
		var sw = string.Empty;
		if (button.Position.y > 320) {
			sw = rotation == ClockRotations.Clockwise ? "right" : "left";
		} else {
			sw = rotation == ClockRotations.Clockwise ? "left" : "right";
		}
		
		var normal = string.Format("{0}arrow_{1}_{2}_{3}", path, color, ns, sw);
		var hover = normal + "_hover";
		
		button.activeStyle.normal.background = ResourceManager.GetResource<Texture2D>(normal);
		button.hoverStyle.normal.background = ResourceManager.GetResource<Texture2D>(hover);
		button.inactiveStyle.normal.background = ResourceManager.GetResource<Texture2D>(normal);
	}

	public void PerformUIRotation(ClockRotations clockRotation)
	{
		poorMansBarrier = 0;
		if (clockRotation == ClockRotations.Clockwise) {
			SlideButtonsOut(() => OnButtonsSlidOut(RotateRight, clockRotation));
		} else{
			SlideButtonsOut(() => OnButtonsSlidOut(RotateLeft, clockRotation));
		}
	}
	
	private void OnButtonsRotated()
	{
		SlideButtonsIn();
	}

	private void RotateLeft() {
		Vector2 tmp = positions[positions.Count - 1];
		positions.Insert(0, tmp);
		positions.RemoveAt(positions.Count - 1);
		UpdatePositions();
		OnButtonsRotated();
	}
	
	private void RotateRight() {
		positions.Add(positions[0]);
		positions.RemoveAt(0);
		UpdatePositions();
		OnButtonsRotated();
	}
	
	private void UpdatePositions() {
		
		buttonC.positionX = (int)positions[0].x;
		buttonC.positionY = (int)positions[0].y;
		
		buttonE.positionX = (int)positions[1].x;
		buttonE.positionY = (int)positions[1].y;
		
		buttonQ.positionX = (int)positions[2].x;
		buttonQ.positionY = (int)positions[2].y;
		
		buttonY.positionX = (int)positions[3].x;
		buttonY.positionY = (int)positions[3].y;
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
	
	private Vector2 GetSnapPositionForButton(UIElementBehaviour<GUIManager> button)
	{
			// left
		if (button.Position.x < 480) {
			if (button.Position.y > 320) {
				return new Vector2(0, 640 - button.Height);
			} else {
				return new Vector2(0, 0);
			}
		} else {
			if (button.Position.y > 320) {
				return new Vector2(960 - button.Width, 640 - button.Height);
			} else {
				return new Vector2(960 - button.Width, 0);
			}
		}
	}
	
	private void PerformButtonSlide(UIElementBehaviour<GUIManager> button, SlideDirections direction, Action postAction)
	{
		var retinaCenter = new Vector2(480, 320);
		var buttonCenter = new Vector2(button.Position.x + (float) button.Width / 2.0f, button.Position.y + (float) button.Height / 2.0f );

		var directingVector = buttonCenter - retinaCenter;
		directingVector.Normalize();
		
		var sign = direction == SlideDirections.Out ? 1 : -1;
		var targetPosition =  buttonCenter + sign * directingVector * ButtonSlideDistance;
		
		//decentralize position
		targetPosition -= new Vector2((float)button.Width / 2.0f, (float)button.Height / 2.0f);
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
		TextEntity.ShowText(value);
	}

	public void NotifyButtonQHit() {
		symbolChain += "Q";
		InvokeSymbolsChanged();
	}

	public void NotifyButtonEHit() {
		symbolChain += "E";
		InvokeSymbolsChanged();
	}

	public void NotifyButtonYHit() {
		symbolChain += "Y";
		InvokeSymbolsChanged();
	}

	public void NotifyButtonCHit() {
		symbolChain += "C";
		InvokeSymbolsChanged();
	}

	public void DisplayPoints(float points) {
		if (pointsDisplay == null)
			pointsDisplay = gameObject.GetComponentInChildren<PointsDisplay>();
		pointsDisplay.Points = points;
	}

	public void DisplayLife(float life) {
		if (pointsDisplay == null)
			pointsDisplay = gameObject.GetComponentInChildren<PointsDisplay>();
		pointsDisplay.Life = life;
	}

	public void PreSwipeCommitted(Directions direction) {
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

	private void InvokeSwipeCommitted(Directions direction) {
		var handler = SwipeCommitted;
		if (handler == null) {
			return;
		}
		
		var e = new SwipeEventArgs(direction, symbolChain);
		SwipeCommitted(this, e);
	}
}
