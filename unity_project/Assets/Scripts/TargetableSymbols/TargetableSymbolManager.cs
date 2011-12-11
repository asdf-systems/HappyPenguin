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
		
		public bool IsTargetableRegistered(string symbolChain){
			if (string.IsNullOrEmpty(symbolChain)) {
				return false;
			}
			return targets.ContainsKey(symbolChain);
		}
		
		public void RegisterTargetable(TargetableEntityBehaviour entity)
		{
			if (entity == null) {
				EditorDebug.Log("targetable is null");
				return;
			}
			
			
			entity.SymbolChain = GenerateSymbolChain(entity.SymbolRange);
			var message = string.Format("registering {0}: {1}", entity.SymbolChain, entity);
			EditorDebug.Log(message);
			targets.Add(entity.SymbolChain, entity);
		}
		
		public IEnumerable<TargetableEntityBehaviour> Targetables {
			get{ return targets.Values; }
		}
		
		public TargetableEntityBehaviour GetTargetable(string symbolChain){
			return targets[symbolChain];	
		}
		
		public void VoidTargetable(TargetableEntityBehaviour entity)
		{
			if (targets.ContainsKey(entity.SymbolChain)) {
				var message = string.Format("unregistering {0}: {1}", entity.SymbolChain, entity);
				EditorDebug.Log(message);
				targets.Remove(entity.SymbolChain);	
				entity.SymbolChain = string.Empty;
				return;
			}
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