using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.Serialization;

[System.Serializable]
public class SFFD{
	[SerializeField]
	int amWidth;
	[SerializeField]
	int amHeight;
	[SerializeField]
	int amLayers;
	[SerializeField]
	int dWidth;
	[SerializeField]
	int dHeight;
	[SerializeField]
	int dLayers;
	[SerializeField]
	int hmWidth;
	[SerializeField]
	int hmHeight;
	
	[SerializeField]
	float[,] hm;
	
	[SerializeField]
	float[,,] alphamap;
	[SerializeField]
	int[,] detailmap;
	
	
	public SFFD(int amwidth, int amheight, int amlayers, int dwidth, int dheight, int dnumlayers, int hmwidth, int hmheight){
		amWidth = amwidth;
		amHeight = amheight;
		amLayers = amlayers;
	 	dWidth = dwidth;
		dHeight = dheight;
		dLayers = dnumlayers;
		hmWidth = hmwidth;
		hmHeight = hmheight;
		hm = new float[hmWidth,hmHeight];
		alphamap = new float[amWidth,amHeight,amLayers];
		detailmap = new int[dWidth,dHeight];
	}
	
	public void save(){
		
	}
	
	
	public void setHM(float[,] h){
		hm = h;
	}
	
	public void setAM(float[,,] a){
		alphamap = a;
	}
	
	public void setDM(int[,] d){
		detailmap = d;
	}
	
  	public float[,] LoadHM() {
		
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
