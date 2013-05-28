using UnityEngine;
using System.Collections;

public class TerrainGen : MonoBehaviour {
	public GameObject terrain;
	float[,] shm;
	float[,] hm;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.R)){
			terrain.GetComponent<Terrain>().terrainData.SetHeights(0,0,shm);
		}
	}
	void genStdHM(){
		shm = new float[terrain.GetComponent<Terrain>().terrainData.heightmapWidth,terrain.GetComponent<Terrain>().terrainData.heightmapHeight];
		for(int i = 0; i < terrain.GetComponent<Terrain>().terrainData.heightmapWidth; i++){
			for(int j = 0; j < terrain.GetComponent<Terrain>().terrainData.heightmapHeight; j++){
				shm[i,j] = 0;
			}
		}
	}
	void genHM(){
		hm = new float[terrain.GetComponent<Terrain>().terrainData.heightmapWidth,terrain.GetComponent<Terrain>().terrainData.heightmapHeight];
		for(int i = 0; i < terrain.GetComponent<Terrain>().terrainData.heightmapWidth; i++){
			for(int j = 0; j < terrain.GetComponent<Terrain>().terrainData.heightmapHeight; j++){
				hm[i,j] = Random.Range(0.0f,0.1f)* 0.05f;
			}
		}
	}
}
