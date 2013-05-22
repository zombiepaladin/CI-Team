using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WaypointFPT{
	public List<Vector3> points = new List<Vector3>();
	
	GameObject terrain;
	//creates the object
	public WaypointFPT(GameObject te){
		terrain = te;
	}
	//generates streight waypoints
	public void genPointsStr(Vector3 cp, bool isPos){
		if(points.Count > 0){
			points.Clear ();
		}
		points.Add(cp);
		float k = cp.z;
		float i = 1.0f;
		if(isPos){
			while(k <= terrain.GetComponent<Terrain>().terrainData.size.z-50.0f){
				//GameObject t = new GameObject();
				//t.GetComponent<Transform>().position = new Vector3(cp.position.x,0,300*i);
				points.Add(new Vector3(cp.x,0,50.0f*i));
				i++;
				k += 50;
			}
		}
		else if(!isPos){
			while(k > 50){
				//GameObject t = new GameObject();
				//t.GetComponent<Transform>().position = new Vector3(cp.position.x,0,300*i);
				points.Add(new Vector3(cp.x,0,cp.z-(50.0f*i)));
				i++;
				k -= 50;
			}
		}
	}
	//generates turning waypoints
	public void genPointsTurn(Vector3 cp){
		if(points.Count > 0){
			points.Clear ();
		}
		points.Add(cp);
		int k = 0;
		int i = 1;
		int z = 1;
		while(k <= 45){
			//GameObject t = new GameObject();
			if(2*i < 10){
				points.Add(new Vector3(cp.x+(5*i),0,cp.z+(2*i)));
				//t.GetComponent<m>().position = new Vector3(cp.position.x+(5*i),0,cp.position.z+(2*i));
			}
			else if(2*i == 10){
				points.Add(new Vector3(cp.x+(5*i),0,cp.z+10));
				//t.GetComponent<Transform>().position = new Vector3(cp.position.x+(5*i),0,cp.position.z+(2*i));
			}
			else if(2*i > 10){
				points.Add(new Vector3(cp.x+(5*i),0,cp.z+(10-(z*2))));
				z++;
				//t.GetComponent<Transform>().position = new Vector3(cp.position.x+(5*i),0,cp.position.z+(2*i));
			}
			
			k += 5;
			i++;
		}	
	}
	public string checkPath(Vector3 cp){
		if(cp.z > points[0].z){
			return "Right";
		}
		else if(cp.z < points[0].z){
			return "Left";
		}
		else{
			return "Streight";
		}
	}
	
	public float checkHowFarOff(Vector3 cp, bool pos){
		if(pos){
			return points[0].z - cp.z;
		}
		else if(!pos){
			return cp.z - points[0].z;
		}
		else{
			return 0;
		}
	}
	
	public bool endOfPath(Vector3 cp){
		if(cp == points[points.Count-1]){
			return true;
		}
		else{
			return false;
		}
	}
}
