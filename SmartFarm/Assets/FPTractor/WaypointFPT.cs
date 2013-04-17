using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WaypointFPT : MonoBehaviour {
	List<Transform> points = new List<Transform>();
	
	GameObject terrain;
	
	public WaypointFPT(GameObject te){
		terrain = te;
	}
	
	public void genPointsStr(Transform cp, bool isPos){
		if(points.Count > 0){
			points.Clear ();
		}
		points.Add(cp);
		int k = (int)cp.position.z;
		int i = 1;
		if(isPos){
			while(k <= terrain.GetComponent<Terrain>().terrainData.size.z-50){
				GameObject t = new GameObject();
				t.GetComponent<Transform>().position = new Vector3(cp.position.x,0,300*i);
				points.Add(t.GetComponent<Transform>());
				i++;
				k += 300;
			}
		}
		else if(!isPos){
			while(k >= 50){
				GameObject t = new GameObject();
				t.GetComponent<Transform>().position = new Vector3(cp.position.x,0,300*i);
				points.Add(t.GetComponent<Transform>());
				i++;
				k += 300;
			}
		}
	}
	
	public void genPointsTurn(Transform cp){
		if(points.Count > 0){
			points.Clear ();
		}
		points.Add(cp);
		int k = 0;
		int i = 1;
		while(k <= 50){
			GameObject t = new GameObject();
			if(k < 25){
				t.GetComponent<Transform>().position = new Vector3(cp.position.x+(5*i),0,cp.position.z+(2*i));
			}
			else if(k == 25){
				t.GetComponent<Transform>().position = new Vector3(cp.position.x+(5*i),0,cp.position.z+(2*i));
			}
			else{
				t.GetComponent<Transform>().position = new Vector3(cp.position.x+(5*i),0,cp.position.z+(2*i));
			}
			points.Add(t.GetComponent<Transform>());
			k+= 5;
			i++;
		}	
	}
	public string checkPath(Transform cp){
		if(cp.position.z > points[0].position.z){
			return "Right";
		}
		else if(cp.position.z < points[0].position.z){
			return "Left";
		}
		else{
			return "Streight";
		}
	}
	
	public float checkHowFarOff(Transform cp, bool pos){
		if(pos){
			return points[0].position.z - cp.position.z;
		}
		else if(!pos){
			return cp.position.z - points[0].position.z;
		}
		else{
			return 0;
		}
	}
	
	public void removePastPoint(Transform cp){
		if(cp.position == points[0].position && points.Count-1 == 0){
			points.RemoveAt(0);
		}
	}
	
	public bool endOfPath(Transform cp){
		if(cp.position == points[0].position && points.Count == 1){
			return true;
		}
		else{
			return false;
		}
	}
}
