using System;
using HappyPenguin.Entities;
using System.Collections.Generic;
using UnityEngine;

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
		
		internal string GenerateSymbolChain(Range range)
		{
			string chain;
			int rnd1;
			do {
				chain = "";
				rnd1 = UnityEngine.Random.Range((int)range.From, (int)range.To+1);
				for (int i = 1; i <= rnd1; i++) {
					int rnd = UnityEngine.Random.Range(1, 5);
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
			Debug.Log(chain);
			return chain;	
		}
	}
}