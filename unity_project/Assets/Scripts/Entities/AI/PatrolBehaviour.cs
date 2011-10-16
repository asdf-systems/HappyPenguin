using System.Linq;
using UnityEngine;
using HappyPenguin.Unity;
using HappyPenguin.Entities;
using System.Collections;
using System.Collections.Generic;

public sealed class PatrolBehaviour : EntityBehaviour
{
	private int currentTargetIndex;

	public Vector3[] PatrolPositions;
	public bool IsActive;

	protected override void AwakeOverride() {
		if (PatrolPositions.Length == 0 || !IsActive) {
			return;
		}
		
		var targetPosition = PatrolPositions[currentTargetIndex];
		this.MoveTo(targetPosition);
	}

	// Update is called once per frame
	protected override void StartOverride() {
		Debug.Log("PatrolPoints:" + PatrolPositions.Length);
		
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
		
		base.StartOverride();
	}
}
