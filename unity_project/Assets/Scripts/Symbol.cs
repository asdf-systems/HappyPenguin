using UnityEngine;
using System.Collections;
using System;

public class Symbol : MonoBehaviour {
	
	public Texture DefaultTexture;
	public Texture LightUpTexture;
	private bool changed = false;
	
	private TimeSpan timeSinceLightUp = TimeSpan.Zero;
	
	public bool IsHighlighted {
		get;
		set;
	}
	
	private void LightUpSymbols(){
		renderer.material.SetTexture("LightUpTexture",LightUpTexture);
		gameObject.transform.localScale.Scale(new Vector3(gameObject.transform.localScale.x * 1.1f,
		                                                  gameObject.transform.localScale.y * 1.1f,
		                                                  gameObject.transform.localScale.z * 1.1f));
		
	}
	
	private void SetDefaultSymbols(){
		renderer.material.SetTexture("DefaultTexture",DefaultTexture);
		gameObject.transform.localScale.Scale(new Vector3(gameObject.transform.localScale.x / 1.1f,
		                                                  gameObject.transform.localScale.y / 1.1f,
		                                                  gameObject.transform.localScale.z / 1.1f));
	}
}
