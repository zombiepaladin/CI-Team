using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using Serialization;

public class AITractor : MonoBehaviour {
	/*
	 * This is the AI controller for any vehicle. 
	 * It takes in the vehicle frame (in this case a FPS controller) and creates an instance of it called tractorAI
	 * It takes in the Position so it knows where to start.
	 * It takes in also the Terran and a dugup Texture. the Terrain is to be passed into the waypoints class the texture is to be applied to the dugup terrain.
	 * 
	 * isLine is to see if it is turning or going stright.
	 * isPos is to see if the line is increasing or decreasing.
	 * currentPoint is the current spot in the waypoint list.
	 * waypoints is the waypont manager and documentation for that can be found in waypointFPT.cs 
	 * 
	 * 
	 * 
	*/
	public GameObject objPre;
	public Transform thisPos;
	public GameObject terrain;
	private GameObject tractorAI;
	bool isline = true;
	bool isPos = true;
	float currentPoint = 0;
	WaypointFPT waypoints;
	float lerpRate = 1.0f; 
	float TotalTime = 0;
	float[,,] alphatext;
	SFField sf = new SFField();
	string FieldName = "";
	float cropSpace = 10.0f;
	float moved = 0.0f;
	float startingPoint = 50.0f;
	Crop crop;
	bool loaded = false;
	
	/*
	 * This is ran during the start up of the scene. 
	 * This set the terrain to dug up when driven over.
	 * 
	*/ 
	void Start () {
		crop = new Crop();
		waypoints = new WaypointFPT(terrain);
		waypoints.genPointsStr(new Vector3(50,0,50),true);
		tractorAI = (GameObject)Instantiate(objPre,new Vector3(50,.3f,50),Quaternion.identity);
		tractorAI.name = "AI Tractor FP";
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
			tractorAI.transform.position += new Vector3(0,15,0);
			terrain.GetComponent<Terrain>().terrainData.SetHeights(0,0,sf.getHM ());
			terrain.GetComponent<Terrain>().terrainData.SetAlphamaps(0,0,sf.getAM());
			alphatext = sf.getAM();
			terrain.GetComponent<Terrain>().terrainData.SetAlphamaps(0,0,alphatext);
			loaded = true;
		}
		
	}
	
	/*
	 * This is ran during every frame.
	 * This does our custom lerping and updates the terrain if it is driven over. 
	 * 
	*/
	void Update(){
		if(Input.GetKeyUp(KeyCode.F1)){
			sf.setAM(alphatext,terrain.GetComponent<Terrain>().terrainData.alphamapWidth,terrain.GetComponent<Terrain>().terrainData.alphamapHeight,terrain.GetComponent<Terrain>().terrainData.alphamapLayers);
			sf.setHM (terrain.GetComponent<Terrain>().terrainData.GetHeights(0,0,terrain.GetComponent<Terrain>().terrainData.heightmapWidth,terrain.GetComponent<Terrain>().terrainData.heightmapHeight),terrain.GetComponent<Terrain>().terrainData.heightmapWidth,terrain.GetComponent<Terrain>().terrainData.heightmapHeight);
			sf.setCrops(crop.getPosAsVec4());
			sf.Save (FieldName);
			Debug.Log ("Bob");
		}
		TotalTime += Time.deltaTime;
		currentPoint = Mathf.Floor(TotalTime/lerpRate);
		float offsetTime = TotalTime - (currentPoint*lerpRate);
		if(currentPoint > waypoints.points.Count){
			if(isline){
				isline = false;
				waypoints.genPointsTurn(tractorAI.GetComponent<Transform>().position,isPos);
				TotalTime = 0;
				moved = 0.0f;
			}
			else if(!isline){
				if(isPos){
					isline = true;
					isPos = false;
					waypoints.genPointsStr(tractorAI.GetComponent<Transform>().position,isPos);
					TotalTime = 0;
					startingPoint = tractorAI.transform.position.z;
				}
				else if(!isPos){;
					isline = true;
					isPos = true;
					waypoints.genPointsStr(tractorAI.GetComponent<Transform>().position,isPos);
					TotalTime = 0;
				}
			}
		}
		else{
			tractorAI.transform.position = waypoints.points[(int)currentPoint]+offsetTime*(waypoints.points[(int)currentPoint+1]-waypoints.points[(int)currentPoint]);
		}
		if(isline){
			moved = Mathf.Abs(tractorAI.transform.position.z - startingPoint);
		}
		if(moved >= cropSpace && isline && !loaded){
			
			crop.PlantCrop(new Vector3(tractorAI.transform.position.x,tractorAI.transform.position.y,tractorAI.transform.position.z-3.0f));
			moved = 0.0f;
			startingPoint = tractorAI.transform.position.z;
		}
		if(loaded){
			if(crop.lastPlant(tractorAI.transform.position)){
				loaded = false;
			}
		}
		Vector3 localPos = tractorAI.transform.position - terrain.transform.position;
		Vector3 normalPos = new Vector3((localPos.x/terrain.GetComponent<Terrain>().terrainData.size.x) * terrain.GetComponent<Terrain>().terrainData.alphamapWidth,
			0,
			(localPos.z/terrain.GetComponent<Terrain>().terrainData.size.z) * terrain.GetComponent<Terrain>().terrainData.alphamapHeight);
		alphatext[(int)normalPos.z,(int)normalPos.x,0] = 0;
		alphatext[(int)normalPos.z,(int)normalPos.x,1] = 1;
		terrain.GetComponent<Terrain>().terrainData.SetAlphamaps(0,0,alphatext);
		
	}
}
