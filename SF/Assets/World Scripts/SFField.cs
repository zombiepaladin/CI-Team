using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Serialization;
using System.IO;

/// <summary>
/// SF field file that is serialized using UnitySerializer and compressed and writen to file using my own static class.
/// </summary>
public class SFField {
	[SerializeThis]
	/// <summary>
	/// The player position.
	/// </summary>
	Vector3 playerPos;
	
	[SerializeThis]
	/// <summary>
	/// The width of the hm.
	/// </summary>
	int hmWidth;
	
	[SerializeThis]
	/// <summary>
	/// The height of the hm.
	/// </summary>
 	int hmHeight;
	
	[SerializeThis]
	/// <summary>
	/// The width of the am.
	/// </summary>
	int amWidth;
	
	[SerializeThis]
	/// <summary>
	/// The height of the am.
	/// </summary>
	int amHeight;
	
	[SerializeThis]
	/// <summary>
	/// The am layers.
	/// </summary>
	int amLayers;
	
	[SerializeThis]
	/// <summary>
	/// The hm.
	/// </summary>
	float[,] hm;
	
	[SerializeThis]
	/// <summary>
	/// The am.
	/// </summary>
	float[,,] am;
	
	[SerializeThis]
	/// <summary>
	/// The plant position.
	/// </summary>
	List<Vector4> plantPos = new List<Vector4>();
	
	
	/// <summary>
	/// Save the object after it is seralized.
	/// </summary>
	/// <param name='fn'>
	/// File name.
	/// </param>
	public void Save(string fn){
		byte[] seralized = UnitySerializer.Serialize(this);
		BS.Save(seralized,fn);
	}
	
	public void SaveToWeb(string fn){
		byte[] seralized = UnitySerializer.Serialize(this);
		BS.SaveToWeb(seralized,fn,"bob");
	}
	
	/// <summary>
	/// Load the byte array of a file.
	/// </summary>
	/// <param name='fn'>
	/// File name.
	/// </param>
	public byte[] Load(string fn){
		byte[] seralized = BS.Load(fn);
		return seralized;
	}
	
	/// <summary>
	/// Sets the height map.
	/// </summary>
	/// <param name='h'>
	/// passed in height map.
	/// </param>
	/// <param name='width'>
	/// Height Map Width.
	/// </param>
	/// <param name='height'>
	/// Height Map Height.
	/// </param>
	public void setHM(float[,] h, int width, int height){
		hm = h;
		hmWidth = width;
		hmHeight = height;
	}
	
	/// <summary>
	/// Sets the Alpha Map.
	/// </summary>
	/// <param name='a'>
	/// Passed in Alpha Map.
	/// </param>
	/// <param name='width'>
	/// Alpha map Width.
	/// </param>
	/// <param name='height'>
	/// Alpha Map Height.
	/// </param>
	/// <param name='layers'>
	/// Alpha Map Layers.
	/// </param>
	public void setAM(float[,,] a, int width, int height, int layers){
		am = a;
		amWidth = width;
		amHeight = height;
		amLayers = layers;
	}
	
	/// <summary>
	/// Gets the Height Map.
	/// </summary>
	/// <returns>
	/// The Height Map.
	/// </returns>
	public float[,] getHM(){
		return hm;
	}
	
	/// <summary>
	/// Gets the Alpha Map.
	/// </summary>
	/// <returns>
	/// The Aalpha Map.
	/// </returns>
	public float[,,] getAM(){
		return am;
	}
	

	
	/// <summary>
	/// Adds a single Crop to the List of crops.
	/// </summary>
	/// <param name='crop'>
	/// Crops.
	/// </param>
	public void addPlant(Vector4 crop){
		plantPos.Add(crop);
	}
	
	/// <summary>
	/// Sets the crops.
	/// </summary>
	/// <param name='cropPos'>
	/// List of Crop position.
	/// </param>
	public void setCrops(List<Vector4> cropPos){
		plantPos = cropPos;
	}
	
	/// <summary>
	/// Gets the crops.
	/// </summary>
	/// <returns>
	/// The crops.
	/// </returns>
	public List<Vector4> getCrops(){
		return plantPos;
	}
	
	public void setPP(Vector3 v){
		playerPos = v;
	}
	
	public Vector3 getPP(){
		return playerPos;
	}
	
}
