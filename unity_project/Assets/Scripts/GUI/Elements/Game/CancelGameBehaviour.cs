using UnityEngine;
using System.Collections;

public class CancelGameBehaviour : InteractionBehaviour {

	public override void Click (MouseEventArgs mouse){
		GUIManager.Instance.InvokeGameCanceld();
	}
}
