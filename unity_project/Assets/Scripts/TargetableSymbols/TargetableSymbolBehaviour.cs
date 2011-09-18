using UnityEngine;
using System.Collections;
using System;

public sealed class TargetableSymbolBehaviour : MonoBehaviour
{
	public Texture DefaultTexture;
	public Texture LightUpTexture;
	private bool isLit;

	public bool IsHighlighted {
		get { return isLit; }
		set {
			if (isLit == value) {
				return;
			}
			
			isLit = value;
			if (isLit) {
				LightUp();
			} else {
				LightDown();
			}
		}
	}

	private void LightUp() {
		renderer.material.SetTexture("LightUpTexture", LightUpTexture);
		gameObject.transform.localScale.Scale(new Vector3(gameObject.transform.localScale.x * 1.1f, gameObject.transform.localScale.y * 1.1f, gameObject.transform.localScale.z * 1.1f));
	}

	private void LightDown() {
		renderer.material.SetTexture("DefaultTexture", DefaultTexture);
		gameObject.transform.localScale.Scale(new Vector3(gameObject.transform.localScale.x / 1.1f, gameObject.transform.localScale.y / 1.1f, gameObject.transform.localScale.z / 1.1f));
	}
}
