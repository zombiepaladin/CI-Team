using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Plugins;

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
	 * This class also uses the HOTween plug in which is distributed for free on the Unity asset store. 
	 * Documentation for the class can be found at http://www.holoville.com/hotween/documentation.html
	 * 
	 * 
	*/
	public GameObject objPre;
	public Transform thisPos;
	public GameObject terrain;
	public Texture dugup;
	private GameObject tractorAI;
	bool isline = true;
	bool isPos = true;
	float currentPoint = 0;
	WaypointFPT waypoints;
	float lerpAmout = 0;
	float lerpRate = 1.0f; 
	float TotalTime = 0;
	/*
	 * This is ran during the start up of the scene.
	 * 
	 * This intilizes HOTween and generates the first line of waypoints.
	 * 
	*/ 
	void Start () {
		HOTween.Init(true,true,true);
		HOTween.EnableOverwriteManager();
		waypoints = new WaypointFPT(terrain);
		waypoints.genPointsStr(new Vector3(50,0,50),true);
		tractorAI = (GameObject)Instantiate(objPre,new Vector3(50,0,50),Quaternion.identity);
		tractorAI.name = "AI Tractor FP";

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
		Debug.Log ("Current Pos " + currentPoint);
		//have terrain texture update here
	}
}
