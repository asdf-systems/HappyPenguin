using System;

namespace HappyPenguin.Entities
{
	public abstract class TargetableEntityBehaviour : EntityBehaviour
	{
		public TargetableEntityBehaviour () {
			SymbolRange = new Range(1, 4);	
		}
		
		public Range SymbolRange {
			get;
			set;
		}
		
		public string SymbolChain {
			get;
			set;
		}
	}
}

