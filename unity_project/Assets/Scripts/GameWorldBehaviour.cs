using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using HappyPenguin.Entities;
using HappyPenguin.Effects;
using HappyPenguin.Spawning;
using HappyPenguin;

public sealed class GameWorldBehaviour : MonoBehaviour
{
	private Camera _playerCamera;
	public GUIManager guiManager;
	public GameObject RetreatPoint;
	private readonly EffectManager effectManager;

	private readonly CreatureSpawner creatureSpawner;
	private readonly PerkSpawner perkSpawner;
	private readonly TargetableSymbolManager symbolManager;
	public EntityManager entityManager{ get; private set;}

	private AttackZoneBehaviour attackZone;
	
	public string perkText;
	public string wrongSymbolChainText;
	public string looseText;

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
			guiManager.alert(wrongSymbolChainText);
			Debug.Log("implemented punish player");
			return;
		}
		
		entityManager.Player.PlayAnimation("throw");
		
		List<Effect> killEffects = target.KillEffects;
		foreach (Effect effect in killEffects) {
			effectManager.RegisterEffect(effect);
		}		
		Debug.Log("retreat creature.");
	}

	void OnAttackZoneEntered(object sender, AttackZoneEventArgs e) {
		var creature = e.Creature.GetComponent<CreatureBehaviour>();
		if (creature != null) {
			var attackEffects = creature.AttackEffects;
			foreach (var effect in attackEffects) {
				effectManager.RegisterEffect(effect);
			}
		}
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
		InitCreatureSpawningNode();
		InitPerkSpawningNode();
		InitAttackZone();
		InitUI();
	}
	
	private void InitUI()
	{
		guiManager.changePoints(entityManager.Player.Points);
		guiManager.changeLife(entityManager.Player.Life);
	}
	
	private void InitPerkSpawningNode()
	{
		var spawnPoint = gameObject.GetComponentsInChildren<SpawnPointBehaviour>().FirstOrDefault(x => x.Key == "perks");
		if (spawnPoint == null) {
			throw new ApplicationException("spawn point component not found");
		}
		
		entityManager.SetPerkSpawnPoint(spawnPoint);
	}

	private void InitPlayer() {
		var player = gameObject.GetComponentInChildren<PlayerBehaviour>();
		if (player == null) {
			throw new ApplicationException("player component not found");
		}
		
		player.StartLife = 5;
		player.StartPoints = 0;
		var state =  new EntityState("player_idle");
		state.AnimationNames.Add("idle");
		player.CurrentState = state;
		entityManager.Player = player;
	}

	private void InitCreatureSpawningNode() {
		var spawnPoint = gameObject.GetComponentsInChildren<SpawnPointBehaviour>().FirstOrDefault(x => x.Key == "creatures");
		if (spawnPoint == null) {
			throw new ApplicationException("spawn point component not found");
		}
		
		entityManager.SetCreatureSpawnPoint(spawnPoint);
	}

	private void OnCreatureGenerated(object sender, EntityGeneratedEventArgs<CreatureTypes> e) {
		entityManager.SpawnCreature(e.EntityType);
	}


	private void OnPerkGenerated(object sender, EntityGeneratedEventArgs<PerkTypes> e) {
		entityManager.SpawnPerk(e.EntityType);
	}

	public void ChangePlayerHealth(float lifeChange) {
		var player = gameObject.GetComponentInChildren<PlayerBehaviour>();
		entityManager.Player.Life += lifeChange;
		guiManager.changeLife(entityManager.Player.Life);
		Debug.Log("Health modified: " + lifeChange);
		
		if (lifeChange > 0) {
			guiManager.alert("" + lifeChange + " Life");
			player.audio.clip = player.AttackSound;
			player.audio.Play();
		}
		else {
			guiManager.alert(lifeChange + " Life");
		}
		
		if (entityManager.Player.IsDead) {
			guiManager.alert(looseText);
			PlayerBehaviour.FinalPoints = entityManager.Player.Points;
			Application.LoadLevel(2);
		}
	}

	public void ChangePlayerPoints(float pointsChange) {
		Debug.Log("points modified: " + pointsChange);
		guiManager.alert("+ " + pointsChange + " Points");
		entityManager.Player.Points += pointsChange;
		guiManager.changePoints(entityManager.Player.Points);
	}

	public void Update() {
		creatureSpawner.Update();
		effectManager.Update();
		perkSpawner.Update();
	}
}
