using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Xml.Serialization;
using System.IO;

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
	SFFD fieldData;
	/*
	 * This is ran during the start up of the scene.
	 * 
	 * 
	*/ 
	void Start () {
		waypoints = new WaypointFPT(terrain);
		waypoints.genPointsStr(new Vector3(50,0,50),true);
		tractorAI = (GameObject)Instantiate(objPre,new Vector3(50,0,50),Quaternion.identity);
		tractorAI.name = "AI Tractor FP";
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
		Debug.Log(normalPos.ToString());
		alphatext[(int)normalPos.z,(int)normalPos.x,0] = 0;
		alphatext[(int)normalPos.z,(int)normalPos.x,1] = 1;
		terrain.GetComponent<Terrain>().terrainData.SetAlphamaps(0,0,alphatext);
		fieldData = new SFFD(terrain.GetComponent<Terrain>().terrainData.alphamapWidth,terrain.GetComponent<Terrain>().terrainData.alphamapHeight,2,
			terrain.GetComponent<Terrain>().terrainData.detailWidth,terrain.GetComponent<Terrain>().terrainData.detailHeight,terrain.GetComponent<Terrain>().terrainData.detailPrototypes.Length,
			terrain.GetComponent<Terrain>().terrainData.heightmapWidth,terrain.GetComponent<Terrain>().terrainData.heightmapHeight);
		fieldData.setHM (terrain.GetComponent<Terrain>().terrainData.GetHeights(0,0,terrain.GetComponent<Terrain>().terrainData.heightmapWidth,terrain.GetComponent<Terrain>().terrainData.heightmapHeight));
		fieldData.setAM (terrain.GetComponent<Terrain>().terrainData.GetAlphamaps(0,0,terrain.GetComponent<Terrain>().terrainData.alphamapWidth,terrain.GetComponent<Terrain>().terrainData.alphamapHeight));
		fieldData.setDM (terrain.GetComponent<Terrain>().terrainData.GetDetailLayer(0,0,terrain.GetComponent<Terrain>().terrainData.detailWidth,terrain.GetComponent<Terrain>().terrainData.detailHeight,1));
		fieldData.save();
	}
	
	/*
	 * This is ran during every frame.
	 * 
	 * 
	*/
	void Update(){
		TotalTime += Time.deltaTime;
		currentPoint = Mathf.Floor(TotalTime/lerpRate);
		float offsetTime = TotalTime - (currentPoint*lerpRate);
		if(currentPoint > waypoints.points.Count){
			if(isline){
				isline = false;
				waypoints.genPointsTurn(tractorAI.GetComponent<Transform>().position);
				TotalTime = 0;
			}
			else if(!isline){
				if(isPos){
					isline = true;
					isPos = false;
					waypoints.genPointsStr(tractorAI.GetComponent<Transform>().position,isPos);
					TotalTime = 0;
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
			Vector3 localPos = tractorAI.transform.position - terrain.transform.position;
			Vector3 normalPos = new Vector3((localPos.x/terrain.GetComponent<Terrain>().terrainData.size.x) * terrain.GetComponent<Terrain>().terrainData.alphamapWidth,
				0,
				(localPos.z/terrain.GetComponent<Terrain>().terrainData.size.z) * terrain.GetComponent<Terrain>().terrainData.alphamapHeight);
			Debug.Log(normalPos.ToString());
			alphatext[(int)normalPos.z,(int)normalPos.x,0] = 0;
			alphatext[(int)normalPos.z,(int)normalPos.x,1] = 1;
			terrain.GetComponent<Terrain>().terrainData.SetAlphamaps(0,0,alphatext);
	}
}
