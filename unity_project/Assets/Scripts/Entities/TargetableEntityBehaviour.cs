using System;
using UnityEngine;
using System.Collections.Generic;
using Pux.Effects;
using Pux.Entities;
using Pux;

namespace Pux.Entities
{
	public abstract class TargetableEntityBehaviour : EntityBehaviour
	{
		private GameObject billboardNode;
		private TargetableSymbolProjector projector;
		
		public Range SymbolRange { get; set; }
		public List<Effect> HitEffects { get; private set;}
		
		private string symbolChain;
		public string SymbolChain
		{ 
			get{ return symbolChain; } 
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
		
		public void DarkenSymbols() {
			projector.DarkenSymbols();
		}
		
		private void OnTriggerEnter(Collider c){
			TryProcessSnowballHit(c);
		}
		
		private bool TryProcessSnowballHit(Collider c)
		{
			var behaviour = c.GetComponent<SnowballBehaviour>();
			if (behaviour == null) {
				return false;
			}
			
			// check whether we actually hit the right target
			if (behaviour.DedicatedTarget != this) {
				return false;
			}
			
			InvokeTargetHit(behaviour);
			return true;
		}
		
		public event EventHandler<BehaviourEventArgs<SnowballBehaviour>> TargetHit;
		internal void InvokeTargetHit(SnowballBehaviour snowball)
		{
			var handler = TargetHit;
			if (handler == null) {
				return;
			}
			var e = new BehaviourEventArgs<SnowballBehaviour>(snowball);
			handler(this, e);
		}
		
		protected override void AwakeOverride ()
		{
			base.AwakeOverride();
			HitEffects = new List<Effect> ();
			projector = new TargetableSymbolProjector(this);
			SymbolRange = new Range(1, 4);
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

