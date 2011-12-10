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
