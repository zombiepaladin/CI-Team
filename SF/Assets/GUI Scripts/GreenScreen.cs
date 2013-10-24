using UnityEngine;
using System.Collections;

public class GreenScreen : MonoBehaviour {
	
	public Material screen;
	
	Texture gpsMap;
	
	// Use this for initialization
	void Start () {
		screen.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnApplicationQuit(){
		screen.color = Color.green;
	}
	
	public void getTexture(){
		//get texture here
	}
}
