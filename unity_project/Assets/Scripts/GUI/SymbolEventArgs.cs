using System;

namespace HappyPenguin
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

