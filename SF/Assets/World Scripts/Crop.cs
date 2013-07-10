using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A class for crops.
/// </summary>
public class Crop{
	
	/// <summary>
	/// Crop type.
	/// </summary>
	enum CropType{
		corn = 0,
		wheat = 1,
		soy = 2,
		grass,
	};
	
	/// <summary>
	/// What the game object is for the crop.
	/// </summary>
	GameObject cr;
	
	/// <summary>
	/// The crop type.
	/// </summary>
	CropType ct = 0;
	
	/// <summary>
	/// The panted crops stored as game objects.
	/// </summary>
	List<GameObject> pantedCrops = new List<GameObject>();
	
	/// <summary>
	/// The positions of the crops stored as vector3.
	/// </summary>
	List<Vector3> positions = new List<Vector3>();
	

	public Crop(){
	}
	
	/// <summary>
	/// Sets the type.
	/// </summary>
	/// <param name='t'>
	/// the type as an int.
	/// </param>
	public void setType(int t){
		ct = (CropType)t;
	}
	
	/// <summary>
	/// Plants the crop.
	/// </summary>
	/// <param name='pos'>
	/// Position as a vector 3.
	/// </param>
	public void PlantCrop(Vector3 pos){
		switch(ct){
		case CropType.corn:
			positions.Add(pos);
			pantedCrops.Add((GameObject)GameObject.Instantiate(GameObject.Find ("1Corn"),new Vector3(pos.x,pos.y-.25f,pos.z),Quaternion.identity));
			break;
			
		case CropType.wheat:
			positions.Add(pos);
			pantedCrops.Add((GameObject)GameObject.Instantiate(GameObject.Find ("hay bale"),pos,Quaternion.identity));
			break;
			
		case CropType.soy:
			positions.Add(pos);
			pantedCrops.Add((GameObject)GameObject.Instantiate(GameObject.Find ("hay bale"),pos,Quaternion.identity));
			break;
			
		default:
			positions.Add(pos);
			pantedCrops.Add((GameObject)GameObject.Instantiate(GameObject.Find ("hay bale"),pos,Quaternion.identity));
			break;
		}
	}
	
	/// <summary>
	/// Plants the crop.
	/// </summary>
	/// <param name='pos'>
	/// Position of the crop.
	/// </param>
	/// <param name='type'>
	/// Type of the crop.
	/// </param>
	public void PlantCrop(Vector3 pos, int type){
		switch(type){
		case 0:
			positions.Add(pos);
			pantedCrops.Add((GameObject)GameObject.Instantiate(GameObject.Find ("hay bale"),pos,Quaternion.identity));
			break;
			
		case 1:
			positions.Add(pos);
			pantedCrops.Add((GameObject)GameObject.Instantiate(GameObject.Find ("hay bale"),pos,Quaternion.identity));
			break;
			
		case 2:
			positions.Add(pos);
			pantedCrops.Add((GameObject)GameObject.Instantiate(GameObject.Find ("hay bale"),pos,Quaternion.identity));
			break;
			
		default:
			positions.Add(pos);
			pantedCrops.Add((GameObject)GameObject.Instantiate(GameObject.Find ("hay bale"),pos,Quaternion.identity));
			break;
		}
	}
	
	/// <summary>
	/// Gets the planted crops game object.
	/// </summary>
	/// <returns>
	/// The planted crops.
	/// </returns>
	public List<GameObject> getPlantedCrops(){
		return pantedCrops;
	}
	
	/// <summary>
	/// Gets the positions in the vector3 array.
	/// </summary>
	/// <returns>
	/// The positions.
	/// </returns>
	public List<Vector3> getPositions(){
		return positions;	
	}
	
	/// <summary>
	/// Gets the crop.
	/// </summary>
	/// <returns>
	/// The crop as a string.
	/// </returns>
	public string getCrop(){
		return ct.ToString();
	}
	
	/// <summary>
	/// Gets the position as vec4.
	/// </summary>
	/// <returns>
	/// The position as vec4.
	/// </returns>
	public List<Vector4> getPosAsVec4(){
		List<Vector4> v4list = new List<Vector4>();
		
		foreach(Vector3 v in positions){
			v4list.Add(new Vector4(v.x,v.y,v.z,(float)ct));
		}
		
		return v4list;
	}
	
	/// <summary>
	/// Lasts the plant.
	/// </summary>
	/// <returns>
	/// The plant.
	/// </returns>
	/// <param name='pos'>
	/// If set to <c>true</c> position.
	/// </param>
	public bool lastPlant(Vector3 pos){
		if(pos.z >= positions[positions.Count-1].z){
			return true;
		}
		else{
			return false;
		}
	}
	
	/// <summary>
	/// Sets the field.
	/// </summary>
	/// <param name='crops'>
	/// Crops in a vector4 format loaded from file.
	/// </param>
	public void setField(List<Vector4> crops){
		bool typeSet = false;
		foreach(Vector4 v in crops){
			if(!typeSet){
				setType((int)v.w);
				typeSet = true;
			}
			PlantCrop(new Vector3(v.x,v.y,v.z), (int) v.w);
		}
	}
}
