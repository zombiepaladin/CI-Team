using UnityEngine;
using System.Collections;

public class AnimalAI : MonoBehaviour {
	
	int time = 0;
	bool wentLeft = false;
	int eatTime = 0; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//if animation is running statement
		if(eatTime == time){
		int todo = Random.Range(0,1);
		if(todo == 0){
			if(wentLeft){
				//play move right animation
				wentLeft = false;
			}
			else{
				//play left animation
				wentLeft = true;
			}
		}
		
		if(todo == 1){
			eatTime = Random.Range (5*60,20*60);
			time = 0;
			//play eat animation	
		}
		}
		else{
			time++;
		}
	}
}
