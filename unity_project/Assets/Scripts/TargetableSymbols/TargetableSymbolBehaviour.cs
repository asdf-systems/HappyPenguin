using UnityEngine;
using System.Collections;
using System;

public sealed class TargetableSymbolBehaviour : MonoBehaviour
{
	public Texture DefaultTexture;
	public Texture LightUpTexture;
	private bool isLit;

	public void Awake ()
	{
		renderer.material.mainTexture = DefaultTexture;
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
		renderer.material.mainTexture = LightUpTexture;
		gameObject.transform.localScale.Scale (new Vector3 (gameObject.transform.localScale.x * 1.1f, gameObject.transform.localScale.y * 1.1f, gameObject.transform.localScale.z * 1.1f));
	}

	private void LightDown ()
	{
		renderer.material.mainTexture = DefaultTexture;
		gameObject.transform.localScale.Scale (new Vector3 (gameObject.transform.localScale.x / 1.1f, gameObject.transform.localScale.y / 1.1f, gameObject.transform.localScale.z / 1.1f));
	}
}