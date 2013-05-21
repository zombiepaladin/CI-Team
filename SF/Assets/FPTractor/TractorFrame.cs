using UnityEngine;
using System.Collections;

public class TractorFrame : MonoBehaviour {

	public GameObject sp;
	public GameObject frame;
	private GameObject	instancedFrame;
	void Start () {
		frame.GetComponent<Transform>().position = new Vector3(sp.GetComponent<Transform>().position.x,sp.GetComponent<Transform>().position.y+.8f,sp.GetComponent<Transform>().position.z);
		frame.GetComponent<Transform>().localScale = new Vector3(2.2f,2.2f,2.2f);
		//instancedFrame = (GameObject)Instantiate(frame,new Vector3(sp.GetComponent<Transform>().position.x,sp.GetComponent<Transform>().position.y,sp.GetComponent<Transform>().position.z),new Quaternion(280f,0,0,0));
		//instancedFrame.GetComponent<Transform>().rotation = new Quaternion(280f,0,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		if((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A))&& frame.GetComponent<Transform>().rotation.z < 90){
			//instancedFrame.GetComponent<Transform>().rotation = new Quaternion(instancedFrame.GetComponent<Transform>().rotation.x+(.001f),instancedFrame.GetComponent<Transform>().rotation.y,instancedFrame.GetComponent<Transform>().rotation.z,instancedFrame.GetComponent<Transform>().rotation.w);
			//frame.GetComponent<Transform>().rotation = (new Quaternion(frame.GetComponent<Transform>().rotation.x,frame.GetComponent<Transform>().rotation.y,frame.GetComponent<Transform>().rotation.z,frame.GetComponent<Transform>().rotation.w))*(new Quaternion(frame.GetComponent<Transform>().rotation.x,frame.GetComponent<Transform>().rotation.y,frame.GetComponent<Transform>().rotation.z+1f,frame.GetComponent<Transform>().rotation.w));
		}
		else if((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))&& frame.GetComponent<Transform>().rotation.z >= 0){
			//instancedFrame.GetComponent<Transform>().rotation = new Quaternion(instancedFrame.GetComponent<Transform>().rotation.x-(.001f),instancedFrame.GetComponent<Transform>().rotation.y,instancedFrame.GetComponent<Transform>().rotation.z,instancedFrame.GetComponent<Transform>().rotation.w);
			//frame.GetComponent<Transform>().rotation = (new Quaternion(frame.GetComponent<Transform>().rotation.x,frame.GetComponent<Transform>().rotation.y,frame.GetComponent<Transform>().rotation.z,frame.GetComponent<Transform>().rotation.w))*(new Quaternion(frame.GetComponent<Transform>().rotation.x,frame.GetComponent<Transform>().rotation.y,frame.GetComponent<Transform>().rotation.z-1f,frame.GetComponent<Transform>().rotation.w));
		}
		//instancedFrame.GetComponent<Transform>().position = new Vector3(sp.GetComponent<Transform>().position.x,sp.GetComponent<Transform>().position.y,sp.GetComponent<Transform>().position.z);
		frame.GetComponent<Transform>().position = new Vector3(sp.GetComponent<Transform>().position.x,sp.GetComponent<Transform>().position.y+.8f,sp.GetComponent<Transform>().position.z);
	}
}
