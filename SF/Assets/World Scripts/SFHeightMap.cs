using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Smart Farm Height Map.
/// </summary>
public class SFFieldData{
	
	float[,] hm;
	float[,,] am;
	int[,] dm;
	int hmHeight;
	int hmWidth;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="SFHeightMap"/> class.
	/// </summary>
	/// <param name='width'>
	/// Width of height map.
	/// </param>
	/// <param name='height'>
	/// Height of height map.
	/// </param>
	/// <param name='gen'>
	/// A bool to see if you want flat or modeled terrain.
	/// </param>
	public SFFieldData(int HMwidth, int HMheight, bool gen){
		hmHeight = HMheight;
		hmWidth = HMwidth;
		hm = new float[hmWidth,hmHeight];
		if(gen){
			genHM ();
		}
		else{
			genStdHM();
		}
	}
	
	/// <summary>
	/// Saves the terrain in a SFHM file (A fancy text file).
	/// </summary>
	public void SaveTerrain(string filename) {
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
	
	/// <summary>
	/// Loads the terrain from a SFHM file.
	/// </summary>
	/// <returns>
	/// The terrain.
	/// </returns>
  	public float[,] LoadTerrain() {
        FileStream fs = new FileStream("", FileMode.Open, FileAccess.ReadWrite);
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
	
	/// <summary>
	/// Gets the Height map.
	/// </summary>
	/// <returns>
	/// The Height map.
	/// </returns>
	public float[,] getHM(){
		return hm;
	}
	
	/// <summary>
	/// Generates a flat height map.
	/// </summary>
   	void genStdHM(){
		for(int i = 0; i < hmWidth; i++){
			for(int j = 0; j < hmHeight; j++){
				hm[i,j] = 0;
			}
		}
	}
	
	/// <summary>
	/// Generates a random height map.
	/// </summary>
	void genHM(){
		for(int i = 0; i < hmWidth; i++){
			for(int j = 0; j < hmHeight; j++){
				hm[i,j] = Random.Range(0.0f,0.1f)* 0.05f;
			}
		}
	}
}
