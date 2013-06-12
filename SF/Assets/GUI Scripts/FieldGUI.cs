using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class FieldGUI : MonoBehaviour {
	
	string[] files;
	
	//mySQLScripts sql = new mySQLScripts();
	bool nf = false;
	bool une = false;
	string fn = "Enter Field Name";
	string un = "UserName";
	
	void Start(){
		//sql.Connect();
	}
	
	void OnGUI(){
		if(!nf && une){
			GUIStyle gs = "box";
			GUILayout.BeginArea(new Rect(Screen.width/2 - 200,Screen.height/2 - 300, 400, 600),gs);
			GUILayout.BeginVertical(); 
    		GUILayout.FlexibleSpace();
			GUILayout.Space(60);
			if(GUILayout.Button("New Field")){
				PlayerPrefs.SetString("FieldName","");
				nf = true;
			}
			foreach(string s in files){
				string s1 = s.Remove(0, (Application.dataPath + "\\SFFields\\").Length);
				if(!s1.Contains(".meta")){
				s1 = s1.Remove (s1.IndexOf('.'));
				if(GUILayout.Button(s1)){
					PlayerPrefs.SetString("FieldName",s1);
					Application.LoadLevel("AI Tractor");
				}
				}
			}
			GUILayout.FlexibleSpace();
    		GUILayout.EndVertical();
    		GUILayout.EndArea();
		}
		else if(nf && une){
			GUIStyle gs = "box";
			GUILayout.BeginArea(new Rect(Screen.width/2 - 200,Screen.height/2 - 300, 400, 600),gs);
			GUILayout.BeginVertical(); 
    		GUILayout.FlexibleSpace();
			fn = GUILayout.TextField(fn);
			if(GUILayout.Button("OK")){
				PlayerPrefs.SetString("NFName",name);
				Application.LoadLevel("AI Tractor");
			}
			if(GUILayout.Button("Cancel")){
				nf = false;
			}
			GUILayout.FlexibleSpace();
    		GUILayout.EndVertical();
    		GUILayout.EndArea();
		}
		else{
			GUIStyle gs = "box";
			GUILayout.BeginArea(new Rect(Screen.width/2 - 200,Screen.height/2 - 300, 400, 600),gs);
			GUILayout.BeginVertical(); 
    		GUILayout.FlexibleSpace();
			GUILayout.Space(60);
			GUILayout.TextField(un);
			if(GUILayout.Button("Ok")){
				PlayerPrefs.SetString("UserName",un);
				//files = sql.GetFields(un).ToArray();
				une = true;
			}
		}
	}
}
