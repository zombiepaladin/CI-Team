using UnityEngine;
using System.Collections;

public class EnterExit : MonoBehaviour {
	
	public Transform vehicle;
	public Transform player;
	public Transform exitPoint;
	public Transform doorTriggerLeft;
	public Camera PlayerCamera;
	public Camera CarCamera;
	public bool isPlayerVisible;
	
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.E) && isPlayerVisible){
			//set player invisable
			player.gameObject.SetActive(false);
			//set the exit parent
			player.parent = exitPoint.transform;
			player.transform.localPosition = new Vector3(-1.5f,0.0f,0.0f);
			//parent player to car;
			exitPoint.parent = vehicle.transform;
			exitPoint.transform.localPosition = new Vector3(-0.5f,0.0f,0.0f);
			//enable driving
			(GameObject.Find("TractorFP").GetComponent("FPTractorController") as MonoBehaviour).enabled = true;
			(GameObject.Find("TractorFP").GetComponent("FPTractorMoter") as MonoBehaviour).enabled = true;
			(GameObject.Find("FarmerFP").GetComponent("MouseLook") as MonoBehaviour).enabled = false;
			(GameObject.Find("FarmerFP").GetComponent("CharacterMoter") as MonoBehaviour).enabled = false;
			(GameObject.Find("FarmerFP").GetComponent("FPSInputController") as MonoBehaviour).enabled = false;
			PlayerCamera.enabled = false;
			CarCamera.enabled = true;
			isPlayerVisible = false;
		}
		else{
			if(Input.GetKeyUp(KeyCode.R)){
				//set player invisable
				player.gameObject.SetActive(true);
				//set the exit parent
				player.transform.parent = null;
				//parent player to car;
				exitPoint.parent = vehicle.transform;
				//enable walking
				(GameObject.Find("TractorFP").GetComponent("FPTractorController") as MonoBehaviour).enabled = false;
				(GameObject.Find("TractorFP").GetComponent("FPTractorMoter") as MonoBehaviour).enabled = false;
				(GameObject.Find("FarmerFP").GetComponent("MouseLook") as MonoBehaviour).enabled = true;
				(GameObject.Find ("FarmerFP").GetComponent("CharacterMoter") as MonoBehaviour).enabled = true;
				(GameObject.Find("FarmerFP").GetComponent("FPSInputController") as MonoBehaviour).enabled = true;
				PlayerCamera.enabled = true;
				CarCamera.enabled = false;
				isPlayerVisible = true;
			}
		}
	}
void OnTriggerEnter(Collider Player){
		isPlayerVisible = true;
	}
	
void OnTriggerExit(Collider Player){
		isPlayerVisible = false;
	}
}

