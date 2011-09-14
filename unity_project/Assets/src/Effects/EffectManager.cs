using System;
using System.Collections.Generic;
using HappyPenguin.Collections;

namespace HappyPenguin.Effects
{
	public sealed class EffectManager
	{
		public EffectManager () {
			Effects = new BindingList<Effect>();
			//Effects.ListChanged += OnEffectsListChanged;
		}
		
		public IList<Effect> Effects {
			get;
			private set;
		}
		
		private void OnEffectsListChanged(object sender, ListChangedEventArgs e)
		{
			
		}
	}
}

