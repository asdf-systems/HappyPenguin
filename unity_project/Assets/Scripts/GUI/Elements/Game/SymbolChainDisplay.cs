using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HappyPenguin;

public class SymbolChainDisplay: UIElementBehaviour<GUIManager>{
		
	
	public GUIStyle signQ;
	public GUIStyle signE;
	public GUIStyle signY;
	public GUIStyle signC;
	
	public int signSize;
	public int signPosXStart;
	public int signPosYStart;
	public int signPosXStep;
	
	private List<GUIStyle> symbols;
	
	void Start(){
		guiStatics.SymbolsChanged += HandleGuiStaticsSymbolsChanged;
		symbols = new List<GUIStyle>();
		
	}

	void HandleGuiStaticsSymbolsChanged(object sender, SymbolEventArgs e){
		// show Elements on GUI
		symbols.Clear();
		if(e.SymbolChain != string.Empty){
			foreach(char s in e.SymbolChain){
				switch(s){
					case 'Q':
						symbols.Add(signQ);
					break;
					case 'E':
						symbols.Add(signE);
					break;
					case 'Y':
						symbols.Add(signY);
					break;
					case 'C':
						symbols.Add(signC);
					break;
					/*case default
						Debug.Log("Sign " + s + " is unkown");
					break;*/
					
				}
				
			}
		} 
		
	}
	protected override void showElements(){
		GeneralScreenGUI.Box(guiStatics, new Rect (positionX,positionY,512,512), "", currentStyle);
		int signPosX = signPosXStart;
		int signPosY = signPosYStart;
		foreach(GUIStyle style in symbols){
			GeneralScreenGUI.Box(guiStatics, new Rect (signPosX,signPosY,signSize,signSize), "", style);					
			signPosX += signPosXStep;
		}
	}
	
	protected override void hit(){
		
	}
}
