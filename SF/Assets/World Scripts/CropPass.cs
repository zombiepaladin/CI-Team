using UnityEngine;
using System.Collections;

public class CropPass : MonoBehaviour {
	public GameObject corn;
	public GameObject wheat;
	public GameObject soy;
	public GameObject grass;
	
	public GameObject getCrop(int type){
		switch(type){
		case 0:
			return corn;
			break;
		case 1:
			return wheat;
			break;
		case 2:
			return soy;
			break;
		case 3:
			return grass;
			break;
		}
		return grass;
	}
}
