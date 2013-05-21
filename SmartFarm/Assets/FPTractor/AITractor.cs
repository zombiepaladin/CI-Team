using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AITractor : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
		
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
	}
	
}
