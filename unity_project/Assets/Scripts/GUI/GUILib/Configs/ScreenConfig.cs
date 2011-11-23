using UnityEngine;
using System.Collections;

public class ScreenConfig : MonoBehaviour {

	public int TargetScreenWidth = 960;
	public int TargetScreenHeight = 640;
	public float ScreenAspect = 1.0f;
	public int[] FontSizes;
	public Font[] Fonts;
	
	public static ScreenConfig Instance{
		get;
		private set;
	}
	
	void Awake(){
		Instance = this;
	}
	
	
	
	
	
}
