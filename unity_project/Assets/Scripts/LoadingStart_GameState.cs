using UnityEngine;
using System.Collections;
using System;

public class LoadingStart_GameState : MonoBehaviour {

	public Button nextButton;
	public PlayResourceLoadingBehaviour LoadResourceBehaviour;
	public Renderer BackgroundRenderer;
	public Texture2D SecondScreenMaterial;
	
	private Timer timer;
	
	// Use this for initialization
	void Start () {
		hideNextButton();
		LoadResourceBehaviour.LoadingFinished += OnFinishedLoading;
		timer = new Timer(5);
		timer.TimerFinished += OnTimerFinished;
		timer.StartTimer();
	}

	private void OnTimerFinished (object sender, EventArgs e){
		BackgroundRenderer.material.SetTexture("_MainTex", SecondScreenMaterial);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void OnFinishedLoading(object sender, EventArgs e){
		showNextButton();
	}
	
	private void hideNextButton(){
		nextButton.Visibility = false;
	}
	
	private void showNextButton(){
		nextButton.Visibility = true;
	}
}
