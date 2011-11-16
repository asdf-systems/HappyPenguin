using System;

public class SwipeBehaviour: InteractionBehaviour{
	

	public override void Swipe(MouseEventArgs e){
		
		if(e.MoveDirection.x < 0) {
			GUIManager.Instance.ClearSymbols();
		}
		else {
			GUIManager.Instance.PreSwipeCommitted(e.MoveDirection);
		}
	}
}


