using UnityEngine;
using System.Collections;

public class CustomTerrain {
	TerrainData td;
	float[,] heights;
	
	public CustomTerrain(TerrainData t){
		td = t;
	}
	
	public CustomTerrain(){
		td = new TerrainData();
	}
	
	public CustomTerrain(float[,] hm, TerrainData t){
		td = t;
		heights = hm;
	}
	
	public CustomTerrain(float[,] hm){
		td = new TerrainData();
		heights = hm;
	}
	
	public float[,] resetHM(){
		float[,] hm = new float[td.heightmapWidth,td.heightmapHeight];
		for(int i = 0; i < td.heightmapWidth; i++){
			for(int j = 0; j < td.heightmapHeight; j++){
				hm[i,j] = 0;
			}
		}
		return hm;
	}
	
	public float[,] genRandomHM(){
		float[,] hm = new float[td.heightmapWidth,td.heightmapHeight];
		for(int i = 0; i < td.heightmapWidth; i++){
			for(int j = 0; j < td.heightmapHeight; j++){
				hm[i,j] = Random.Range(0.0f,0.1f)* 0.05f;
			}
		}
		return hm;
	}
	
	public void setHM(float[,] hm){
		heights = hm;
	}
	
	public void setHights(){
		td.SetHeights(0,0,heights);
	}
	
	public GameObject genTerrain(){
		return Terrain.CreateTerrainGameObject(td);
	}
}
