using UnityEngine;
using System.Collections;

public sealed class BillboardBehaviour : MonoBehaviour
{
	public void Awake() {
		transform.rotation = Quaternion.identity;
		transform.localRotation = Quaternion.identity;
	}
	
	// Update is called once per frame
	public void Update() {
		var camTransform = Camera.main.transform;
		transform.LookAt(transform.position + camTransform.right, -(camTransform.rotation * Vector3.forward));
		transform.Rotate(OrientationCorrection);
	}
	
	public Vector3 OrientationCorrection;
}

