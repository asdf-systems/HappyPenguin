using UnityEngine;
using System;
using HappyPenguin.Entities;
using HappyPenguin.Effects;
using HappyPenguin.Spawning;
using HappyPenguin;

public sealed class GameWorldBehaviour : MonoBehaviour
{
	private readonly GUIManager guiManager;
	private readonly EffectManager effectManager;
	private readonly CreatureSpawner creatureSpawner;
	private readonly PerkSpawner perkSpawner;
	private readonly SymbolManager symbolManager;
	private readonly EntityManager entityManager;

	public string GamePlayFunction;
	
	public void Awake()
	{
		InitPlayer();
	}
	
	private void InitPlayer()
	{
		
	}
	
	public GameWorldBehaviour() {
		effectManager = new EffectManager();
		
		creatureSpawner = new CreatureSpawner();
		creatureSpawner.EntitySpawned += OnCreatureGenerated;
		
		perkSpawner = new PerkSpawner();
		//perkSpawner.PerkSpawned += OnPerkSpawned;
	}

	private void OnCreatureGenerated(object sender, EntityGeneratedEventArgs<CreatureBehaviour> e) {
		entityManager.SpawnCreature(e.Entity);
	}

	public void Update() {
		creatureSpawner.Update();
		entityManager.Update();
	}
}
