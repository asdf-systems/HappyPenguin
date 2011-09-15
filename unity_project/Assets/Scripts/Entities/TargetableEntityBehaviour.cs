using System;
using UnityEngine;

namespace HappyPenguin.Entities
{
	public abstract class TargetableEntityBehaviour : EntityBehaviour
	{
		private GameObject billboardNode;

		public TargetableEntityBehaviour() {
			SymbolRange = new Range(1, 4);
		}

		public Range SymbolRange { get; set; }

		public string SymbolChain { get; set; }

		public override void AwakeOverride() {
			//InitBillboardNode();
		}

		public void HighlightSymbols(int count) {
			
		}

		public void ShowSymbols() {
			
		}

		public void HideSymbols() {
			  
		}

		private void InitBillboardNode() {
			var resource = Resources.Load("Symbols/BillboardNode");
			billboardNode = resource as GameObject; 
			billboardNode.transform.parent = gameObject.transform;
			GameObject.Instantiate(billboardNode, Vector3.up * 20, Quaternion.identity);
		}

		public GameObject GetBillboardNode() {
			return billboardNode;
		}
	}
}

