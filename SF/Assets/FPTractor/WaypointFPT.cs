using UnityEngine;
using System.Collections.Generic;
using System.Collections;


/*
 * this is a Waypoint genteration class for smart farm 
 * 
 */
public class WaypointFPT{
	/// <summary>
	/// A list of waypoints stored as vector3s 
	/// </summary>
	public List<Vector3> points = new List<Vector3>();
	
	/// <summary>
	/// The terrain.
	/// </summary>
	GameObject terrain;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="WaypointFPT"/> class.
	/// </summary>
	/// <param name='te'>
	/// the passed in terrain.
	/// </param>
	public WaypointFPT(GameObject te){
		terrain = te;
	}
	
	/// <summary>
	/// Generates the points for a streight line
	/// </summary>
	/// <param name='cp'>
	/// Current point of vehicle.
	/// </param>
	/// <param name='isPos'>
	/// this sees if it is positive or not.
	/// </param>
	public void genPointsStr(Vector3 cp, bool isPos){
		if(points.Count > 0){
			points.Clear ();
		}
		points.Add(cp);
		float k = cp.z;
		float i = 1.0f;
		if(isPos){
			while(k <= terrain.GetComponent<Terrain>().terrainData.size.z-50.0f){
				points.Add(new Vector3(cp.x,0,50.0f*i));
				i++;
				k += 50;
			}
		}
		else if(!isPos){
			while(k > 50){
				points.Add(new Vector3(cp.x,0,cp.z-(50.0f*i)));
				i++;
				k -= 50;
			}
		}
	}
	
	/// <summary>
	/// Generates the points turn.
	/// </summary>
	/// <param name='cp'>
	/// Current point of vehicle.
	/// </param>
	public void genPointsTurn(Vector3 cp){
		if(points.Count > 0){
			points.Clear ();
		}
		points.Add(cp);
		int k = 0;
		int i = 1;
		int z = 1;
		while(k <= 45){
			if(2*i < 10){
				points.Add(new Vector3(cp.x+(5*i),0,cp.z+(2*i)));
			}
			else if(2*i == 10){
				points.Add(new Vector3(cp.x+(5*i),0,cp.z+10));
			}
			else if(2*i > 10){
				points.Add(new Vector3(cp.x+(5*i),0,cp.z+(10-(z*2))));
				z++;
			}
			
			k += 5;
			i++;
		}	
	}
	
	/// <summary>
	/// Checks the path.
	/// </summary>
	/// <returns>
	/// It returns a string to indicate how far off you are.
	/// </returns>
	/// <param name='cp'>
	/// Current point of vehicle.
	/// </param>
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
	
	/// <summary>
	/// Checks the how far off from a given point.
	/// </summary>
	/// <returns>
	/// The how far off.
	/// </returns>
	/// <param name='cp'>
	/// Current point of vehicle.
	/// </param>
	/// <param name='pos'>
	/// positive or not.
	/// </param>
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
	
	/// <summary>
	/// Checks to see if it is the end of the path
	/// </summary>
	/// <returns>
	/// if it is the end or not
	/// </returns>
	/// <param name='cp'>
	/// Current point of vehicle.
	/// </param>
	public bool endOfPath(Vector3 cp){
		if(cp == points[points.Count-1]){
			return true;
		}
		else{
			return false;
		}
	}
}
