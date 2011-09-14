using System;

namespace HappyPenguin
{
	public class SymbolEventArgs : EventArgs
	{
		public string symbolChain{
			get;
			private set;
		}
		
		public SymbolEventArgs(string symbols) {
			symbolChain = symbols;
		}
	}
}

