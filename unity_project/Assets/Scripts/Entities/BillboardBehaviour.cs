using UnityEngine;
using System.Collections;

public sealed class BillboardBehaviour : MonoBehaviour
{
	// Update is called once per frame
	public void Update() {
		transform.LookAt(Camera.main.transform, Vector3.up);
		transform.RotateAround(transform.right, 90);
	}
}

