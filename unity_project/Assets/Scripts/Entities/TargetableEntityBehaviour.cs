using System;
using UnityEngine;

namespace HappyPenguin.Entities
{
	public abstract class TargetableEntityBehaviour : EntityBehaviour
	{
		private GameObject billboardNode;
		private SymbolProjector projector;

		void Awake() {
			SymbolRange = new Range(1, 4);
			projector = new SymbolProjector(this);
		}

		public Range SymbolRange { get; set; }
		
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
		
		public override void AwakeOverride ()
		{
			base.AwakeOverride();
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

