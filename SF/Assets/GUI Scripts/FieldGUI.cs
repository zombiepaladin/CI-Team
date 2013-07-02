using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class FieldGUI : MonoBehaviour {
	
	//string[] files;
	
	string[] crops = {"corn","wheat","soy","grass"};
	
	bool nf = false;
	bool une = true;
	string fn = "Enter Field Name";
	string un = "UserName";
	Vector2 sb = Vector2.zero;
	int selected = 0;
	
	void Start(){
		//files = System.IO.Directory.GetFiles(Application.dataPath + "\\SFFields\\");
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
			sb = GUILayout.BeginScrollView(sb,gs);
//			foreach(string s in files){
//				string s1 = s.Remove(0, (Application.dataPath + "\\SFFields\\").Length);
//				if(!s1.Contains(".meta")){
//				s1 = s1.Remove (s1.IndexOf('.'));
//				if(GUILayout.Button(s1)){
//					PlayerPrefs.SetString("FieldName",s1);
//					Application.LoadLevel("AI Tractor");
//				}
//				}
			//}
			GUILayout.EndScrollView();
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
			sb = GUILayout.BeginScrollView(sb,gs);
			selected = GUILayout.SelectionGrid(selected,crops,1);
			GUILayout.EndScrollView();
			if(GUILayout.Button("OK")){
				PlayerPrefs.SetString("NFName",fn);
				PlayerPrefs.SetInt("CropType",selected);
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
			un = GUILayout.TextField(un);
			if(GUILayout.Button("Ok")){
				PlayerPrefs.SetString("UserName",un);
				une = true;
			}
			GUILayout.FlexibleSpace();
    		GUILayout.EndVertical();
    		GUILayout.EndArea();
		}
	}
}
