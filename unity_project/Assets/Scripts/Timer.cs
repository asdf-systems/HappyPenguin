using UnityEngine;
using System.Collections;
using System;

public class Timer {

	private float currentTime = 0;
	private float maxTime;
	private bool run = false;
	
	public event EventHandler TimerFinished;
	
	public Timer(){
		init();
	}
	
	public Timer(float timeInSeconds){
		init();
		maxTime = timeInSeconds;
	}
	
	~Timer(){
		TimeBehaviour.Instance.RemoveTimer(this);
	}
	
	// Update is called once per frame
	public void Update () {
		if(run){
			currentTime += Time.deltaTime;
			if(currentTime >= maxTime){
				InvokeTimerFinished();
				StopTimer();
			}
		}
			
		
	}
	
	public void StartTimer(float timeInSeconds){
		StopTimer();
		TimeBehaviour.Instance.AddTimer(this);
		maxTime = timeInSeconds;
		run = true;
	}
	
	public void StartTimer(){
		StartTimer(maxTime);
	}
	
	public void StopTimer(){
		currentTime = 0;
		run = false;
		TimeBehaviour.Instance.RemoveTimer(this);
	}
	
	private void init(){
		TimeBehaviour.Instance.AddTimer(this);
	}
	
	private void InvokeTimerFinished(){
		var handler = TimerFinished;
		if (handler == null) {
			return;
		}
		handler(this, null);
	}
}
