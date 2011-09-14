using UnityEngine;
using System;
using HappyPenguin.Entities;
using HappyPenguin.Effects;
using HappyPenguin.Spawning;
using HappyPenguin;

	public sealed class GameWorldManager : MonoBehaviour
	{
		private GUIManager guiManager;
		private EffectManager effectManager;
		private CreatureSpawner creatureSpawner;

		public GameWorldManager () {
			effectManager = new EffectManager ();
			creatureSpawner = new CreatureSpawner ();
			creatureSpawner.EntitySpawned += OnCreatureSpawned;
		}

		public event EventHandler<ButtonRotationRequestedEventArgs> ButtonRotationRequested;
		private void InvokeButtonRotationRequested () {
			var handler = ButtonRotationRequested;
			if (handler == null) {
				return;
			}
			
			var e = new ButtonRotationRequestedEventArgs ();
			ButtonRotationRequested (this, e);
		}

		private void OnCreatureSpawned (object sender, EntitySpawnedEventArgs<CreatureBehaviour> e) {
			
		}
	
	public void Update()
	{
		creatureSpawner.Update();
	}
	
}
