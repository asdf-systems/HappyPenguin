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
	
	void Start(){
		StartOverride();
	}
	
	protected override void StartOverride(){
		base.StartOverride();
		GUIManager.Instance.SymbolsChanged += OnSymbolsChanged;
		symbols = new List<Panel>();
	}
	
	void Update(){
		UpdateOverride();
	}
	protected override void UpdateOverride(){
		base.UpdateOverride();
#if UNITY_EDITOR
		if(activeScreen.DebugModus)
			updateSymbolChain();
#endif
		/*if(firstUpdate){
			firstUpdate = false;
			removeFloat();
		}*/
			
	}
	void OnSymbolsChanged(object sender, SymbolEventArgs e){
		if(e.SymbolChain == string.Empty){
			
			clear();
			return;
		}
		
		char symbol =  e.SymbolChain[e.SymbolChain.Length-1];
		Panel sign = ResourceManager.CreateInstance<GameObject>("Symbols/Sign"+symbol).GetComponent<Panel>();
		if(sign == null)
			EditorDebug.LogError("Sign " + symbol + " is unkown");
		else{ 
			symbols.Add(sign);		
			sign.transform.parent = this.transform;
			sign.CreateElement();
			sign.UpdateParent();
			updateSymbolChain();
			activeScreen.UpdateElement(); // Update Screeen because of new Objects
		}
		
		 
		
	}
	
	private void clear(){
		
		for(int i=0; i < symbols.Count; i++){
			GameObject.Destroy(symbols[i].gameObject);
		}
		symbols.Clear();
		
	}
	
	/*public override void UpdateElement (){
		base.UpdateElement();
		updateSymbolChain();
	}*/
	
	void updateSymbolChain(){
		float signPosX = SignRegion.x;
		
		for(int i = 0; i < symbols.Count; i++){
			var panel = symbols[i];
			panel.VirtualRegionOnScreen = new Rect (signPosX,SignRegion.y,SignRegion.width,SignRegion.height);
			//EditorDebug.Log("BEFORE Sign Real Pos: " + panel.RealRegionOnScreen);
			panel.UpdateElement();
			//EditorDebug.Log("Sign Real Pos: " + panel.RealRegionOnScreen);
			signPosX += signPosXStep;
			
		}
	}

}
