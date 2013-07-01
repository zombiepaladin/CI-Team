using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoundingBox : MonoBehaviour {
	/// <summary>
	/// The size of the bounding box.
	/// </summary>
	public int sizeofbound;
	
	/// <summary>
	/// The cube that makes up the bounding box.
	/// </summary>
	public GameObject cube;
	
	/// <summary>
	/// The instanced cubes in a list.
	/// </summary>
	List<GameObject> instanceCubes = new List<GameObject>();
	
	/// <summary>
	/// The current number of cubes.
	/// </summary>
	int cn = 0;
	// Use this for initialization
	void Start () {
		
		int cubeSize = (int)cube.transform.localScale.z;
		Debug.Log (cubeSize);
		for(int i = 0; i < sizeofbound+1;i++){
			instanceCubes.Add((GameObject)Instantiate(cube,new Vector3(0,1,i),Quaternion.identity));
			instanceCubes[cn].name = "BoundingCube"+cn;
			cn++;
			if(cubeSize > 1){
				i += cubeSize-1;
			}
		}
		for(int i = 1; i < sizeofbound+1;i++){
			instanceCubes.Add((GameObject)Instantiate(cube,new Vector3(i,1,0),Quaternion.identity));
			instanceCubes[cn].name = "BoundingCube"+cn;
			cn++;
			if(cubeSize > 1){
				i += cubeSize-1;
			}
		}
		
		for(int i = sizeofbound; i > 0; i--){
			instanceCubes.Add((GameObject)Instantiate(cube,new Vector3(1000,1,i),Quaternion.identity));
			instanceCubes[cn].name = "BoundingCube"+cn;
			cn++;
			if(cubeSize > 1){
				i -= cubeSize-1;
			}
		}
		for(int i = sizeofbound; i > 0; i--){
			instanceCubes.Add((GameObject)Instantiate(cube,new Vector3(i,1,1000),Quaternion.identity));
			instanceCubes[cn].name = "BoundingCube"+cn;
			cn++;
			if(cubeSize > 1){
				i -= cubeSize-1;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
