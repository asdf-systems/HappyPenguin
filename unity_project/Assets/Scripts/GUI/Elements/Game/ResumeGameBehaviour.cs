using UnityEngine;
using System.Collections;

public class ResumeGameBehaviour : InteractionBehaviour {

	public override void Click (MouseEventArgs mouse){
		GUIManager.Instance.InvokeGameResumed();
	}
}
