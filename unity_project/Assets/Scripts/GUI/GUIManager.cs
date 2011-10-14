using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HappyPenguin;
using System;

public class GUIManager : GUIStatics {

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
	
	public AlertTextBehaviour TextEntity;
	
	private Time textTimer;
	
	public enum Directions{Left , Right};
	private List<Vector2> positions;
	
	public event EventHandler<SymbolEventArgs> SymbolsChanged;
	public event EventHandler<SwipeEventArgs> SwipeCommitted;
	
	// Use this for initialization
	void Start () {
		Init();
		LoadButtons();
		Reset();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void Init(){
		positions = new List<Vector2>();
		TextEntity = gameObject.GetComponentInChildren<AlertTextBehaviour>();
		pointsAndLifeDisplay = gameObject.GetComponentInChildren<PointsAndLifeDisplay>();
	}
	
	private void Reset(){
		buttonC.positionX = (int)buttonCpos.x;
		buttonC.positionY = (int)buttonCpos.y;
		
		buttonE.positionX = (int)buttonEpos.x;
		buttonE.positionY = (int)buttonEpos.y;
		
		buttonQ.positionX = (int)buttonQpos.x;
		buttonQ.positionY = (int)buttonQpos.y;
		
		buttonY.positionX = (int)buttonYpos.x;
		buttonY.positionY = (int)buttonYpos.y;
		
	}
	private void LoadButtons(){
		
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
	}
	
	private void UpdatePositions(){
		
		buttonC.positionX = (int)positions[0].x;
		buttonC.positionY = (int)positions[0].y;
		
		buttonE.positionX = (int)positions[1].x;
		buttonE.positionY = (int)positions[1].y;
		
		buttonQ.positionX = (int)positions[2].x;
		buttonQ.positionY = (int)positions[2].y;
		
		buttonY.positionX = (int)positions[3].x;
		buttonY.positionY = (int)positions[3].y;
	}
	
	public void RotateLeft(){
		Vector2 tmp = positions[positions.Count-1];
		positions.Insert(0,tmp);
		positions.RemoveAt(positions.Count-1);
		UpdatePositions();
	}
	
	public void RotateRight(){
		positions.Add(positions[0]);
		positions.RemoveAt(0);
		UpdatePositions();
	}
	
	public void ClearSymbols(){
		symbolChain = string.Empty;
		InvokeSymbolsChanged();
	}
	
	public void Alert(string value){
		TextEntity.ShowText(value);
	}
	
	public void NotifyButtonQHit(){
		symbolChain += "Q";
		InvokeSymbolsChanged();
	}
	
	public void NotifyButtonEHit(){
		symbolChain += "E";
		InvokeSymbolsChanged();
	}
	
	public void NotifyButtonYHit(){
		symbolChain += "Y";
		InvokeSymbolsChanged();
	}
	
	public void NotifyButtonCHit(){
		symbolChain += "C";
		InvokeSymbolsChanged();
	}
	
	public void DisplayPoints(float points){
		if(pointsAndLifeDisplay == null)
			pointsAndLifeDisplay = gameObject.GetComponentInChildren<PointsAndLifeDisplay>();
		pointsAndLifeDisplay.Points = points;
	}
	
	public void DisplayLife(float life){
		if(pointsAndLifeDisplay == null)
			pointsAndLifeDisplay = gameObject.GetComponentInChildren<PointsAndLifeDisplay>();
		pointsAndLifeDisplay.Life = life;
	}
	
	public void PreSwipeCommitted(Directions direction){
		InvokeSwipeCommitted(direction);
	}
	
	private void InvokeSymbolsChanged(){
		var handler = SymbolsChanged;
		if (handler == null) {
				return;
		}
			
		var e = new SymbolEventArgs(symbolChain);
		SymbolsChanged(this, e);
	}
	
	private void InvokeSwipeCommitted(Directions direction){
		var handler = SwipeCommitted;
		if (handler == null) {
				return;
		}
			
		var e = new SwipeEventArgs(direction , symbolChain);
		SwipeCommitted(this, e);
	}
}
