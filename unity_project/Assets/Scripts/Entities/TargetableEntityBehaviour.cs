using System;
using UnityEngine;

namespace HappyPenguin.Entities
{
	public abstract class TargetableEntityBehaviour : EntityBehaviour
	{
		private GameObject billboardNode;

		void Awake() {
			SymbolRange = new Range(1, 4);
		}

		public Range SymbolRange { get; set; }
		public string SymbolChain { get; set; }

		public void HighlightSymbols(int count) {
			
		}
		
		public override void AwakeOverride ()
		{
			base.AwakeOverride();
			FindBillboardNode();
		}
		
		private void FindBillboardNode()
		{
			
		}

		public void ShowSymbols() {
			
		}

		public void HideSymbols() {
			  
		}

		public GameObject GetBillboardNode() {
			return billboardNode;
		}
	}
}

