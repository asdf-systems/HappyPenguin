using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pux.Resources;

public class Numbers : MonoBehaviour {

	
	private string points;
	private List<Panel> numberPanels;
	
	private CameraScreen activeScreen;
	
	public Rect VirtualRegionOnScreen;
	
	public string Points{
		get{
			return points;	
		}
		set{
			points = value;
			updateGUIElement();
		}
	}

	void Awake(){
		numberPanels = new List<Panel>();
		Points = string.Empty;
		activeScreen = CameraScreen.GetScreenForObject(this.gameObject);
	}
	
#if UNITY_EDITOR
	void Update(){
		if(activeScreen.DebugModus)
			updateGUIElementPosition();
	}
#endif

	private void updateGUIElement(){
		int number =0;
		
		for(int i=0; i < 7  && i < Points.Length; i++){
			number = int.Parse(""+Points[i]);
			Panel sign = ResourceManager.CreateInstance<GameObject>("Numbers/number"+number.ToString()).GetComponent<Panel>();
			sign.transform.parent = activeScreen.transform;
			sign.Create();
			numberPanels.Add(sign);
			updateGUIElementPosition();
			
		}

	}
	
	private void updateGUIElementPosition(){
		int xOff = 0;
		for(int i=0; i < numberPanels.Count; i++){
			var sign = numberPanels[i];
			sign.VirtualRegionOnScreen = VirtualRegionOnScreen;
			sign.VirtualRegionOnScreen.x  += xOff; 
			xOff += (int)(sign.VirtualRegionOnScreen.width) - 15;	
		}
		
	}
}
