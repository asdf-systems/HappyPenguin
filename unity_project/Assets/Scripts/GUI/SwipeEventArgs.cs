using System;
using Pux;
using UnityEngine;

namespace Pux
{
	public class SwipeEventArgs : EventArgs
	{
		public string SymbolChain;
		public Vector2 Direction;
		
		public SwipeEventArgs (Vector2 direction , string symbolChain){
			this.Direction = direction;
			this.SymbolChain = symbolChain;
		}
	}
}

