using UnityEngine;
using System;
using HappyPenguin.Entities;
using HappyPenguin.Effects;
using HappyPenguin.Spawning;
using HappyPenguin;

public sealed class GameWorldBehaviour : MonoBehaviour
{
	private Camera _playerCamera;
	
	private readonly GUIManager guiManager;
	//private readonly EffectManager effectManager;
	private readonly CreatureSpawner creatureSpawner;
	//private readonly PerkSpawner perkSpawner;
	private readonly TargetableSymbolManager symbolManager;
	private readonly EntityManager entityManager;
	
	public string GamePlayFunction;

	public GameWorldBehaviour() {
		entityManager = new EntityManager();
		
		//effectManager = new EffectManager();
		
		creatureSpawner = new CreatureSpawner();
		creatureSpawner.EntitySpawned += OnCreatureGenerated;
		
		
		//perkSpawner = new PerkSpawner();
		//perkSpawner.PerkSpawned += OnPerkSpawned;
	}

	public void Awake() {
		InitPlayer();
		InitSpawnPoint();
	}
	
	private void InitPlayer() {
		var player = gameObject.GetComponentInChildren<PlayerBehaviour>();
		if (player == null) {
			throw new ApplicationException("player component not found");
		}
		
		entityManager.SetPlayer(player);
	}
	
	private void InitSpawnPoint()
	{
		var spawnPoint = gameObject.GetComponentInChildren<SpawnPointBehaviour>();
		if (spawnPoint == null) {
			throw new ApplicationException("spawn point component not found");
		}
		
		entityManager.SetSpawnPoint(spawnPoint);
	}

	private void OnCreatureGenerated(object sender, EntityGeneratedEventArgs<CreatureTypes> e) {
		entityManager.SpawnCreature(e.EntityType);
	}

	public void Update() {
		creatureSpawner.Update();
	}
}
