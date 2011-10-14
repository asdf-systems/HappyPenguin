using System.Linq;
using UnityEngine;
using HappyPenguin.Unity;
using HappyPenguin.Entities;
using System.Collections;
using System.Collections.Generic;

public sealed class PatrolBehaviour : EnvironmentEntityBehaviour
{
	private int currentTargetIndex;
	
	public Vector3[] PatrolPositions;
	public bool IsActive;

	// Use this for initialization
	protected override void AwakeOverride() {
		base.AwakeOverride();
		
		if (PatrolPositions.Length == 0) {
			return;
		}
		
		var targetPosition = PatrolPositions[currentTargetIndex];
		this.MoveTo(targetPosition);
	}

	// Update is called once per frame
	protected override void UpdateOverride() {
		base.UpdateOverride();
		
		if (PatrolPositions.Length == 0 || !IsActive) {
			return;
		}

		var targetPosition = PatrolPositions[currentTargetIndex];
		if (!transform.position.IsCloseEnoughTo(targetPosition)) {
			return;
		}
		
		currentTargetIndex++;
		if (currentTargetIndex == PatrolPositions.Length) {
			// start from beginning
			currentTargetIndex = 0;
		}
		this.MoveTo(targetPosition);
	}
}
