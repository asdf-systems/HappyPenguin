using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using Pux.Entities;
using Pux.Effects;
using Pux.Spawning;
using Pux;
using Pux.UI;

public sealed class GameWorldBehaviour : MonoBehaviour
{
	public GUIManager guiManager;
	public GameObject Trebuchet;
	public readonly EffectManager effectManager;
	private readonly IconSlotManager iconSlotManager;

	private readonly CreatureSpawner creatureSpawner;
	private readonly PerkSpawner perkSpawner;
	private readonly TargetableSymbolManager symbolManager;
	private readonly List<LifeSpawnBeacon> lifeBeacons;

	public EntityManager entityManager { get; private set; }

	private AttackZoneBehaviour attackZone;

	public string PerkText;
	public string WrongSymbolChainText;
	public string LooseText;

	private void HighlightSymbols(string chain) {
		var targetables = entityManager.FindTargetables();
		foreach (var entity in targetables) {
			if (entity.SymbolChain.StartsWith(chain)) {
				entity.HighlightSymbols(chain.Length);
			}
		}
	}

	private void DarkenSymbols() {
		var targetables = entityManager.FindTargetables();
		foreach (var entity in targetables) {
			entity.DarkenSymbols();
		}
	}

	private void OnSymbolChanged(object sender, SymbolEventArgs e) {
		if (string.IsNullOrEmpty(e.SymbolChain)) {
			DarkenSymbols();
			return;
		}
		HighlightSymbols(e.SymbolChain);
	}

	private void OnEffectsReleased(object sender, EffectEventArgs e) {
		foreach (var effect in e.Effects) {
			effectManager.RegisterEffect(effect);
		}
	}
	
	private void OnEffectExpired(object sender, EffectEventArgs e){
		foreach (var effect in e.Effects) {
			iconSlotManager.HideEffect(effect);
		}
	}

	public GameWorldBehaviour() {
		entityManager = new EntityManager();
		entityManager.EffectsReleased += OnEffectsReleased;
		
		effectManager = new EffectManager(this);
		effectManager.EffectExpired += OnEffectExpired;
		
		creatureSpawner = new CreatureSpawner();
		creatureSpawner.EntitySpawned += OnCreatureGenerated;
		creatureSpawner.CreatureCountNeeded += OnCreatureCountNeeded;
		
		perkSpawner = new PerkSpawner();
		perkSpawner.EntitySpawned += OnPerkGenerated;
		
		lifeBeacons = new List<LifeSpawnBeacon>();
		iconSlotManager = new IconSlotManager();
	}

	private void OnSwipeCommitted(object sender, SwipeEventArgs e) {
		guiManager.ClearSymbols();
		var target = entityManager.FindTargetable(e.symbolChain);
		if (target == null) {
			ApplyEffect(new UIRotationEffect());
			InvokePlayerMiss();
			return;
		}
		InvokePlayerHit(target);
	}

	private void InvokePlayerHit(TargetableEntityBehaviour target) {
		var player = entityManager.Player;
		if (player.gameObject.animation.IsPlaying("throw")) {
			player.gameObject.animation.Stop();
		}
		target.TargetHit += (sender,e ) => {
			effectManager.RegisterEffects(target.HitEffects);
		};
		entityManager.Player.PlayAnimation("throw");
		entityManager.ThrowSnowball(target);
	}

	private void InvokePlayerMiss() {
		Debug.Log("implement trip animation or camera quake ...");
	}

	private void OnAttackZoneEntered(object sender, BehaviourEventArgs<CreatureBehaviour> e) {
		var creature = e.Behaviour;
		if (creature != null) {
			var attackEffects = creature.AttackEffects;
			for (int i = 0; i < attackEffects.Count; i++) {
				ApplyEffect(attackEffects[i]);
			}
		}
	}
	
	private void ApplyEffect(Effect effect)
	{
		effectManager.RegisterEffect(effect);
		if (effect.Duration > TimeSpan.Zero) {
			iconSlotManager.DisplayEffect(effect);
		}
	}

	private void InitAttackZone() {
		attackZone = gameObject.GetComponentInChildren<AttackZoneBehaviour>();
		if (attackZone == null) {
			Debug.LogError("No AttackZone found in Gameworld");
		}
		
		attackZone.AttackZoneEntered += OnAttackZoneEntered;
	}

	public void InvokeUIRotation(ClockRotations clockRotation) {
		guiManager.PerformUIRotation(clockRotation);
	}

	public void Awake() {
		InitIconSlotManager();
		InitPlayer();
		InitLifeBeacons();
		InitEntityRoot();
		InitCreatureNodes();
		InitPerkNodes();
		InitAttackZone();
		InitUI();
		InitStatics();
	}

	private void InitIconSlotManager() {
		iconSlotManager.InitSpotManager();
	}

	private void InitLifeBeacons() {
		var nodes = GameObject.FindGameObjectsWithTag("life_node");
		for (int i = 0; i < entityManager.Player.MaxLife; i++) {
			var node = nodes[i];
			var beacon = new LifeSpawnBeacon(node);
			lifeBeacons.Add(beacon);
		}
		// this must be here, we need the player to do this
		ChangePlayerHealth(entityManager.Player.MaxLife);
	}

	private void InitEntityRoot() {
		var root = GameObject.FindWithTag("entity_root");
		if (root == null) {
			throw new ApplicationException("entity root object not found");
		}
		GameObjectRegistry.RegisterObject("entity_root", root);
	}

	private void InitStatics() {
		GameStatics.Points = 0;
	}

	private void InitUI() {
		guiManager.DisplayPoints(entityManager.Player.Points);
		guiManager.DisplayLife(entityManager.Player.Life);
		guiManager.SwipeCommitted += OnSwipeCommitted;
		guiManager.SymbolsChanged += OnSymbolChanged;
	}

	private void InitPerkNodes() {
		var perkImpact = GameObject.FindWithTag("perk_impact");
		if (perkImpact == null) {
			throw new ApplicationException("perk spawn object not found");
		}
		GameObjectRegistry.RegisterObject("perk_impact", perkImpact);
		
		var perkSpawn = GameObject.FindWithTag("perk_spawn");
		if (perkSpawn == null) {
			throw new ApplicationException("perk spawn object not found");
		}
		GameObjectRegistry.RegisterObject("perk_spawn", perkSpawn);
		
		var perkRetreat = GameObject.FindWithTag("perk_retreat");
		
		if (perkRetreat == null) {
			throw new ApplicationException("perk retreat object not found");
		}
		GameObjectRegistry.RegisterObject("perk_retreat", perkRetreat);
	}

	private void InitPlayer() {
		var player = gameObject.GetComponentInChildren<PlayerBehaviour>();
		if (player == null) {
			throw new ApplicationException("player component not found");
		}
		
		entityManager.Player = player;
		player.PlayAnimation("idle");
		player.Points = 0;
		player.Life = 0;
	}

	private void InitCreatureNodes() {
		var creatureSpawn = GameObject.FindWithTag("creature_spawn");
		
		var patrol = creatureSpawn.GetComponent<PatrolBehaviour>();
		patrol.PatrolPositions.Add(new Vector3(-200, -0.9f, 200));
		patrol.PatrolPositions.Add(new Vector3(-200, -0.9f, -140));
		patrol.IsActive = true;
		
		if (creatureSpawn == null) {
			throw new ApplicationException("creature spawn object not found");
		}
		GameObjectRegistry.RegisterObject("creature_spawn", creatureSpawn);
		
		var creatureRetreat = GameObject.FindWithTag("creature_retreat");
		if (creatureRetreat == null) {
			throw new ApplicationException("creature spawn object not found");
		}
		GameObjectRegistry.RegisterObject("creature_retreat", creatureRetreat);
	}

	private void OnCreatureGenerated(object sender, EntityGeneratedEventArgs<CreatureTypes> e) {
		entityManager.SpawnCreature(e.EntityType);
	}

	private void OnPerkGenerated(object sender, EntityGeneratedEventArgs<PerkTypes> e) {
		entityManager.SpawnPerk(e.EntityType);
		Trebuchet.animation.Play("shoot");
		Trebuchet.animation.PlayQueued("pull");
	}

	private void ApplyHealth(int life) {
		entityManager.Player.Life += life;
	}

	private void ApplyDamage(int damage) {
		entityManager.Player.Life -= damage;
	}

	public void ChangePlayerHealth(float lifeChange) {
		var player = entityManager.Player;
		var missingLife = player.MaxLife - player.Life;
		var actualLifeChange = (int)Math.Min(missingLife, lifeChange);
		
		if (lifeChange < 0) {
			ReleaseBalloons(-actualLifeChange);
			ApplyDamage(-actualLifeChange);
		} else {
			SpawnBalloons(actualLifeChange);
			ApplyHealth(actualLifeChange);
		}
		
		CheckForDeadsies();
		guiManager.DisplayLife(entityManager.Player.Life);
	}

	private void ReleaseBalloons(int count) {
		for (int i = 0; i < count; i++) {
			var beacon = lifeBeacons.First(x => x.IsOccupied);
			entityManager.ReleaseLifeBalloon(beacon);
		}
	}

	private void SpawnBalloons(int count) {
		for (int i = 0; i < count; i++) {
			var beacon = lifeBeacons.First(x => !x.IsOccupied);
			entityManager.SpawnLifeBalloon(beacon);
		}
	}

	private void CheckForDeadsies() {
		if (entityManager.Player.IsDead) {
			Application.LoadLevel(2);
		}
	}

	public void OnCreatureCountNeeded(object sender, CreatureCountNeededEventArgs e) {
		e.CreatureCount = entityManager.FindCreatures().Count();
	}

	public void ChangePlayerPoints(float pointsChange) {
		guiManager.Alert("+ " + pointsChange + " Points");
		entityManager.Player.Points += pointsChange;
		guiManager.DisplayPoints(entityManager.Player.Points);
		GameStatics.Points = entityManager.Player.Points;
	}

	public void Update() {
		creatureSpawner.Update();
		effectManager.Update();
		perkSpawner.Update();
	}
}
