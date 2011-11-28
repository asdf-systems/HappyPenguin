using UnityEngine;

public class Loader : MonoBehaviour
{
	private static bool done = false;

	public void Awake() {
		if(!done) {
			done = true;
			Application.LoadLevel(0);
		}
	}
}
