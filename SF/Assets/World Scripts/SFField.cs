using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Serialization;

public class SFField {
	
	int hmWidth;
 	int hmHeight;
	int amWidth;
	int amHeight;
	int amLayers;
	
	[SerializeThis]
	float[,] hm;
	
	[SerializeThis]
	float[,,] am;
	
	[SerializeThis]
	List<Vector4> plantPos = new List<Vector4>();
	
	
	public void Save(){
		UnitySerializer.WriteToFile(UnitySerializer.JSONSerialize(this),"field.txt");
	}
	
	public void setHM(float[,] h, int width, int height){
		hm = h;
		hmWidth = width;
		hmHeight = height;
	}
	
	public void setAM(float[,,] a, int width, int height, int layers){
		am = a;
		amWidth = width;
		amHeight = height;
		amLayers = layers;
	}
	
	public void addPlant(Vector4 crop){
		plantPos.Add(crop);
	}
	
	public void setCrops(List<Vector4> cropPos){
		plantPos = cropPos;
	}
	
}
