using UnityEngine;
using System.Collections;

public sealed class BillboardBehaviour : MonoBehaviour
{
	public Camera PlayerCamera {
		get;
		set;
	}
	
	// Use this for initialization
	void Awake() {
		
	}

	// Update is called once per frame
	void Update() {
		if (PlayerCamera == null) {
			return;
		}
		
		transform.LookAt(Camera.main.transform);
		
		var cross = Vector3.Cross(Camera.main.transform.forward, Camera.main.transform.up);
		cross.Normalize();
		transform.RotateAround(cross, 90);
	}
}

