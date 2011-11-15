using UnityEngine;
using System.Collections;
using Pux.Resources;

public class SharedMaterialBehaviour : MonoBehaviour {

	public string SharedMaterialResourcePath;
	
	void Awake(){
		Material mat = ResourceManager.GetResource<Material>(SharedMaterialResourcePath);
		if(mat != null)
				renderer.sharedMaterial = mat;
		else
			Debug.LogWarning("Cannot found shared Material: " + mat.name + " on Object: " + gameObject.name);
			
	}
	
}
