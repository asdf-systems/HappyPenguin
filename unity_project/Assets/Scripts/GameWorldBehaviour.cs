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
	private GUIManager guiManager;
	private EffectManager effectManager;
	private IconSlotManager iconSlotManager;

	private CreatureSpawner creatureSpawner;
	private PerkSpawner perkSpawner;
	private List<LifeSpawnBeacon> lifeBeacons;

	private AttackZoneBehaviour attackZone;
	
	private bool isPaused = false;
	private float oldTimeScale;
	public EntityManager entityManager { get; private set; }
	public GameObject Trebuchet;
	public string PerkText;
	public string WrongSymbolChainText;
	public string LooseText;
	public IngameSoundEffects IngameSounds;
	public int ProbabilityForCheers = 5;
	
	public AudioClip SlowBackgroundMusic;
	public AudioClip NormalBackgroundMusic;

	public Range SymbolRangeModifer {
		set { entityManager.SymbolRangeModifer = value; }
	}

	// implemented
	public float PointsMultiplier { get; set; }

	// implemented
	public float SnowballSpeedModifier { get; set; }
	
	public Color DarkLight;
	private Color oldAmbientLight;
	private System.Random random;

	public void Awake() {
		random = new System.Random();
		InitGameWorldBehaviour();
		InitIconSlotManager();
		InitPlayer();
		InitLifeBeacons();
		InitEntityRoot();
		InitCreatureNodes();
		InitPerkNodes();
		InitAttackZone();
		InitStatics();
		initAmbientLights();
	}

	void Start() {
		InitUI();
		PointsMultiplier = 1;
		SnowballSpeedModifier = 1;
		oldTimeScale = Time.timeScale;
		PlayNormalBackgroundMusic();
	}


// Init Functions
	private void InitGameWorldBehaviour() {
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
	
	private void initAmbientLights(){
		oldAmbientLight = RenderSettings.ambientLight;
	}
	private void InitAttackZone() {
		attackZone = gameObject.GetComponentInChildren<AttackZoneBehaviour>();
		if (attackZone == null) {
			EditorDebug.LogError("No AttackZone found in Gameworld");
		}
		
		attackZone.AttackZoneEntered += OnAttackZoneEntered;
	}

	private void InitCreatureNodes() {
		var attackNode = GameObject.FindWithTag("attack_node");
		if (attackNode == null) {
			throw new ApplicationException("attack node not found");
		}
		GameObjectRegistry.RegisterObject("attack_node", attackNode);
		
		var creatureSpawn = GameObject.FindWithTag("creature_spawn");
		
		var patrol = creatureSpawn.GetComponent<PatrolBehaviour>();
		patrol.PatrolPositions.Add(new Vector3(-200, -0.9f, 200));
		patrol.PatrolPositions.Add(new Vector3(-200, -0.9f, -140));
		//patrol.PatrolPositions.Add(new Vector3(-370, -0.9f, 54));
		//patrol.PatrolPositions.Add(new Vector3(-40, -0.9f, 270));
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
		guiManager = GUIManager.Instance;
		guiManager.DisplayPoints(entityManager.Player.Points);
		//guiManager.DisplayLife(entityManager.Player.Life);
		guiManager.SwipeCommitted += OnSwipeCommitted;
		guiManager.SymbolsChanged += OnSymbolChanged;
		guiManager.GamePaused += OnGamePaused;
		guiManager.GameResumed += OnGameResumed;
		guiManager.GameCanceld += OnGameCancelled;
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

// Effekt Handling
	public void RegisterEffect(Effect effect) {
		effectManager.RegisterEffect(effect);
		if (effect.HasDescription) {
			string s = effect.Description;
			if(s != string.Empty)
				guiManager.Alert(s);
		}
		if (effect.IsIconAvailable && effect.Duration > TimeSpan.Zero) {
			iconSlotManager.DisplayEffect(effect);
		}
	}

	public void RegisterEffects(IEnumerable<Effect> effects) {
		foreach (var effect in effects) {
			RegisterEffect(effect);
		}
	}

// Symbol Modifikation
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

// Event Handling
	private void OnSymbolChanged(object sender, SymbolEventArgs e) {
		if (string.IsNullOrEmpty(e.SymbolChain)) {
			DarkenSymbols();
			return;
		}
		HighlightSymbols(e.SymbolChain);
	}

	private void OnEffectsReleased(object sender, EffectEventArgs e) {
		foreach (var effect in e.Effects) {
			RegisterEffect(effect);
		}
	}

	private void OnEffectExpired(object sender, EffectEventArgs e) {
		foreach (var effect in e.Effects) {
			if (effect.IsIconAvailable) {
				iconSlotManager.HideEffect(effect);	
			}
		}
	}

	private void OnSwipeCommitted(object sender, SwipeEventArgs e) {
		guiManager.ClearSymbols();
		var target = entityManager.FindTargetable(e.SymbolChain);
		if (target == null) {
			InvokePlayerMiss();
			return;
		}
		InvokePlayerHit(target);
	}

	private void OnCreatureGenerated(object sender, EntityGeneratedEventArgs<CreatureTypes> e) {
		entityManager.SpawnCreature(e.EntityType);
	}

	private void OnPerkGenerated(object sender, EntityGeneratedEventArgs<PerkTypes> e) {
		entityManager.SpawnPerk(e.EntityType);
		Trebuchet.animation.Play("shoot");
		Trebuchet.animation.PlayQueued("pull");
	}

	public void OnCreatureCountNeeded(object sender, CreatureCountNeededEventArgs e) {
		e.CreatureCount = entityManager.FindCreatures().Count();
	}
	
	public void OnGameResumed(object sender, EventArgs e){
		if(isPaused){
			IngameSounds.PlayPauseEnd();
			DarkenScreen(false);
			Time.timeScale = oldTimeScale;
			isPaused = false;
		}
	}
	
	public void OnGamePaused(object sender, EventArgs e){
		if(!isPaused){
			isPaused = true;
			IngameSounds.PlayPauseStart();
			oldTimeScale = Time.timeScale;
			Time.timeScale = 0;
			//oldAmbientLight = RenderSettings.ambientLight;
			//RenderSettings.ambientLight = new Color(0.5f,0.5f,0.5f,1);
			DarkenScreen(true);
		}
	}
	
	public bool CanPerformUIRotation(){
		return guiManager.CanRotate();
	}
	
	public void OnGameCancelled(object sender, EventArgs e){
		Time.timeScale = 1;
		DarkenScreen(false);
		Application.LoadLevel(1);
	}

// Event Invoke
	private void InvokePlayerHit(TargetableEntityBehaviour target) {
		target.TargetHit += (sender, e) => { RegisterEffects(target.HitEffects); };
		if (!entityManager.Player.IsPlaying("throw")) {
			entityManager.Player.PlayAnimation("throw");	
		}
		entityManager.ThrowSnowball(target, SnowballSpeedModifier);
	}


	private void InvokePlayerMiss() {
		//entityManager.SpawnCreature(CreatureTypes.Blowfish);
		//ChangePlayerPoints(255);
		entityManager.SpawnPerk(PerkTypes.CreatureSlowdown);
		RegisterEffect(new UIRotationEffect(ClockRotations.CounterClockwise));
		IngameSounds.PlayBooSound();
	}

	private void OnAttackZoneEntered(object sender, BehaviourEventArgs<CreatureBehaviour> e) {
		var creature = e.Behaviour;
		if (creature != null) {
			RegisterEffects(creature.AttackEffects);
		}
	}

	public void InvokeUIRotation(ClockRotations clockRotation, bool restore) {
		guiManager.PerformUIRotation(clockRotation, restore);
	}

	internal void ModifyCreatures(Action<CreatureBehaviour> action) {
		var creatures = entityManager.FindCreatures();
		foreach (var creature in creatures) {
			action(creature);
		}
	}

// Game World Handling 
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
	}

	private void ReleaseBalloons(int count) {
		for (int i = 0; i < count; i++) {
			var beacon = lifeBeacons.First(x => x.IsOccupied);
			entityManager.ReleaseLifeBalloon(beacon);
			IngameSounds.PlayBooSound();
		}
	}

	private void SpawnBalloons(int count) {
		for (int i = 0; i < count; i++) {
			var beacon = lifeBeacons.First(x => !x.IsOccupied);
			entityManager.SpawnLifeBalloon(beacon);
			IngameSounds.PlayCheerSound();
		}
	}

	private void CheckForDeadsies() {
		if (entityManager.Player.IsDead) {
			Application.LoadLevel(3);
		}
	}

	public void ChangePlayerPoints(float change) {
		EditorDebug.Log("Get Points: " + change);
		
		var points = change * PointsMultiplier;
		
		entityManager.Player.Points += points;
		guiManager.DisplayPoints(entityManager.Player.Points);
		GameStatics.Points = entityManager.Player.Points;
		float val = random.Next(0,100);
		if(val >= ProbabilityForCheers)
			IngameSounds.PlayCheerSound();
	}
	
	public void DarkenScreen(bool darken){
		guiManager.DarkenScreen(darken);
	}
	public void PlayFastBackgroundMusic(){
		audio.clip = SlowBackgroundMusic;
		audio.Play();
	}
	
	public void PlayNormalBackgroundMusic(){
		audio.clip = NormalBackgroundMusic;
		audio.Play();
	}

	public void Update() {
		creatureSpawner.Update();
		effectManager.Update();
		perkSpawner.Update();
	}
}
