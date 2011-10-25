using System;

namespace Pux
{
	public class SymbolEventArgs : EventArgs
	{
		public string SymbolChain{
			get;
			private set;
		}
		
		public SymbolEventArgs(string symbols) {
			SymbolChain = symbols;
		}
	}
}

