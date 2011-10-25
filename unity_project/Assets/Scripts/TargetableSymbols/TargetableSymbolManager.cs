using System;
using Pux.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace Pux
{
	public sealed class TargetableSymbolManager
	{
		private Dictionary<string, TargetableEntityBehaviour> targets;
		private readonly System.Random random;
		
		public TargetableSymbolManager ()
		{
			targets = new Dictionary<string, TargetableEntityBehaviour>();
			random = new System.Random();
		}
		
		public void RegisterTargetable(TargetableEntityBehaviour entity)
		{
			if (entity == null) {
				Debug.Log("perk still null");
				return;
			}
			
			entity.SymbolChain = GenerateSymbolChain(entity.SymbolRange);
			targets.Add(entity.SymbolChain, entity);
		}
		
		public void VoidTargetable(TargetableEntityBehaviour entity)
		{
			targets.Remove(entity.SymbolChain);
		}
		
	
		internal string GenerateSymbolChain(Range range)
		{
			string chain = string.Empty; 
			int rnd1;
			do {
				chain = string.Empty;
				rnd1 = random.Next((int)range.From, (int)range.To+1);
				for (int i = 1; i <= rnd1; i++) {
					int rnd = random.Next(1, 5);
				
					switch (rnd) {
						case(1):
							chain+= "Q";
							break;
						case(2):
							chain+= "E";
							break;
						case(3):
							chain+= "Y";
							break;
						case(4):
							chain+= "C";
							break;
					}
				}
			} while (targets.ContainsKey(chain));
			return chain;	
		}
	}
}