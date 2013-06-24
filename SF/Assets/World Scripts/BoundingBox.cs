using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoundingBox : MonoBehaviour {
	
	public GameObject cube;
	
	public GameObject terrain;
	
	List<GameObject> instanceCubes = new List<GameObject>();
	
	int cn = 0;
	// Use this for initialization
	void Start () {
		for(int i = 0; i < 101;i++){
			instanceCubes.Add((GameObject)Instantiate(cube,new Vector3(0,1,i),Quaternion.identity));
			instanceCubes[cn].name = "BoundingCube"+cn;
			cn++;
		}
		for(int i = 1; i < 101;i++){
			instanceCubes.Add((GameObject)Instantiate(cube,new Vector3(i,1,0),Quaternion.identity));
			instanceCubes[cn].name = "BoundingCube"+cn;
			cn++;
		}
		
		for(int i = 100; i > 0; i--){
			instanceCubes.Add((GameObject)Instantiate(cube,new Vector3(100,1,i),Quaternion.identity));
			instanceCubes[cn].name = "BoundingCube"+cn;
			cn++;
		}
		for(int i = 100; i > 0; i--){
			instanceCubes.Add((GameObject)Instantiate(cube,new Vector3(i,1,100),Quaternion.identity));
			instanceCubes[cn].name = "BoundingCube"+cn;
			cn++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
