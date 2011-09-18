using System.Linq;
using UnityEngine;
using HappyPenguin.Unity;
using HappyPenguin.Entities;
using System.Collections;
using System.Collections.Generic;

public sealed class PatrolBehaviour : EnvironmentEntityBehaviour
{
	public IList<Vector3> PatrolPositions;
	private int currentTargetIndex;
	public bool IsActive;

	// Use this for initialization
	protected override void AwakeOverride() {
		base.AwakeOverride();
		
		IsActive = true;
		PatrolPositions = new List<Vector3>();
	}

	// Update is called once per frame
	protected override void UpdateOverride() {
		base.UpdateOverride();
		
		if (!PatrolPositions.Any()) {
			return;
		}
		
		var targetPosition = PatrolPositions[currentTargetIndex];
		if (Position.IsCloseEnoughTo(targetPosition)) {
			CurrentState = null;
			currentTargetIndex++;
			if (currentTargetIndex == PatrolPositions.Count) {
				// start from beginning
				currentTargetIndex = 0;
			}
			return;
		}
		
		if (CurrentState == null) {
			CurrentState = EntityStateGenerator.CreatePatrolState(targetPosition);
		}
	}
}
