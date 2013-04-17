using UnityEngine;
using System.Collections;

public class LightBar : MonoBehaviour {
	
	public Material lightedLights;
	public Material darkenLights;
	public GameObject tractor;
	public GameObject terrain;
	WaypointFPT waypoints;
	bool isPos = true;
	bool isLine = true;
	// Use this for initialization
	void Start () {
		waypoints = new WaypointFPT(terrain);
		waypoints.genPointsStr(tractor.GetComponent<Transform>(),true);
	}
	
	// Update is called once per frame
	void Update () {

		if(waypoints.checkPath(tractor.GetComponent<Transform>()) != "Streight"){
			float hfo;
			if(isLine){
				hfo = waypoints.checkHowFarOff(tractor.GetComponent<Transform>(),isPos);
			}
			else{
				hfo = waypoints.checkHowFarOff(tractor.GetComponent<Transform>(),true);
			}
			
			// code to turn on lights		
		}
		waypoints.removePastPoint(tractor.GetComponent<Transform>());
		waypoints.endOfPath(tractor.GetComponent<Transform>());
	}
}
