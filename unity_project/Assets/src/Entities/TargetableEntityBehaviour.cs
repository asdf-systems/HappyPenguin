using System;

namespace HappyPenguin.Entities
{
	public abstract class TargetableEntityBehaviour : EntityBehaviour
	{
		public TargetableEntityBehaviour () {
			
		}
		
		public string SymbolChain {
			get;
			set;
		}
	}
}

