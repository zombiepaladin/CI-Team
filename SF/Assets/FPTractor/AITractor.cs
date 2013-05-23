using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Plugins;

public class AITractor : MonoBehaviour {
	
	public GameObject objPre;
	public Transform thisPos;
	public GameObject terrain;
	public Texture dugup;
	private GameObject tractorAI;
	bool isline = true;
	bool isPos = true;
	int currentPoint = 1;
	float speed = 50.0f;
	WaypointFPT waypoints;
	
	// Use this for initialization
	void Start () {
		HOTween.Init(true,true,true);
		HOTween.EnableOverwriteManager();
		waypoints = new WaypointFPT(terrain);
		waypoints.genPointsStr(new Vector3(50,0,50),true);
		tractorAI = (GameObject)Instantiate(objPre,new Vector3(50,0,50),Quaternion.identity);
		tractorAI.name = "AI Tractor FP";
		HOTween.To(tractorAI.GetComponent<Transform>(),1.0f,"position",waypoints.points[1]);
		currentPoint++;
		Debug.Log("Terrain size " + terrain.GetComponent<Terrain>().terrainData.size.z.ToString());
	}
	
	void Update(){
		
		
		//have terrain texture update here
		if(!HOTween.IsTweening(tractorAI.GetComponent<Transform>())){
			if(waypoints.endOfPath(tractorAI.GetComponent<Transform>().position)){
				if(isline){
					isline = false;
					waypoints.genPointsTurn(tractorAI.GetComponent<Transform>().position);
					currentPoint = 1;
					HOTween.To(tractorAI.GetComponent<Transform>(),1.0f,"position",waypoints.points[1]);
					currentPoint++;
				}
				else if(!isline){
					if(isPos){
						isline = true;
						isPos = false;
						waypoints.genPointsStr(tractorAI.GetComponent<Transform>().position,isPos);
						currentPoint = 1;
						HOTween.To(tractorAI.GetComponent<Transform>(),10.0f,"position",waypoints.points[1]);
						currentPoint++;
						Debug.Log ("Current Point Pos " + waypoints.points[1].ToString());
					}
					else if(!isPos){
						Debug.Log ("Current Point Pos " + waypoints.points[1].ToString());
						isline = true;
						isPos = true;
						waypoints.genPointsStr(tractorAI.GetComponent<Transform>().position,isPos);
						currentPoint = 1;
						HOTween.To(tractorAI.GetComponent<Transform>(),10.0f,"position",waypoints.points[1]);
						currentPoint++;
						
					}
				}
			}
			else{
				Debug.Log ("Current Point Pos " + waypoints.points[currentPoint].ToString());
				HOTween.To(tractorAI.GetComponent<Transform>(),1.0f,"position",waypoints.points[currentPoint]);
				currentPoint++;
			}
		}
		/*
		 * This if Statement generates the waypoints after the intital waypoints.
		 */ 

	}
}
