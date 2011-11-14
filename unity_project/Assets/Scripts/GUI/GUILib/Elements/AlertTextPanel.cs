using UnityEngine;
using System.Collections;
using System;
public class AlertTextPanel : TextPanel {
	
	public  int ShowTimeInSeconds = 8;
	
	public string help_alert = "ONLY IN DEBUGMODUS:";
	public bool ShowAlways; 
	
	private bool textShow = false;
	private Timer timer1;
	
	void Awake(){
		AwakeOverride();
	}
	
	void Start(){
		StartOverride();
	}
	
	protected override void AwakeOverride(){
		base.AwakeOverride();
		timer1 = new Timer(ShowTimeInSeconds);
		timer1.TimerFinished += OnTimerFinished;
	}

	
	void Update(){
		UpdateOverride();
	}
	
	protected override void UpdateOverride(){
		base.UpdateOverride();
		plane.renderer.enabled = textShow;
#if UNITY_EDITOR
		plane.renderer.enabled |= ShowAlways;
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
		textShow = true;
		Text = value;
		timer1.StartTimer();
	}
	
	private void OnTimerFinished(object sender, EventArgs e){
		textShow = false;
	}
}
