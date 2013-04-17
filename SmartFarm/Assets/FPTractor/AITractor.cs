using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Plugins;

public class AITractor : MonoBehaviour {
	
	public GameObject objPre;
	
	public GameObject pathPre;
	
	public GameObject terrain;
	
	public GameObject thisPos;
	
	public Texture dugup;
	private GameObject newPath;
	private GameObject tractorAI;
	private Transform movingPos;
	private iMove im;
	private List<Transform> wps = new List<Transform>();
	bool line = true;
	bool isPos = true;
	int j = 0;
	Vector3 sp = new Vector3();
	// Use this for initialization
	void Start () {
		
		movingPos = thisPos.GetComponent<Transform>();
		movingPos.position = new Vector3(50,0,50);
		wps.Add(movingPos);
		wps[0].name = "WaypointStart";
		int k = (int)movingPos.position.z;
		int i = 1;
		while(k < terrain.GetComponent<Terrain>().terrainData.size.z-50){
			GameObject t = new GameObject();
			
			t.GetComponent<Transform>().position = new Vector3(50,0,k+50);
			if(t.GetComponent<Transform>().position == terrain.GetComponent<Terrain>().terrainData.size){
				wps.Add(t.GetComponent<Transform>());
				wps[i].name = "WaypointEnd";
			}
			else{
				wps.Add(t.GetComponent<Transform>());
				wps[i].name = "Waypoint"+(i);
			}
			i++;
			k+=50;
		}
		movingPos.position = new Vector3(50,0,50);
		pathPre.GetComponent<Transform>().position = new Vector3(50,0,50);
		pathPre.AddComponent<PathManager>();
		pathPre.GetComponent<PathManager>().waypoints = wps.ToArray();
		pathPre.GetComponent<PathManager>().drawCurved = false;
		tractorAI = (GameObject)Instantiate(objPre,new Vector3(50,0,50),Quaternion.identity);
		tractorAI.name = "AI Tractor FP";
		newPath = (GameObject)Instantiate(pathPre,new Vector3(50,0,50),Quaternion.identity);
		newPath.name = "Tractor Path l " + j;
		WaypointManager.AddPath(newPath);
		im = tractorAI.GetComponent<iMove>();
		im.SetPath(WaypointManager.Paths[newPath.name]);
		j++;
	}
	
	void Update(){
		//have terrain texture update here
		
		if(Input.GetKeyDown(KeyCode.PageUp)){
			tractorAI.GetComponent<iMove>().speed += 5;
		}
		if(Input.GetKeyDown (KeyCode.PageDown)){
			tractorAI.GetComponent<iMove>().speed -=5;
		}
		
		/*
		 * This if Statement generates the waypoints after the intital waypoints.
		 */ 
		if(tractorAI.GetComponent<Transform>().position == wps[wps.Count-1].position){
			if(line && tractorAI.GetComponent<Transform>().position.x+50 == terrain.GetComponent<Terrain>().terrainData.size.x){
				//stop the simulation and display resluts.
			}
			else if(line){
				wps.Clear();
				WaypointManager.Destroy(WaypointManager.Paths[newPath.name]);
				line = false;
				GameObject t = new GameObject();
				t.GetComponent<Transform>().position = new Vector3(tractorAI.GetComponent<Transform>().position.x,0,tractorAI.GetComponent<Transform>().position.z);
				wps.Add (t.GetComponent<Transform>());
				wps[0].name = "WayStart";
				int k = 0;
				int i = 1;
				int z = 1;
				while(k < 50){
					GameObject t2 = new GameObject();
					if(2*i < 10){
						t2.GetComponent<Transform>().position = new Vector3(tractorAI.GetComponent<Transform>().position.x+(5*i),0,wps[0].position.z+(2*i));
					}
					else if(2*i == 10){
						t2.GetComponent<Transform>().position = new Vector3(tractorAI.GetComponent<Transform>().position.x+(5*i),0,wps[0].position.z+10);
					}
					else if(2*i > 10){
						t2.GetComponent<Transform>().position = new Vector3(tractorAI.GetComponent<Transform>().position.x+(5*i),0,wps[0].position.z+(10-(2*z)));
						z++;
					}
					wps.Add(t2.GetComponent<Transform>());
					if(t2.GetComponent<Transform>().position == terrain.GetComponent<Terrain>().terrainData.size){
						wps[i].name = "WayEnd";
					}
					else{
						wps[i].name = "Waypoint "+i;
					}
					k += 5;
					i++;
				}
				pathPre.GetComponent<PathManager>().waypoints = wps.ToArray();
				pathPre.GetComponent<PathManager>().drawStraight = false;
				pathPre.GetComponent<PathManager>().drawCurved = true;
				pathPre.GetComponent<Transform>().position = new Vector3(wps[0].position.x,0,wps[0].position.z);
				newPath = (GameObject)Instantiate(pathPre,wps[0].position,Quaternion.identity);
				newPath.name = "TractorPath c" + j;
				j++;
				WaypointManager.AddPath(newPath);
				im = tractorAI.GetComponent<iMove>();
				im.SetPath (WaypointManager.Paths[newPath.name]);
				if(isPos){
					isPos = false;
				}
				else if(!isPos){
					isPos = true;
				}
			}
			else if(!line){
				WaypointManager.Destroy(WaypointManager.Paths[newPath.name]);
				line = true;
				wps.Clear();
				GameObject t = new GameObject();
				t.GetComponent<Transform>().position = new Vector3(tractorAI.GetComponent<Transform>().position.x,0,tractorAI.GetComponent<Transform>().position.z);
				wps.Add(t.GetComponent<Transform>());
				wps[0].name = "WaypointStart";
				int k = (int)movingPos.position.z;
				int i = 1;
				if(isPos){
					while(k <= terrain.GetComponent<Terrain>().terrainData.size.z-50){
						GameObject t2 = new GameObject();
						t2.GetComponent<Transform>().position = new Vector3(wps[0].position.x,0,k+50);
						wps.Add(t2.GetComponent<Transform>());
						wps[i].name = "Waypoint"+(i);
						i++;
						k+=50;
					}
				}
				else if(!isPos){
					while(k > 50){
						GameObject t2 = new GameObject();
						t2.GetComponent<Transform>().position = new Vector3(wps[0].position.x,0,k-50);
						wps.Add(t2.GetComponent<Transform>());
						wps[i+1].name = "Waypoint"+(i+1);
						i++;
						k -=50;
					}
				}
				movingPos.position = new Vector3(movingPos.position.x,0,movingPos.position.z);
				pathPre.GetComponent<Transform>().position = movingPos.position;
				pathPre.GetComponent<PathManager>().waypoints = wps.ToArray();
				pathPre.GetComponent<PathManager>().drawCurved = false;
				pathPre.GetComponent<PathManager>().drawStraight = true;
				newPath = (GameObject)Instantiate(pathPre,movingPos.position,Quaternion.identity);
				newPath.name = "Tractor Path l " + j;
				WaypointManager.AddPath(newPath);
				im = tractorAI.GetComponent<iMove>();
				im.SetPath(WaypointManager.Paths[newPath.name]);
				j++;
			}
		}
	}
	
}
