using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIManager :MonoBehaviour {
	
	public Camera PlayerCam;
	public bool active{get;set;}
	
	// This means the Resolution what all absolute Values setuped for (like Button positions etc.)
	public int TargetScreenHeight;
	public int TargetScreenWidth;
	
	public GUISkin guiSkin;
	
	// Button Styles
	public GUIStyle newGame_btn_style;
	public GUIStyle menuTop_style;
	public GUIStyle menuRight_style;
	public GUIStyle credits_btn_style;
	public GUIStyle options_btn_style;
	public GUIStyle highscore_btn_style;
	
	
	private Stack<GUIState> mGUIStates;
	
	// Use this for initialization
	void Start () {
		
	}
	
	void Awake(){
		mGUIStates = new Stack<GUIState>();	
		mGUIStates.Push(new MainGUS(this, PlayerCam));
		active = true;
		
	}
	void OnGUI(){
		GUI.skin = guiSkin;
		if(mGUIStates.Count > 0 && active)
			mGUIStates.Peek().OnGUI();	
		
	}
	
	public void pushGUIState(GUIState state){
		mGUIStates.Push(state);
		
	}
	
	public GUIState popGUIState(){
		return mGUIStates.Pop();
		
	}
	
	
}
