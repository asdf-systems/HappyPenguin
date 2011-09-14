using System;
using HappyPenguin.Entities;
using System.Collections.Generic;

namespace HappyPenguin
{
	public sealed class SymbolManager
	{
		private Dictionary<string, TargetableEntityBehaviour> targets;
		
		public SymbolManager ()
		{
			targets = new Dictionary<string, TargetableEntityBehaviour>();
		}
		
		public void RegisterTargetable(TargetableEntityBehaviour entity)
		{
			
		}
		
		public void VoidTargetable(TargetableEntityBehaviour entity)
		{
			
		}
		
		public string GenerateSymbolChain(int min, int max)
		{
			return string.Empty;
		}
	}
}

