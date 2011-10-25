using System;
using Pux;
namespace Pux
{
	public class SwipeEventArgs : EventArgs
	{
		public GUIManager.Directions direction ;
		public string symbolChain;
		
		public SwipeEventArgs (GUIManager.Directions direction , string symbolChain){
			this.direction = direction;
			this.symbolChain = symbolChain;
		}
	}
}

