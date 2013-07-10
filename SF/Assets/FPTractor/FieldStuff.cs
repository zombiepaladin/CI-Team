using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using Serialization;

public class FieldStuff : MonoBehaviour {
	
	public GameObject tractor;
	public GameObject terrain;
	float[,,] alphatext;
	SFField sf = new SFField();
	string FieldName = "";
	float cropSpace = 10.0f;
	float moved = 0.0f;
	float startingPoint = 50.0f;
	Crop crop;
	bool loaded = false;
	
	// Use this for initialization
	void Start () {
		crop = new Crop();
		if(PlayerPrefs.GetString("FieldName") == ""){
			crop.setType(PlayerPrefs.GetInt("CropType"));
			FieldName = PlayerPrefs.GetString("NFName");
			alphatext = new float[terrain.GetComponent<Terrain>().terrainData.alphamapWidth,terrain.GetComponent<Terrain>().terrainData.alphamapHeight,2];
			for(int i = 0; i < terrain.GetComponent<Terrain>().terrainData.alphamapWidth; i++){
				for(int j = 0; j < terrain.GetComponent<Terrain>().terrainData.alphamapHeight; j++){
					alphatext[i,j,0] = 1;
					alphatext[i,j,1] = 0;
				}
			}
			Vector3 localPos = new Vector3(50,0,50) - terrain.transform.position;
			Vector3 normalPos = new Vector3((localPos.x/terrain.GetComponent<Terrain>().terrainData.size.x) * terrain.GetComponent<Terrain>().terrainData.alphamapWidth,
				0,
				(localPos.z/terrain.GetComponent<Terrain>().terrainData.size.z) * terrain.GetComponent<Terrain>().terrainData.alphamapHeight);
			alphatext[(int)normalPos.z,(int)normalPos.x,0] = 0;
			alphatext[(int)normalPos.z,(int)normalPos.x,1] = 1;
			terrain.GetComponent<Terrain>().terrainData.SetAlphamaps(0,0,alphatext);
		}
		else{
			FieldName = PlayerPrefs.GetString("FieldName");
			byte[] load = sf.Load (PlayerPrefs.GetString("FieldName"));
			UnitySerializer.DeserializeInto(load,sf);
			crop.setField(sf.getCrops());
			terrain.GetComponent<Terrain>().terrainData.SetHeights(0,0,sf.getHM ());
			terrain.GetComponent<Terrain>().terrainData.SetAlphamaps(0,0,sf.getAM());
			alphatext = sf.getAM();
			terrain.GetComponent<Terrain>().terrainData.SetAlphamaps(0,0,alphatext);
			loaded = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 localPos = tractor.transform.position - terrain.transform.position;
		Vector3 normalPos = new Vector3((localPos.x/terrain.GetComponent<Terrain>().terrainData.size.x) * terrain.GetComponent<Terrain>().terrainData.alphamapWidth,
			0,
			(localPos.z/terrain.GetComponent<Terrain>().terrainData.size.z) * terrain.GetComponent<Terrain>().terrainData.alphamapHeight);
		alphatext[(int)normalPos.z,(int)normalPos.x,0] = 0;
		alphatext[(int)normalPos.z,(int)normalPos.x,1] = 1;
		terrain.GetComponent<Terrain>().terrainData.SetAlphamaps(0,0,alphatext);
	}
}
