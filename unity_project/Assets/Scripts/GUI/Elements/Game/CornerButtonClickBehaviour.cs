using UnityEngine;
using System.Collections;

public class CornerButtonClickBehaviour: InteractionBehaviour{

	public string Symbol;
	
	public override void Click(MouseEventArgs e){
		if(Symbol == string.Empty)
			Debug.LogWarning("Button " + gameObject.name + " has no symbol set!");
		
		GUIManager.Instance.NotifyButtonHit(Symbol);

	}
}
