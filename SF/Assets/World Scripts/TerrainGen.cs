using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class TerrainGen : MonoBehaviour {
	public GameObject terrain;
	SFHeightMap hm;
	SFHeightMap shm;
	
	// Use this for initialization
	void Start () {
		shm = new SFHeightMap(terrain.GetComponent<Terrain>().terrainData.heightmapWidth,terrain.GetComponent<Terrain>().terrainData.heightmapHeight,false);
		hm = new SFHeightMap(terrain.GetComponent<Terrain>().terrainData.heightmapWidth,terrain.GetComponent<Terrain>().terrainData.heightmapHeight,true);
		terrain.GetComponent<Terrain>().terrainData.SetHeights(0,0,hm.getHM());
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.R)){
			terrain.GetComponent<Terrain>().terrainData.SetHeights(0,0,shm.getHM());
		}
		if(Input.GetKeyUp(KeyCode.E)){
			terrain.GetComponent<Terrain>().terrainData.SetHeights(0,0,hm.getHM ());
		}
		if(Input.GetKeyUp (KeyCode.F1)){
			hm.SaveTerrain();
		}
		if(Input.GetKeyUp (KeyCode.J)){
			float[,] loadedT = hm.LoadTerrain();
			terrain.GetComponent<Terrain>().terrainData.SetHeights(0,0,loadedT);
		}
	}
}
