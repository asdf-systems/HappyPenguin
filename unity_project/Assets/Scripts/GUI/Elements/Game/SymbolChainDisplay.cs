using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pux;
using Pux.Resources;

public class SymbolChainDisplay: Panel{
		
	
	public Rect SignRegion;
	public int signPosXStep;
	
	private List<Panel> symbols;
	
	
	void Awake(){
		AwakeOverride();
	}
	
	void OnDestroy(){
		OnDestroyOverride();
	}
	protected virtual void AwakeOverride(){
		base.AwakeOverride();
		
	}
	
	void Start(){
		GUIManager.Instance.SymbolsChanged += OnSymbolsChanged;
		symbols = new List<Panel>();
		
	}
	
	protected override void UpdateOverride(){
		base.UpdateOverride();
#if UNITY_EDITOR
		if(activeScreen.DebugModus)
			updateSymbolChain();
#endif
	}
	void OnSymbolsChanged(object sender, SymbolEventArgs e){
		Debug.Log("New Symbols: " + e.SymbolChain + "XX");
		if(e.SymbolChain == string.Empty){
			
			clear();
			return;
		}
		
		char symbol =  e.SymbolChain[e.SymbolChain.Length-1];
		Panel sign = ResourceManager.CreateInstance<GameObject>("Symbols/Sign"+symbol).GetComponent<Panel>();
		if(sign == null)
			Debug.LogError("Sign " + symbol + " is unkown");
		else{ 
			symbols.Add(sign);		
			sign.transform.parent = activeScreen.transform;
			sign.Create();
			updateSymbolChain();
		}
		
		 
		
	}
	
	private void clear(){
		
		for(int i=0; i < symbols.Count; i++){
			Debug.LogWarning("Clean Sybols");
			GameObject.Destroy(symbols[i].gameObject);
		}
		symbols.Clear();
		
	}
	void updateSymbolChain(){
		float signPosX = VirtualRegionOnScreen.x + SignRegion.x;
		
		for(int i = 0; i < symbols.Count; i++){
			var panel = symbols[i];
			panel.VirtualRegionOnScreen = new Rect (signPosX,SignRegion.y+ VirtualRegionOnScreen.y,SignRegion.width,SignRegion.height);
			panel.UpdateElementOnScreen();
			signPosX += signPosXStep;
			
		}
	}

}
