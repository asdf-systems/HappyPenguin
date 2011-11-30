using System;
using System.Collections.Generic;
using Pux.Effects;
using UnityEngine;
using System.Linq;

namespace Pux.UI
{
	public sealed class IconSlotManager
	{
		private List<IconSlotBehaviour> _iconSlots;
		
		public IconSlotManager() {
			_iconSlots = new List<IconSlotBehaviour>();
		}
		
		public void InitSpotManager()
		{
			var slots = GameObject.FindGameObjectsWithTag("icon_spot") as IEnumerable<GameObject>;
			_iconSlots.AddRange(slots.Select(x => x.GetComponent<IconSlotBehaviour>()));
		}
		
		public void DisplayEffect(Effect effect){

			var slot = FindLeftestEmptySlot();

			if(slot != null)
				slot.DisplayEffect(effect);

			slot.DisplayEffect(effect);

		}
		
		public void HideEffect(Effect effect){
		
			// make copy
			var effects = new List<Effect>();
			effects.AddRange(_iconSlots.Where(x => x.IsOccupied).Select(x => x.ActiveEffect));
			effects.Remove(effect);
			
			foreach (var slot in _iconSlots) {
				slot.Clear();
			}
			
			foreach (var e in effects) {
				DisplayEffect(e);
			}
		}
		
		private IconSlotBehaviour FindLeftestEmptySlot()
		{
			IconSlotBehaviour leftiest = null;
			foreach (var item in _iconSlots) {
				if (item.IsOccupied) {
					continue;
				}
				if (leftiest == null) {
					leftiest = item;
					continue;
				}
				leftiest = leftiest.Position.x < item.Position.x ? leftiest : item;
			}
			return leftiest;
		}
	}
}

