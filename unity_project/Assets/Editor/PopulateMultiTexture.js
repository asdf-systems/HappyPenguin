// Add texture to array
static var assetPath ="Assets/Media/Environment/Water/Textures/pPlane_";
static var imageType ="png";



@MenuItem ("Custom/Load MultiTextureObject")
static function loadmultitexture () {
	
	
	var objects=Selection.gameObjects; //.Find("MultiGUITexture");
 	if(objects.Length == 0){
 		print("Please Select Object");
 		return;
 	}
 	var s = objects[0].GetComponent("MovieTexture");
 	
 	if(s) {
 	
		Debug.Log("Find Texture Object");
		//not sure what the types are, so force them
		var a=1;
		var pngAsset : Texture;

		var assets = new Array();
		while (true){
			//set the path of the asset
        	var fillZero = "";
        	/*if(a < 10 )
        		fillZero = "000";
        	else if(a < 100 )
        		fillZero = "00";
        	else if(a < 1000 )
        		fillZero = "0";		*/
        	
    		var path = assetPath + fillZero + (a)+"." + imageType;
    		Debug.Log("Asset Path looked for: " + path);
    		//find the asset and assign it to pngAsset
         	pngAsset=AssetDatabase.LoadAssetAtPath(path,Texture);
         	
         	if(!pngAsset) {
         		Debug.Log("Texture not Found");
         		break;
         	}
         	
         	a++;
         	assets.push(pngAsset);
		}
		s.textures = assets.ToBuiltin(Texture);
	}
	else {
		print("Could not find a MovieTexture on Selected Object");
	}
    
}
function Update () {
}