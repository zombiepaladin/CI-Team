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
	int currentPoint = 1;
	WaypointFPT waypoints;
	
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
		HOTween.To(tractorAI.GetComponent<Transform>(),1.0f,"position",waypoints.points[1]);
		currentPoint++;
	}
	
	/*
	 * This is ran during every frame.
	 * 
	 * The if statement sees if the tween is compleated.
	 * 	It then checks to see if it is the end of the path.
	 * 	if it is not then it generates the next tween.
	 * 	If it is at the end it sees if it isline is ture (as isline is true when you are in a line)
	 * 	If isline is false (as the turn was just compleated) it then checks if isPos is ture or not.
	 *  If isPos is ture it calculates a negitive line(as isPos is true when you finish a positive line)
	 * 	During all of these commands it generates new waypoints for the path and then creates a Tween.
	 * 
	*/
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
					}
					else if(!isPos){;
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
				HOTween.To(tractorAI.GetComponent<Transform>(),1.0f,"position",waypoints.points[currentPoint]);
				currentPoint++;
			}
		}

	}
}
