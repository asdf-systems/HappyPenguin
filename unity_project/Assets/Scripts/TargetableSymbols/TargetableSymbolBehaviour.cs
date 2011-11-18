using UnityEngine;
using System.Collections;
using System;

public sealed class TargetableSymbolBehaviour : MonoBehaviour
{
	public Rect DefaultUvs;
	public Rect LightUpUvs;
	private bool isLit;
	private UVMoveBehaviour uvSet; 
		
	public void Awake (){
		//renderer.material.mainTexture = DefaultUvs;
		uvSet = gameObject.GetComponent<UVMoveBehaviour>() as UVMoveBehaviour;
		
		
	}
	
	void Start(){
		uvSet.newUvs = DefaultUvs;
	}

	public bool IsHighlighted {
		get { return isLit; }
		set {
			if (isLit == value) {
				return;
			}
			
			isLit = value;
			if (isLit) {
				LightUp ();
			} else {
				LightDown ();
			}
		}
	}

	public string Symbol;

	private void LightUp ()
	{
		uvSet.newUvs = LightUpUvs;
		//gameObject.transform.localScale.Scale (new Vector3 (gameObject.transform.localScale.x * 1.1f, gameObject.transform.localScale.y * 1.1f, gameObject.transform.localScale.z * 1.1f));
	}

	private void LightDown ()
	{
		uvSet.newUvs =  DefaultUvs;
		//gameObject.transform.localScale.Scale (new Vector3 (gameObject.transform.localScale.x / 1.1f, gameObject.transform.localScale.y / 1.1f, gameObject.transform.localScale.z / 1.1f));
	}
}