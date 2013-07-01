using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crop{
	
	GameObject cr;
	
	List<GameObject> pantedCrops = new List<GameObject>();
	
	List<Vector3> positions = new List<Vector3>();
	

	public Crop(GameObject c){
		cr = c;
	}
	
	public void PlantCrop(Vector3 pos){
		positions.Add(pos);
		pantedCrops.Add((GameObject)GameObject.Instantiate(cr,pos,Quaternion.identity));
	}
	
	public List<GameObject> getPlantedCrops(){
		return pantedCrops;
	}
	
	public List<Vector3> getPositions(){
		return positions;	
	}
	public GameObject getCrop(){
		return cr;
	}
}
