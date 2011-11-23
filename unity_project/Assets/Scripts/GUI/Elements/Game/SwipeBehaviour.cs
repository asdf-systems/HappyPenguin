using System;
using UnityEngine;

public class SwipeBehaviour: InteractionBehaviour{

	public override void Swipe(MouseEventArgs e){
		
		float distanceX = e.MoveDirection.x;
		float distanceY = e.MoveDirection.y;
		
		
		bool swipeLeft = false;
		bool swipeRight = false;
		bool swipeUp = false;
		bool swipeDown = false;
		
		if(Math.Abs(distanceX) > Math.Abs(distanceY)){
			if(distanceX > 0)
				swipeRight = true;
			else
				swipeLeft = true;
		} else {
			if(distanceY > 0)
				swipeDown = true;
			else
				swipeUp = true;
		}
		if(swipeLeft) {
			GUIManager.Instance.ClearSymbols();
		}
		else if(swipeRight){
			GUIManager.Instance.PreSwipeCommitted(e.MoveDirection);
		} else if(swipeDown){
			GUIManager.Instance.InvokeGamePaused();
		} else if(swipeUp){
			GUIManager.Instance.InvokeGameResumed();
		}
	}
}


