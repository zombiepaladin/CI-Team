// obtained from http://wiki.unity3d.com/index.php/HeightmapFromGridFloat
import System;
import System.IO;
 
@MenuItem ("Terrain/Heightmap From GridFloat")
 
 
 
static function ApplyHeightmap () {
 
	var cellSize : float = 10;//adjust this according to the resolution of the file you are using.
 
 	var filepath : String = null;
	var hdrpath : String = null;
 
	var bytes : byte[];
	var newHeights : Array[];
	var width = 0;  //number of columns of GridFloat data
	var height = 0; //number of rows of GridFloat data
	var maxHeight = 0.0; //max height value in the file
 
	//filepath = EditorUtility.GetAssetPath(Selection.activeObject);
	filepath = AssetDatabase.GetAssetPath(Selection.activeObject);
	hdrpath = Path.ChangeExtension(filepath,".hdr");
    if (filepath == null) { 
        EditorUtility.DisplayDialog("No file selected", "Please select a FloatGrid (.flt) file.", "Cancel"); 
        return; 
    }
 
 
	if(File.Exists(hdrpath)){
		var hdrReader : StreamReader = new StreamReader(hdrpath);
		var hdrTemp : String = null;
		hdrTemp = hdrReader.ReadLine();
		while(hdrTemp!=null){
			var spaceStart : int = hdrTemp.IndexOf(" ");
			var spaceEnd : int = hdrTemp.LastIndexOf(" ");
 
			hdrTemp=hdrTemp.Remove(spaceStart, spaceEnd-spaceStart);
 
			var lineTemp : String[] = hdrTemp.Split(" "[0]);
 
			switch(lineTemp[0]){
				case "nrows":
					height = Int32.Parse(lineTemp[1]);
					break;
				case "ncols":
					width = Int32.Parse(lineTemp[1]);
					break;
				default:
					break;
			}
			hdrTemp = hdrReader.ReadLine();
		}
	}
	else{
		EditorUtility.DisplayDialog("File not found!", "The header (HDR) file is missing.", "Cancel"); 
        return;
	}
 
	if(File.Exists(filepath)){
		bytes = File.ReadAllBytes(filepath);
		newHeights  = new Array[height];
 
		for(var i=0; i<height;i++){
			newHeights[i]=new Array[width];
			for(var j=0; j<width; j++){
				newHeights[i][j] = BitConverter.ToSingle(bytes,i*width*4+j*4);
				if(newHeights[i][j]>maxHeight) maxHeight = newHeights[i][j];
			}
		}		
	}
	else{
		EditorUtility.DisplayDialog("File not found!", "Odd, I thought I saw it.", "Cancel"); 
        return;
	}
 
 
    if (!EditorUtility.DisplayDialog("Warning", "This will overwrite the existing heightmap; no undo is possible. Are you sure you want to proceed?", "Apply heightmap", "Cancel")) {
        return;
    }
 
    var terrain = Terrain.activeTerrain.terrainData;
 
    var w2 = terrain.heightmapWidth;
    var heightmapData = terrain.GetHeights(0, 0, w2, w2);
 	var wRatio : float = (width*1.0)/(w2*1.0);
	var hRatio : float = (height*1.0)/(w2*1.0);
 
	terrain.size.x = Mathf.Floor(width*cellSize);
	terrain.size.z = Mathf.Floor(height*cellSize);
	terrain.size.y=maxHeight;
 
 
    for (y = 0; y < w2; y++) {
        for (x = 0; x < w2; x++) {
 
            var tempU = 0;
			var tempD = 0;
			var tempL = 0;
			var tempR = 0;
			var tempUL = 0;
			var tempUR = 0;
			var tempDL = 0;
			var tempDR = 0;
 
			if(Mathf.Floor(x*wRatio)>0 && Mathf.Floor(x*wRatio)<width-1 && Mathf.Floor(y*hRatio)>0 && Mathf.Floor(y*hRatio)<height-1){
				tempL = (newHeights[Mathf.Floor(y*hRatio)][Mathf.Floor(x*wRatio)-1] - newHeights[Mathf.Floor(y*hRatio)][Mathf.Floor(x*wRatio)])*wRatio;
				tempR = (newHeights[Mathf.Floor(y*hRatio)][Mathf.Floor(x*wRatio)] - newHeights[Mathf.Floor(y*hRatio)][Mathf.Floor(x*wRatio)+1])*wRatio;
				tempU = (newHeights[Mathf.Floor(y*hRatio)-1][Mathf.Floor(x*wRatio)] - newHeights[Mathf.Floor(y*hRatio)][Mathf.Floor(x*wRatio)])*hRatio;
				tempD = (newHeights[Mathf.Floor(y*hRatio)][Mathf.Floor(x*wRatio)] - newHeights[Mathf.Floor(y*hRatio)+1][Mathf.Floor(x*wRatio)])*hRatio;
			}
 
			var avg = (newHeights[y*hRatio][x*wRatio]) + (tempL+tempR+tempU+tempD);
			heightmapData[x,y] = avg/terrain.size.y;
        }
    }
    terrain.SetHeights(0, 0, heightmapData);
}