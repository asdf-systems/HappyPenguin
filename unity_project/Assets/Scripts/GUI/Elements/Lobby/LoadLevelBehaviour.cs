using UnityEngine;
using System.Collections;

public class LoadLevelBehaviour : InteractionBehaviour  {
		
	public int Level = 0;
	
	public override void Click(MouseEventArgs e){
		Application.LoadLevel(Level);
	}
}
