using UnityEngine;
using System.Collections;

public class TextPanel : Panel {

	public int targetFontSize;
	public string Text;
	public bool Editable = false;
	public bool MultiLine;
	public int LineLength = 1;
	public int MaxInputTextLength = 10; 
	
	public GUIStyle textStyle;
	
	public Rect TextRegion;
	private Rect realTextRegion;
	private string formatetText;
	private string lastRenderdText;
	
	protected override void AwakeOverride(){
		base.AwakeOverride();
		initTextRegion();
		
	}
	
	void Start(){
		StartOverride();
	}
	
	protected override void StartOverride(){
		base.StartOverride();
		formatMultilineText();
		lastRenderdText = Text;
	}
	public override void LayoutElement(){
		base.LayoutElement();
		initTextRegion();
	}
	
	void OnGUI(){
		OnGUIOverride();
	}
	
	protected override void OnGUIOverride(){
		base.OnGUIOverride();
		formatText();
		if(Editable)
			Text = UnityEngine.GUI.TextField(realTextRegion, formatetText, MaxInputTextLength, textStyle);
		else
			UnityEngine.GUI.Label(realTextRegion, formatetText, textStyle);

	}
	
	
	private void formatText(){
		
		if(activeScreen.DebugModus)
			initTextRegion();
		textStyle.fontSize = targetFontSize;
		int size = textStyle.fontSize;
		textStyle.fontSize = CameraScreen.GetPhysicalTextSize(size);
		
		if(Text != lastRenderdText){ // Do this every on GUI because text can be changed from outside
			formatMultilineText();
			InvokeTextChanged();
		}
#if UNITY_EDITOR
		if(activeScreen.DebugModus)
			formatMultilineText();
#endif
	}
	
	private void formatMultilineText(){
		string tmp = string.Empty;
		if(MultiLine){
			for(int i = 0; i < Text.Length; i++){
				tmp += Text[i];
				if((i+1)%LineLength == 0 && LineLength > 0)
					tmp += "\n";
			}
		} else
			tmp = Text;
		formatetText = tmp;
		lastRenderdText = Text;
	}
	
	// Caclulate the Absolute Values on the physical screen - because TextRegion is virtual an relative to the Panel Position
	private void initTextRegion(){
		realTextRegion = new Rect(VirtualRegionOnScreen.x+ TextRegion.x, 
		                            VirtualRegionOnScreen.y + TextRegion.y, TextRegion.width, TextRegion.height);
		realTextRegion = activeScreen.GetPhysicalRegionFromRect(realTextRegion);
	}
	
	private void InvokeTextChanged(){
		InteractionBehaviour[] behaviours = gameObject.GetComponents<InteractionBehaviour>() as InteractionBehaviour[];
		foreach(InteractionBehaviour ib in behaviours){
			ib.TextChanged(Text);
		}	
	}
	
}
