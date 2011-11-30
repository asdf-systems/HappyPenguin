using UnityEngine;
using System.Collections;

public sealed class BillboardBehaviour : MonoBehaviour
{
	public void Awake() {
		transform.rotation = Quaternion.identity;
		transform.localRotation = Quaternion.identity;
	}
	
	public float SymbolDepthOffset;
	
	public void ApplyOffset() {
		var camTransform = Camera.main.transform;
		var worldPosition = transform.position + camTransform.right;
		var direction = worldPosition - transform.position;
		direction.Normalize();
		transform.Translate(direction * SymbolDepthOffset);
	}
	
	// Update is called once per frame
	public void Update() {
		var camTransform = Camera.main.transform;
		var worldPosition = transform.position + camTransform.right;
		transform.LookAt(worldPosition, -(camTransform.rotation * Vector3.forward));
	}
}

