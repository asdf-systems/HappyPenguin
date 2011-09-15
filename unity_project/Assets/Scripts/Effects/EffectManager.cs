using System;
using System.Collections.Generic;
using HappyPenguin.Collections;

namespace HappyPenguin.Effects
{
	public sealed class EffectManager
	{
		public EffectManager () {
			Effects = new ObservableList<Effect>();
			//Effects.ListChanged += OnEffectsListChanged;
		}
		
		public IList<Effect> Effects {
			get;
			private set;
		}
		
	}
}

