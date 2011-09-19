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
		
		public void Update(GameWorldBehaviour gameWorld){
			foreach(Effect e in Effects){
				e.Update(gameWorld);
				if(e.TimeRemaining == TimeSpan.Zero){
					e.Stop();
					Effects.Remove(e);
				}
			}	
		}
		
		public void RegisterEffect(Effect e){
			Effects.Add(e);
		}
		
		public IList<Effect> Effects {
			get;
			private set;
		}
		
	}
}

