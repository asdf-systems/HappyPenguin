using UnityEngine;
using System.Collections;

public sealed class BillboardBehaviour : MonoBehaviour
{
	// Update is called once per frame
	void Update() {
			
		transform.LookAt(Camera.main.transform);
		transform.RotateAround(transform.right, 90);

	}
}

