using UnityEngine;
using System;
using HappyPenguin.Entities;
using HappyPenguin.Effects;
using HappyPenguin.Spawning;
using HappyPenguin;

public sealed class GameWorldManager : MonoBehaviour
{
	private readonly GUIManager guiManager;
	private readonly EffectManager effectManager;
	private readonly CreatureSpawner creatureSpawner;
	private readonly PerkSpawner perkSpawner;
	private readonly SymbolManager symbolManager;
	private readonly EntityManager entityManager;

	public GameWorldManager ()
	{
		effectManager = new EffectManager ();
		
		creatureSpawner = new CreatureSpawner ();
		creatureSpawner.EntitySpawned += OnCreatureGenerated;
		
		perkSpawner = new PerkSpawner ();
		//perkSpawner.PerkSpawned += OnPerkSpawned;
	}

	private void OnCreatureGenerated (object sender, EntitySpawnedEventArgs<CreatureBehaviour> e)
	{
		
	}
	
	private void OnPerkGenerated(object sender, EntitySpawnedEventArgs<PerkBehaviour> e)
	{
		
	}

	public void Update ()
	{
		creatureSpawner.Update ();
	}
	
}
