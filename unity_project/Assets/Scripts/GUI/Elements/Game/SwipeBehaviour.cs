using System;

	public class SwipeBehaviour: UIElementBehaviour<GUIManager>
	{
		
		public SwipeBehaviour ()
		{
			
		}
		
		protected override void swipe(GUIManager.Directions direction){
			if (direction == GUIManager.Directions.Left) {
				guiStatics.clearSymbols();
			}
			else {
				guiStatics.PreSwipeCommitted(direction);
			}
		}
	}

