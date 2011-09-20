using System;
using UnityEngine;
using System.Collections.Generic;
using HappyPenguin.Effects;
using HappyPenguin.Entities;
using HappyPenguin;

namespace HappyPenguin.Entities
{
	public abstract class TargetableEntityBehaviour : EntityBehaviour
	{
		private GameObject billboardNode;
		private TargetableSymbolProjector projector;

		public Range SymbolRange { get; set; }
		public List<Effect> KillEffects {get; set;}
		
		private string symbolChain;
		public string SymbolChain
		{ 
			get{return symbolChain;} 
			set{
				if (symbolChain == value) {
					return;
				}
				symbolChain = value;
				projector.CreateSymbols();
			} 
		}

		public void HighlightSymbols(int count) {
			projector.HighlightSymbols(count);
		}
		
		protected override void AwakeOverride ()
		{
			base.AwakeOverride();
			SymbolRange = new Range(1, 4);
			projector = new TargetableSymbolProjector(this);
			FindBillboardNode();
		}
		
		private void FindBillboardNode() {
			var behaviour = gameObject.GetComponentInChildren<BillboardBehaviour>();
			if (behaviour == null) {
				throw new ApplicationException("billboard behaviour not found on targetable entity");
			}
			BillboardNode = behaviour.gameObject;
		}

		public void ShowSymbols() {
			projector.ShowSymbols();
		}

		public void HideSymbols() {
			  projector.HideSymbols();
		}
		
		public GameObject BillboardNode {
			get;
			private set;
		}
	}
}

