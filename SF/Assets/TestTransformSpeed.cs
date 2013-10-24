using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class TestTransformSpeed : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		var watch = new Stopwatch();
		watch.Start();
		for(var i = 0; i < 10000; i ++)
		{
			var t = transform.position;
		}
		watch.Stop();
		UnityEngine.Debug.Log(watch.ElapsedTicks/10000000f);
		watch.Reset();
		watch.Start();
		for(var i = 0; i < 10000; i ++)
		{
			var t = transform.localPosition;
		}
		watch.Stop();
		UnityEngine.Debug.Log(watch.ElapsedTicks/10000000f);
		
		
	
	}
	
	
}
