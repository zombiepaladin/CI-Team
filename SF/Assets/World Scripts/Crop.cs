using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crop{
	enum CropType{
		corn = 0,
		wheat = 1,
		soy = 2,
		grass,
	};
	
	GameObject cr;
	
	CropType ct = 0;
	
	List<GameObject> pantedCrops = new List<GameObject>();
	
	List<Vector3> positions = new List<Vector3>();
	

	public Crop(){
	}
	
	public void setType(int t){
		ct = (CropType)t;
	}
	
	public void PlantCrop(Vector3 pos){
		switch(ct){
		case CropType.corn:
			positions.Add(pos);
			pantedCrops.Add((GameObject)GameObject.Instantiate(GameObject.Find ("hay bale"),pos,Quaternion.identity));
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
	
	public List<GameObject> getPlantedCrops(){
		return pantedCrops;
	}
	
	public List<Vector3> getPositions(){
		return positions;	
	}
	public string getCrop(){
		return ct.ToString();
	}
	
	public List<Vector4> getPosAsVec4(){
		List<Vector4> v4list = new List<Vector4>();
		
		foreach(Vector3 v in positions){
			v4list.Add(new Vector4(v.x,v.y,v.z,(float)ct));
		}
		
		return v4list;
	}
	
	public bool lastPlant(Vector3 pos){
		if(pos.z >= positions[positions.Count-1].z){
			return true;
		}
		else{
			return false;
		}
	}
	
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
