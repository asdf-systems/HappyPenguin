using UnityEngine;
using System.Collections;
using Pux.Resources;

public class SharedMaterialBehaviour : MonoBehaviour {

	public string SharedMaterialResourcePath;
	private Renderer materialRenderer;
	
	private bool disabled = false;
	private Material material;
	
	void Awake(){
		loadRenderer();
		loadMaterial();	
	}
	
	void Start(){
		if(!disabled)
			materialRenderer.sharedMaterial = material;
	}
	private void loadMaterial(){
		if(disabled)
			return;
		
		Material mat = ResourceManager.GetResource<Material>(SharedMaterialResourcePath);
		if(mat != null){
			material = mat;
			materialRenderer.sharedMaterial = material;
		}
		else
			EditorDebug.LogWarning("Cannot found shared Material: " + SharedMaterialResourcePath + " on Object: " + gameObject.name);
	}
	private void loadRenderer(){
		materialRenderer = gameObject.GetComponent<Renderer>() as Renderer;
		if(materialRenderer == null){
			materialRenderer = gameObject.GetComponentInChildren<Renderer>() as Renderer;
		}
		if(materialRenderer == null){
			EditorDebug.LogError("No Renderer for SharedMaterial found on Object: " + gameObject.name);
			disabled = true;
			
		}
	}
	
}
