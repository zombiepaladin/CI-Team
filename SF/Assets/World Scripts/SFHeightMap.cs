using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class SFHeightMap{
	float[,] hm;
	int hmHeight;
	int hmWidth;
	
	public SFHeightMap(int width, int height, bool gen){
		hmHeight = height;
		hmWidth = width;
		hm = new float[hmWidth,hmHeight];
		if(gen){
			genHM ();
		}
		else{
			genStdHM();
		}
	}
	
	public void SaveTerrain() {
		string filename = EditorUtility.SaveFilePanel("Save a Hight Map","","CurrentHM.sfhm","sfhm");
		if(filename == null){
			EditorUtility.DisplayDialog("Invalid Name!","You must name the file","Ok");
			return;
		}
        FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite);
        StreamWriter sw = new StreamWriter(fs);
		for(int i = 0; i < hmWidth; i++) {
			if(i > 0){
				sw.Write ("\n");
			}
            for(int j = 0; j < hmHeight; j++) {
				if(j > 0){
					sw.Write (" ");
				}
				sw.Write(hm[i,j]);
            }
        }
		
        sw.Close();
		fs.Close();
    }
  	public float[,] LoadTerrain() {
		string filename = EditorUtility.OpenFilePanel("Open a Hight Map","",".sfhm");
		if(filename == null){
			EditorUtility.DisplayDialog("Invalid Name!","You must select a sfhm file","Ok");
			return null;
		}
        FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite);
        StreamReader sr = new StreamReader(fs);
        sr.BaseStream.Seek(0, SeekOrigin.Begin);
		string rin = " ";
		int i = 0;
		while(sr.Peek() >= 0){
			rin = sr.ReadLine();
			rin.Trim();
			string[] rinS = rin.Split(' ');
			for(int k = 0; k < rinS.Length; k++){
				hm[i,k] = float.Parse(rinS[k]);
			}
			i++;
		}
        sr.Close();
		fs.Close();
        return hm;
    }
	
	public float[,] getHM(){
		return hm;
	}
	
   	void genStdHM(){
		for(int i = 0; i < hmWidth; i++){
			for(int j = 0; j < hmHeight; j++){
				hm[i,j] = 0;
			}
		}
	}
	void genHM(){
		for(int i = 0; i < hmWidth; i++){
			for(int j = 0; j < hmHeight; j++){
				hm[i,j] = Random.Range(0.0f,0.1f)* 0.05f;
			}
		}
	}
}
