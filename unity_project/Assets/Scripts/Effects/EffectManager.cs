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
			List<Effect> todelete = new List<Effect>();
			foreach(Effect e in Effects){
				e.Update(gameWorld);
				if(e.TimeRemaining == TimeSpan.Zero){
					e.Stop();
					todelete.Add(e);
				}
			}
			foreach(Effect e in todelete){
				Effects.Remove(e);
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

