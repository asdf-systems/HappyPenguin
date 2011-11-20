using UnityEngine;
using System.Collections;
using Pux.Resources;

public class Numbers : Panel {

	public string points{
		get;
		set;
	}

	public int textureSize;

	// Use this for initialization
	void Awake () {
		points = string.Empty;
	}

	public override void createGUIElement(){
		
		int number =0;
		int xOff = 0;
		for(int i=0; i < 7  && i < points.Length; i++){
			Panel sign = ResourceManager.CreateInstance<GameObject>("Numbers/number"+number.ToString()).GetComponent<Panel>();
			number = int.Parse(""+points[i]);
			sign.VirtualRegionOnScreen.x  += xOff; 
			xOff += (int)(sign.VirtualRegionOnScreen.width) - 15;
		}

	}
}
