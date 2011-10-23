using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HappyPenguin;
using HappyPenguin.Effects;
using HappyPenguin.UI;
using System;

public class GUIManager : GUIStatics
{
	private CornerButtonBehaviourC buttonC;
	private CornerButtonBehaviourE buttonE;
	private CornerButtonBehaviourQ buttonQ;
	private CornerButtonBehaviourY buttonY;

	private PointsAndLifeDisplay pointsAndLifeDisplay;

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
	}
	
	private void InitComponents() {
		positions = new List<Vector2>();
		TextEntity = gameObject.GetComponentInChildren<AlertTextBehaviour>();
		pointsAndLifeDisplay = gameObject.GetComponentInChildren<PointsAndLifeDisplay>();
	}

	private void Reset() {
		buttonC.positionX = (int)buttonCpos.x;
		buttonC.positionY = (int)buttonCpos.y;
		
		buttonE.positionX = (int)buttonEpos.x;
		buttonE.positionY = (int)buttonEpos.y;
		
		buttonQ.positionX = (int)buttonQpos.x;
		buttonQ.positionY = (int)buttonQpos.y;
		
		buttonY.positionX = (int)buttonYpos.x;
		buttonY.positionY = (int)buttonYpos.y;
		
	}
	private void InitButtons() {
		buttonC = gameObject.GetComponentInChildren<CornerButtonBehaviourC>();
		buttonY = gameObject.GetComponentInChildren<CornerButtonBehaviourY>();
		buttonQ = gameObject.GetComponentInChildren<CornerButtonBehaviourQ>();
		buttonE = gameObject.GetComponentInChildren<CornerButtonBehaviourE>();
		
		buttonCpos = new Vector2(buttonC.positionX, buttonC.positionY);
		buttonEpos = new Vector2(buttonE.positionX, buttonE.positionY);
		buttonQpos = new Vector2(buttonQ.positionX, buttonQ.positionY);
		buttonYpos = new Vector2(buttonY.positionX, buttonY.positionY);
		
		positions.Add(buttonCpos);
		positions.Add(buttonEpos);
		positions.Add(buttonQpos);
		positions.Add(buttonYpos);
		
		Reset();
	}

	public void PerformUIRotation(ClockRotations clockRotation)
	{
		if (clockRotation == ClockRotations.Clockwise) {
			SlideButtonsOut(() => 
			                RotateRight(() =>
			                            SlideButtonsIn(null)
			                ));
		} else{
			SlideButtonsOut(() => 
			                RotateLeft(() =>
			                            SlideButtonsIn(null)
			                ));
		}
	}

	private void RotateLeft(Action action) {
		Vector2 tmp = positions[positions.Count - 1];
		positions.Insert(0, tmp);
		positions.RemoveAt(positions.Count - 1);
		UpdatePositions();
		
		if (action == null) {
			return;
		}
		
		action.Invoke();
	}
	
	private void RotateRight(Action action) {
		positions.Add(positions[0]);
		positions.RemoveAt(0);
		UpdatePositions();
		
		if (action == null) {
			return;
		}
		
		action.Invoke();
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
		StartButtonSlide(buttonC, SlideDirections.Out, postAction);
		StartButtonSlide(buttonE, SlideDirections.Out, postAction);
		StartButtonSlide(buttonQ, SlideDirections.Out, postAction);
		StartButtonSlide(buttonY, SlideDirections.Out, postAction);
	}
	
	private void SlideButtonsIn(Action postAction)
	{
		StartButtonSlide(buttonC, SlideDirections.In, postAction);
		StartButtonSlide(buttonE, SlideDirections.In, postAction);
		StartButtonSlide(buttonQ, SlideDirections.In, postAction);
		StartButtonSlide(buttonY, SlideDirections.In, postAction);
	}
	
	private void StartButtonSlide(UIElementBehaviour<GUIManager> button, SlideDirections direction, Action postAction)
	{
		var center = new Vector2(Screen.width / 2, Screen.height / 2);
		
		var cDir = button.Position - center;
		cDir.Normalize();
		var targetPosition = cDir * ButtonSlideDistance + button.Position;
		
		var controller = new UIElementSlideController(direction == SlideDirections.Out ? targetPosition : -targetPosition);
		if (postAction != null) {
			controller.ControllerFinished += (sender, e) => postAction.Invoke();
		}
		
		button.Controllers.Add(controller);
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
		if (pointsAndLifeDisplay == null)
			pointsAndLifeDisplay = gameObject.GetComponentInChildren<PointsAndLifeDisplay>();
		pointsAndLifeDisplay.Points = points;
	}

	public void DisplayLife(float life) {
		if (pointsAndLifeDisplay == null)
			pointsAndLifeDisplay = gameObject.GetComponentInChildren<PointsAndLifeDisplay>();
		pointsAndLifeDisplay.Life = life;
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
