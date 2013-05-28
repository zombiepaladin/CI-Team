using UnityEngine;
using System.Collections;

public class StartScript : MonoBehaviour {
	CustomTerrain ct;
	public GameObject terrain;
	float[,] shm;
	// Use this for initialization
	void Start () {
		ct = new CustomTerrain(terrain.GetComponent<Terrain>().terrainData);
		shm = ct.resetHM();
		terrain.GetComponent<Terrain>().terrainData.SetHeights(0,0,shm);	
		float[,] hm = ct.genRandomHM();
		terrain.GetComponent<Terrain>().terrainData.SetHeights(0,0,hm);	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.R)){
			terrain.GetComponent<Terrain>().terrainData.SetHeights(0,0,shm);
		}
	}
}
