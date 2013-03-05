using UnityEngine;
using System.Collections;
using UnityEditor;

public class Object2Terrain : EditorWindow {

	[MenuItem ("Terrain/Object to Terrain", true)] static bool Validate () {
		try { return Selection.activeTransform.GetComponent<MeshFilter>().sharedMesh && Terrain.activeTerrain;}
		catch { return false;}
		
	}
	
	[MenuItem ("Terrain/Object to Terrain", false, 2000)] static void OpenWindow() {
		EditorWindow.GetWindow<Object2Terrain>(true);	
	}
	
	float sizeAdjustment;	//custom-tailor edge behavior
	
	void OnGUI () {
		GUI.SetNextControlName("Size Adjustment");
		sizeAdjustment = EditorGUILayout.FloatField("Size Adjustment", sizeAdjustment);
		GUI.FocusControl("Size Adjustment");
		if (Event.current.type == EventType.KeyUp &&
			(Event.current.keyCode == KeyCode.Return) || Event.current.keyCode == KeyCode.KeypadEnter)
		{
			this.Close();
			CreateTerrain();
		}
	}
	
	void OnLostFocus () { this.Close();}
	
	delegate void CleanUp ();
	
	void CreateTerrain() {
		TerrainData terrain = Terrain.activeTerrain.terrainData;
		Undo.RegisterUndo(terrain, "Object to Terrain");
		
		MeshCollider collider = Selection.activeGameObject.GetComponent<MeshCollider>();
		CleanUp cleanUp = null;
		if (!collider) {
			collider = Selection.activeGameObject.AddComponent<MeshCollider>();
			cleanUp = () => DestroyImmediate(collider);
		}
		
		Bounds bounds = collider.bounds;
		bounds.Expand(new Vector3(-sizeAdjustment * bounds.size.x, 0, -sizeAdjustment * bounds.size.z));
		
		//raycasting samples over the object to see what terrain heights should be
		float[,] heights = new float[terrain.heightmapWidth, terrain.heightmapHeight];
		Ray ray = new Ray(new Vector3(bounds.min.x, bounds.max.y * 2, bounds.min.z), -Vector3.up);
		RaycastHit hit = new RaycastHit();
		float meshHeightInverse = 1 / bounds.size.y;
		Vector3 rayOrigin = ray.origin;
		Vector2 stepXZ = new Vector2(bounds.size.x / heights.GetLength(1), bounds.size.z / heights.GetLength(0));
		for (int zCount = 0; zCount < heights.GetLength(0); zCount++){
			for (int xCount = 0; xCount < heights.GetLength(1); xCount++){
				heights[zCount, xCount] = collider.Raycast(ray, out hit, bounds.size.y * 2) ?
					1 - (bounds.max.y - hit.point.y) * meshHeightInverse : 0;
				rayOrigin.x += stepXZ[0];
				ray.origin = rayOrigin;
			}
			rayOrigin.z += stepXZ[1];
			rayOrigin.x = bounds.min.x;
			ray.origin = rayOrigin;
		}
		terrain.SetHeights(0, 0, heights);
		
		if (cleanUp != null) cleanUp ();
	}
}
