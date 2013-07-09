using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class FieldGUI : MonoBehaviour {
	
	Dictionary<string,int> fnD = new Dictionary<string, int>();
	
	List<string> fns = new List<string>();
	
	
	string[] crops = {"corn","wheat","soy","grass"};
	
	bool nf = false;
	string fn = "Enter Field Name";
	string un = "UserName";
	Vector2 sb = Vector2.zero;
	int selected = 0;
	
	bool empty = true;
	
	void Start(){
		Application.ExternalCall("getField");
	}
	
	public void getFields(string[] a, string[] b){
		for(int i = 0; i < a.Length;i++){
			fnD.Add(b[i],int.Parse(a[i]));
			fns.Add(b[i]);
		}
		empty = false;
	}
	
	void OnGUI(){
		/*
		 * this if is for the loading field screen.
		 * 
		 */
		if(!nf){
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
			if(!empty){
			foreach(string s in fns){
				if(GUILayout.Button(s)){
					PlayerPrefs.SetString("FieldName",s);
					PlayerPrefs.SetInt ("FieldPK",fnD[s]);
					Application.LoadLevel("AI Tractor");
				}
			}
			}
			GUILayout.EndScrollView();
			GUILayout.FlexibleSpace();
    		GUILayout.EndVertical();
    		GUILayout.EndArea();
		}
		
		/*
		 * this if statement is for new terrains
		 * 
		 */
		else if(nf){
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
		
	}
}
