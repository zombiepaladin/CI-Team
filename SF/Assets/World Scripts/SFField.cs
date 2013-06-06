using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Serialization;
using System.IO;

public class SFField {
	[SerializeThis]
	int hmWidth;
	
	[SerializeThis]
 	int hmHeight;
	
	[SerializeThis]
	int amWidth;
	
	[SerializeThis]
	int amHeight;
	
	[SerializeThis]
	int amLayers;
	
	[SerializeThis]
	float[,] hm;
	
	[SerializeThis]
	float[,,] am;
	
	[SerializeThis]
	List<Vector4> plantPos = new List<Vector4>();
	
	
	public void Save(string fn){
		byte[] seralized = UnitySerializer.Serialize(this);
		BS.Save(seralized,fn);
	}
	
	public byte[] Load(string fn){
		byte[] seralized = BS.Load(fn);
		return seralized;
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
	
	public float[,] getHM(){
		return hm;
	}
	
	public float[,,] getAM(){
		return am;
	}
	
	public List<Vector4> getCrops(){
		return plantPos;
	}
	
	public void addPlant(Vector4 crop){
		plantPos.Add(crop);
	}
	
	public void setCrops(List<Vector4> cropPos){
		plantPos = cropPos;
	}
	
}
