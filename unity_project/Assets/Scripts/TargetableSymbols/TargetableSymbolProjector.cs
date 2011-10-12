using System;
using HappyPenguin.Entities;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace HappyPenguin.Entities
{
	public sealed class TargetableSymbolProjector 
	{
		private TargetableEntityBehaviour targetable;
		private List<TargetableSymbolBehaviour> symbolProjections;
		
		public TargetableSymbolProjector (TargetableEntityBehaviour targetable){
			this.targetable = targetable;
			this.symbolProjections = new List<TargetableSymbolBehaviour>();
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
				gameObject.transform.localRotation = Quaternion.identity;
				
				var z = (i * 7) - (symbolChain.Length * 7 / 2);
				var offset = new Vector3(0, 0, z);
				gameObject.transform.Translate(offset, gameObject.transform.parent);
				
				gameObject.transform.Translate(new Vector3(0, 8, 0), gameObject.transform.parent);
				gameObject.transform.RotateAroundLocal(new Vector3(0, 1, 0),(float) Math.PI / 2.0f);
			}
		}
		
		public void DarkenSymbols()
		{
			foreach (var candidate in symbolProjections) {
				candidate.IsHighlighted = false;
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
			targetable.BillboardNode.SetActiveRecursively(false);
		} 
		
		public void ShowSymbols()
		{
			targetable.BillboardNode.SetActiveRecursively(true);
		}
		
		private GameObject CreateGameObjectFromSymbol(char symbol, int symbolPosition)
		{
			var name = string.Format("Symbols/TargetableSymbol{0}", symbol);
			var resource = Resources.Load(name);
			var gameObject = GameObject.Instantiate(resource, Vector3.zero, Quaternion.identity) as GameObject;
			
			StoreBehaviour(gameObject);
			
			return gameObject;
		}
		
		private void StoreBehaviour(GameObject gameObject)
		{
			var symbol = gameObject.GetComponentInChildren<TargetableSymbolBehaviour>();
			if (symbol == null) {
				throw new ApplicationException("symbol behaviour not found in targetable");
			}
			
			symbolProjections.Add(symbol);
		}
	}
}



