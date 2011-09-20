using UnityEngine;
using System;
using System.Collections.Generic;
using HappyPenguin.Entities;
using HappyPenguin.Effects;
using HappyPenguin.Spawning;
using HappyPenguin;

public sealed class GameWorldBehaviour : MonoBehaviour
{
	private Camera _playerCamera;
	public GUIManager guiManager;
	private readonly EffectManager effectManager;

	private readonly CreatureSpawner creatureSpawner;
	private readonly PerkSpawner perkSpawner;
	private readonly TargetableSymbolManager symbolManager;
	private readonly EntityManager entityManager;

	private AttackZoneBehaviour attackZone;

	public GameWorldBehaviour() {
		entityManager = new EntityManager();
		effectManager = new EffectManager(this);
		
		creatureSpawner = new CreatureSpawner();
		creatureSpawner.EntitySpawned += OnCreatureGenerated;
		
		perkSpawner = new PerkSpawner();
		perkSpawner.EntitySpawned += OnPerkGenerated;
	}

	void OnSwipeCommitted(object sender, SwipeEventArgs e) {
		guiManager.clearSymbols();
		
		var target = entityManager.FindFittingTargetable(e.symbolChain);
		Debug.Log("committed: " + e.symbolChain);
		if (target == null) {
			Debug.Log("implemented punish player");
			return;
		}
		List<Effect> killEffects = target.KillEffects;
		foreach (Effect effect in killEffects) {
			effectManager.RegisterEffect(effect);
		}
		
		entityManager.VoidTargetable(target);
	
		
		Debug.Log("retreat creature.");
	}

	void OnAttackZoneEntered(object sender, AttackZoneEventArgs e) {
		var creature = e.Creature.GetComponent<CreatureBehaviour>();
		if (creature != null) {
			var attackEffects = creature.AttackEffects;
			Debug.Log("Creature Found");
			foreach (var effect in attackEffects) {
				Debug.Log("Register Effect");
				effectManager.RegisterEffect(effect);
				Debug.Log("implement attack animation.");
				
			}
		}
	}
	
	private void RetreatCreature(CreatureBehaviour creature)
	{
		creature.CurrentState = EntityStateGenerator.CreateDiveMovementState(creature, 10, -20);
	}

	private void InitAttackZone() {
		attackZone = gameObject.GetComponentInChildren<AttackZoneBehaviour>();
		if (attackZone == null) {
			Debug.LogError("No AttackZone found in Gameworld");
		}
		
		attackZone.AttackZoneEntered += OnAttackZoneEntered;
		guiManager.SwipeCommitted += OnSwipeCommitted;
	}

	public void Awake() {
		InitPlayer();
		InitSpawningZone();
		InitAttackZone();
	}

	private void InitPlayer() {
		var player = gameObject.GetComponentInChildren<PlayerBehaviour>();
		if (player == null) {
			throw new ApplicationException("player component not found");
		}
		
		player.StartLife = 5;
		player.StartPoints = 0;
		entityManager.Player = player;
	}

	private void InitSpawningZone() {
		var spawnPoint = gameObject.GetComponentInChildren<SpawnPointBehaviour>();
		if (spawnPoint == null) {
			throw new ApplicationException("spawn point component not found");
		}
		
		entityManager.SetSpawnPoint(spawnPoint);
	}

	private void OnCreatureGenerated(object sender, EntityGeneratedEventArgs<CreatureTypes> e) {
		entityManager.SpawnCreature(e.EntityType);
	}


	private void OnPerkGenerated(object sender, EntityGeneratedEventArgs<PerkTypes> e) {
		entityManager.SpawnPerk(e.EntityType);
	}

	public void ChangePlayerHealth(float lifeChange) {
		entityManager.Player.Life += lifeChange;
		Debug.Log("Health modified: " + lifeChange);
		if (entityManager.Player.IsDead) {
			Debug.Log("YOU SUCK!!");
			Application.LoadLevel(2);
		}
	}

	public void ChangePlayerPoints(float pointsChange) {
		Debug.Log("points modified: " + pointsChange);
		entityManager.Player.Points += pointsChange;
	}

	public void Update() {
		creatureSpawner.Update();
		effectManager.Update();
		perkSpawner.Update();
	}
}
