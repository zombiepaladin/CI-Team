using UnityEngine;
using System.Collections;

public class Return : MonoBehaviour {
	
	void OnGUI(){
		if(GUI.Button(new Rect(0,0,150,100),"Return to menu")){
			Application.LoadLevel("Test_Main_Menu");
		}
	}
}
