using UnityEngine;
using System.Collections;
using System;

public class Symbol : MonoBehaviour {
	
	public Texture DefaultTexture;
	public Texture LightUpTexture;
	private bool changed = false;
	
	private TimeSpan timeSinceLightUp = TimeSpan.Zero;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		if (changed) {
//			transform.Rotate(Vector3.right * Time.deltaTime * 50);
//			transform.Rotate(Vector3.left * Time.deltaTime * 100);
//			transform.Rotate(Vector3.right * Time.deltaTime * 50);
//		}
	}
	
	public void LightUp(bool lightUpState){
		if (lightUpState) {
			LightUpSymbols();
			changed = true;
		}
		else {
			SetDefaultSymbols();
			changed = false;
		}
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
