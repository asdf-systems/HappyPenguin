using UnityEngine;
using System.Collections;
using System;
public class AlertTextPanel : TextPanel {
	
	public  int ShowTimeInSeconds = 8;
	
	public string help_alert = "ONLY IN DEBUGMODUS:";
	public bool ShowAlways; 
	
	protected bool textShow = false;
	public Timer timer1{
		get;
		private set;
		
	}
	
	void Awake(){
		AwakeOverride();
	}
	
	void Start(){
		StartOverride();
	}
	
	protected override void AwakeOverride(){
		base.AwakeOverride();
		initTimer();
		
		
	}
	
	private void initTimer(){
		if(timer1 == null)
			timer1 = new Timer(ShowTimeInSeconds);
		timer1.TimerFinished += OnTimerFinished;
	}

	
	void Update(){
		UpdateOverride();
	}
	
	protected override void UpdateOverride(){
		base.UpdateOverride();
		Visibility = textShow;
#if UNITY_EDITOR
		Visibility |= ShowAlways;
#endif
	}
	
	void OnGUI(){
		OnGUIOverride();
	}
	
	protected override void OnGUIOverride(){
		if(textShow || ShowAlways){
			base.OnGUIOverride();	
		}
	}
	
	public void ShowText(string value){
		initTimer();
		textShow = true;
		Text = value;
		timer1.StartTimer();
	}
	
	public void HideText(){
		textShow = false;
	}
	
	private void OnTimerFinished(object sender, EventArgs e){
		textShow = false;
	}
}
