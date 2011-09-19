using System;
namespace AssemblyCSharp
{
	public class SwipeBehaviour: UIElementBehaviour<GUIManager>
	{
		
		public SwipeBehaviour ()
		{
			
		}
		
		protected override void swipe(GUIManager.Directions direction){
			guiStatics.PreSwipeCommitted(direction);
		}
	}
}

