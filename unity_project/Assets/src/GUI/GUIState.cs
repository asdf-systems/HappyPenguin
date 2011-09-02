using UnityEngine;
using System.Collections;

public class GUIState  {
	
	public GUIManager mGUIManager{get; set;}
	public Camera mCamera{get; set;}
	
	public GUIState(){
		mGUIManager = null;
		mCamera = null;
	}
	public GUIState(GUIManager gs, Camera cam){
		mGUIManager = gs;
		mCamera = cam;	
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnGUI(){
		//GUI.skin = mGUIManager.guiSkin;
		if(mCamera != null && mGUIManager != null)
			loadMenu();
		else
			Debug.Log("GUIManager or Camera not set on GUIState");
		
	}
	
	protected virtual void loadMenu(){
		
	}
}
