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
			entity.SymbolChain = GenerateSymbolChain(entity.SymbolRange);
			targets.Add(entity.SymbolChain, entity);
		}
		
		public void VoidTargetable(TargetableEntityBehaviour entity)
		{
			targets.Remove(entity.SymbolChain);
		}
		
		private string GenerateSymbolChain(Range range)
		{
			return "QEYC";
		}
	}
}

