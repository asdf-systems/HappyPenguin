using UnityEngine;
using System.Collections;

public class GUIElement : MonoBehaviour {

	public int guiDepth;
	public GUIManager guiManager;
	
	// Active Rectangle 
	public int height;
	public int width;
	public int positionX;
	public int positionY;
	
	void OnGUI(){
		GUI.depth = guiDepth;
		showElements();
		hitTest();
	}
	
	void hitTest(){
		bool hit = false;
		// check Mouse Position against size
		 
		// check iPhone Tap against size
		
		// check Android Tap against size
		
		if(hit){
			hit();
		}
		
	}
	
	protected void showElements(){
		// Tobe overwriten in child classes
	}
	
	protected void hit(){
		// Tobe overwriten in child classes
	}
	
	
}
