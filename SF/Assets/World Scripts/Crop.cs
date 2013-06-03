using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crop : MonoBehaviour{
	enum PlantType{
		corn = 0,
		wheat = 1,
		grass = 2,
	};
	
	public List<GameObject> crops;
	
	List<Vector3> positions = new List<Vector3>();
	
	PlantType crop = new PlantType();
	
	public Crop(int type){
		switch(type){
		case 0:
			crop = PlantType.corn;
			break;
		case 1:
			crop = PlantType.wheat;
			break;
		default:
			crop = PlantType.grass;
			break;
		}
	}
	
	public void PlantCrop(Vector3 pos){
		
		switch(crop){
		case PlantType.corn:
			//draw an instance of corn at pos
			break;
		case PlantType.wheat:
			//same as above
			break;
		}
	}
	
	public List<Vector3> getPositions(){
		return positions;
	}
}
