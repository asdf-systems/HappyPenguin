using System;

namespace HappyPenguin.Entities
{
	public abstract class TargetableEntityBehaviour : EntityBehaviour
	{
		public TargetableEntityBehaviour () {
			
		}
		
		public Range SymbolRange {
			get;
			internal set;
		}
		
		public string SymbolChain {
			get;
			set;
		}
	}
}

