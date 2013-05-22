using UnityEngine;
using System.Collections;

public class MenuGUI : MonoBehaviour {
	
	void OnGUI(){
		if(GUI.Button(new Rect(0,0,150,100),"Load Tractor AI test")){
			Application.LoadLevel("AI Tractor");
		}
		if(GUI.Button(new Rect(0,100,150,100),"Load FPT Test")){
			Application.LoadLevel("FPTractorEnvierment");
		}
		if(GUI.Button(new Rect(0,200,150,100),"Load 3rd person Tractor")){
			Application.LoadLevel("TractorController");
		}
		if(GUI.Button (new Rect(0,300,150,100),"Load Terrain Test")){
			//load terrain test scene here...
		}
		if(GUI.Button(new Rect(0,400,150,100),"Exit")){
			Application.Quit();
		}
	}
}
