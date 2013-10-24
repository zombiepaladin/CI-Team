using UnityEngine;
using System.Collections;

public class TractorFrame : MonoBehaviour {

	public GameObject tractor;
	public GameObject frame;
	private GameObject	instancedFrame;
	void Start () {
		instancedFrame = (GameObject)Instantiate(frame,new Vector3(tractor.GetComponent<Transform>().position.x,tractor.GetComponent<Transform>().position.y+.15f,tractor.GetComponent<Transform>().position.z+.25f),new Quaternion(0,0,0,0));
		instancedFrame.GetComponent<Transform>().localScale = new Vector3(2.2f,2.2f,2.2f);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)){
			instancedFrame.GetComponent<Transform>().rotation = new Quaternion(instancedFrame.GetComponent<Transform>().rotation.x,instancedFrame.GetComponent<Transform>().rotation.y-(.01f),instancedFrame.GetComponent<Transform>().rotation.z,instancedFrame.GetComponent<Transform>().rotation.w);
			
		}
	
		if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
			instancedFrame.GetComponent<Transform>().rotation = new Quaternion(instancedFrame.GetComponent<Transform>().rotation.x,instancedFrame.GetComponent<Transform>().rotation.y+(.01f),instancedFrame.GetComponent<Transform>().rotation.z,instancedFrame.GetComponent<Transform>().rotation.w);
			
		}
		instancedFrame.GetComponent<Transform>().position = new Vector3(tractor.GetComponent<Transform>().position.x,tractor.GetComponent<Transform>().position.y+.15f,tractor.GetComponent<Transform>().position.z+.25f);
	}
}
