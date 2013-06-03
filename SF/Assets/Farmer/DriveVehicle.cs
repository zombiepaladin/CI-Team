using UnityEngine;
using System.Collections;

/// <summary>
/// A MonoBehaviour that is designed to have the farmer get in the vehicle.
/// </summary>
public class DriveVehicle : MonoBehaviour {
	/// <summary>
	/// The player object.
	/// </summary>
	public GameObject playerObj;
	/// <summary>
	/// The player camera.
	/// </summary>
	public Camera playerCamera;
	/// <summary>
	/// Colider that is the trigger to see if the player is in there.
	/// </summary>
	public Transform doorTriggerLeft;
	/// <summary>
	/// Is the player visible (Default is false).
	/// </summary>
	public bool isPlayerVisible;
	/// <summary>
	/// Is the key press (Default is false).
	/// </summary>
	bool isKeyPressed = false;
	
	/* 
	 * Update is called once per frame.
	 * 
	 * this sees if the E key is press when the player is visible and is so disables the moving code so the player cannot move.
	 */
	void Update () {
		if(Input.GetKeyUp (KeyCode.E) && isPlayerVisible){
			playerCamera.gameObject.GetComponent<MouseLook>().enabled = false;
			playerObj.GetComponent<MouseLook>().enabled = false;
			playerObj.SendMessage("enableYourself",false);
			isKeyPressed = true;
		}
	}
	/*
	 * This is what handels the GUI.
	 * 
	 * The if statment draws a box with buttons in it if the player is visible
	 * Each one loads a different scene that handles how the tractor is driven.
	 * The cancel button renables movement and makes iskeyPressed false.
	 * 
	 */
	void OnGUI(){
		if(isKeyPressed){
			GUI.Box(new Rect(0,100,600,100),"What do you want to do?");
			if(GUI.Button(new Rect(0,125,150,75), "AI tractor")){
				Application.LoadLevel("AI Tractor");
			}
			if(GUI.Button(new Rect(150,125,150,75), "First Person Tractor")){
				Application.LoadLevel("FPTractorEnvierment");
			}
			if(GUI.Button(new Rect(300,125,150,75), "3rd perosn Tractor")){
				Application.LoadLevel("TractorController");
			}
			if(GUI.Button(new Rect(450,125,150,75), "Cancel")){
				playerCamera.gameObject.GetComponent<MouseLook>().enabled = true;
				playerObj.GetComponent<MouseLook>().enabled = true;
				playerObj.SendMessage("enableYourself",true);
				isKeyPressed = false;
			}
		}
	}
	
	/*
	 * a on trigger even to see if the door has been entered by the player.
	 */
	void OnTriggerEnter(Collider Player){
		isPlayerVisible = true;
	}
	/*
	 * a trigger to see if the player has exited the cube.
	 */
	void OnTriggerExit(Collider Player){
		isPlayerVisible = false;
	}
}
