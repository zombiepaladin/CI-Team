using UnityEngine;
using System.Collections;

public class MenuGUI : MonoBehaviour {
	
	void OnGUI(){
		if(GUI.Button(new Rect(0,0,150,100),"Load Tractor AI test")){
			Application.LoadLevel("Load_Terrain");
		}
		if(GUI.Button(new Rect(0,100,150,100),"Load FPT Test")){
			Application.LoadLevel("FPTractorEnvierment");
		}
		if(GUI.Button(new Rect(0,200,150,100),"Load 3rd person Tractor")){
			Application.LoadLevel("TractorController");
		}
		if(GUI.Button (new Rect(0,300,150,100),"Load Terrain Test")){
			Application.LoadLevel("Terrain_Tests");
		}
		if(GUI.Button(new Rect(0,400,150,100),"Load Farmer stuff")){
			Application.LoadLevel ("Test_Farm");
		}
		if(GUI.Button(new Rect(0,500,150,100),"Info")){
			Application.OpenURL("https://github.com/zombiepaladin/CI-Team/wiki");
		}
	}
}
