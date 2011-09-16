using System;
using HappyPenguin.Entities;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace HappyPenguin.Entities
{
	public sealed class SymbolProjector 
	{
		private TargetableEntityBehaviour targetable;
		private List<Symbol> symbolProjections;
		
		public SymbolProjector (TargetableEntityBehaviour targetable){
			this.targetable = targetable;
			this.symbolProjections = new List<Symbol>();
		}
		
		public void CreateSymbols()
		{
			var node = targetable.BillboardNode;
			var symbolChain = targetable.SymbolChain;
			
			for (int i = 0; i < symbolChain.Length; i++) {
				var character = symbolChain[i];
				var gameObject = CreateGameObjectFromSymbol(character, i);
				gameObject.transform.parent = node.transform;
				gameObject.transform.localPosition = Vector3.zero;
				
			}
		}
		
		public void HighlightSymbols(int count)
		{
			var candidates = symbolProjections.Take(count);
			foreach (var candidate	in candidates) {
				candidate.IsHighlighted = true;
			}
		}
		
		public void HideSymbols()
		{
			targetable.BillboardNode.active = false;
		}
		
		public void ShowSymbols()
		{
			targetable.BillboardNode.active = true;
		}
		
		private GameObject CreateGameObjectFromSymbol(char symbol, int symbolPosition)
		{
			var name = string.Format("Symbols/Symbol{0}", symbol);
			var resource = Resources.Load(name);
			var gameObject = GameObject.Instantiate(resource, Vector3.zero, Quaternion.identity) as GameObject;
			
			StoreBehaviour(gameObject);
			
			return gameObject;
		}
		
		private void StoreBehaviour(GameObject gameObject)
		{
			var symbol = gameObject.GetComponentInChildren<Symbol>();
			if (symbol == null) {
				throw new ApplicationException("symbol behaviour not found in targetable");
			}
			
			symbolProjections.Add(symbol);
		}
	}
}



