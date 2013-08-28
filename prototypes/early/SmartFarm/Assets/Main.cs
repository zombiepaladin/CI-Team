using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Patches : MonoBehaviour
{
    Dictionary<int, GameObject> instances = new Dictionary<int, GameObject>();   

    Object[] cornMods = Resources.LoadAll("cornFabs", typeof(GameObject));
    
    // int dicLength = 0;
    Rect bounds; 
    int age;
	int sx;
	int sy;
	
	int sizex = 10;
	int sizey = 10;
	
	int res = 2;
    bool dead = false;

    public Patches(){
        age = 0;
		sx = 0;
		sy = 0;
		sizex = 1;
		sizey = 1;
        bounds = new Rect(sx, sy, sizex, sizey);
        dead = false;

        //for(int cnt=0; cnt < cornMods.Length; cnt++){
        //    Debug.Log("corn model #" + cnt + " is: " + cornMods[cnt].name);
        //}
        //int spawn = 4;
        //GameObject mod = (GameObject)Instantiate(cornMods[spawn], new Vector3(4, 0, 4), Quaternion.identity);
    }

    public int Age{
        get { return age; }
        protected set { age = value; }
    }

    public Patches(Vector2 origin, Vector2 scale){
        age = 0;
        bounds = new Rect(origin.x, origin.y, scale.x, scale.y);
        dead = false;
    }

    public Patches(Rect span){
        age = 0;
        bounds = span;
        dead = false;
    }
	
	public Patches(int x, int y){
        age = 0;  
		sx = x;
		sy = y;
        bounds = new Rect(x, y, sizex, sizey);
        dead = false;
    }
	
	public Patches(int x, int y, int szx, int szy){
        age = 0;
		sx = x;
		sy = y;
		sizex = szx;
		sizey = szy;
        bounds = new Rect(x, y, sizex, sizey);
        dead = false;
    }
	
	public void RemPatch(){
		foreach(GameObject inst in instances.Values){
             Destroy(inst);
             //instances = (GameObject)Instantiate(cornMods[age + 1], inst.rigidbody.position, Quaternion.identity);
        }
		instances = new Dictionary<int, GameObject>();
	}
	
    public bool agePatch(){
		dead = false;
        int len = instances.Count; 
        RemPatch();
        if((age < 10)){
            age++;
			instances = new Dictionary<int, GameObject>();
        	allocPatch();
        }
        else{
            dead = true;
        	age = 0;
			instances = new Dictionary<int, GameObject>();
        	allocPatch();
        }
        return dead;
    }
	
	public bool DeagePatch(){
		dead = false;
        int len = instances.Count; 
        RemPatch();
        if((age > 0)){
            age--;
			instances = new Dictionary<int, GameObject>();
        	allocPatch();
        }
        else{
            age = 10;
			instances = new Dictionary<int, GameObject>();
        	allocPatch();
        }
        return dead;
    }

    public void Die(){
        dead = true;
        age = 0;
    }
	
    public void allocPatch(){
        for (int i = sx; i < sx + bounds.width; i=i+res)
        {
            for (int j = sy; j < sy + bounds.height; j=j+res)
            {
                addCornInstance(i, 0, j, age);
            }
        }
    }

    public void allocPatch(int x, int y)
    {
		sx = x;
		sy = y;
		bounds = new Rect(x, y, sizex, sizey);
         for (int i = sx; i < sx + bounds.width; i++)
        {
            for (int j = sy; j < sy + bounds.height; j++)
            {
                addCornInstance(i, 0, j, age);
            }
        }
    }

//    public void ReAllocPatch(Rect pBound){
//        bounds = pBound;
//        for (int i = sx; i < sx + pBound.width; i=i+res)
//        {
//            for (int j = sy; j < sy + pBound.height; j=j+res)
//            {
//                addCornInstance(i, 0,j, age);
//            }
//        }
//    }

    //public void allocPatch(Rect pBound)
    //{
    //    bounds = pBound;
    //    for (int i = 0; i < pBound.width; i++)
    //    {
    //        for (int j = 0; j < pBound.height; j++)
    //        {
    //            addCornInstance(i, 0, j, age);
    //        }
    //    }
    //}

    public int addCornInstance(){
		Debug.Log ("H1");
        int x, y, z;
        x = 0; y = 0; z = 0;

        GameObject newCorn = (GameObject)Instantiate(cornMods[age], new Vector3(x, y, z), Quaternion.identity);
        instances.Add(instances.Keys.Count, newCorn);

        return instances.Keys.Count - 1;
    }

    public int addCornInstance(int x, int y, int z){
		Debug.Log ("H2");
        GameObject newCorn = (GameObject)Instantiate(cornMods[age], new Vector3(x, y, z), Quaternion.identity);
        instances.Add(instances.Keys.Count, newCorn);

        return instances.Keys.Count-1;
    }

    public int addCornInstance(int x, int y, int z, int ag){
		Debug.Log ("H3");
        GameObject newCorn = (GameObject)Instantiate(cornMods[ag], new Vector3(x, y, z), Quaternion.identity);
        instances.Add(instances.Keys.Count, newCorn);

        return instances.Keys.Count - 1;
    }

    public GameObject getCornInstance(int corn)
    {

        if (instances.ContainsKey(corn))
        {
            return instances[corn];
        }

        throw new MissingComponentException();
    }
}

//public class DataRet : MonoBehaviour
//{
//    public TextAsset GameAsset;
//    public string dupe = "";
//    string[] lines = new string[1];
//    string outDir = @"C:\Users\blackwolf\Desktop\wheat1.out";
//    string inFile = @"C:\Users\blackwolf\Desktop\testWheatChgEnv.apsim";
//    string ld = "";
//    public List<string[]> ls = new List<string[]>();
//
//    string[] variables = new string[6];
//    string[] units = new string[6];
//
//    public void retrieve()
//    {
//        inFile = GUI.TextField(new Rect(40, 10, 400, 20), inFile, 100);
//        outDir = GUI.TextField(new Rect(40, 30, 400, 20), outDir, 100);
//
//        GUI.Label(new Rect(50, 70, 80, 20), ld);
//
//        if (GUI.Button(new Rect(40, 50, 80, 20), "Run"))
//        {
//
//            ld = "Loading...";
//
//            System.Diagnostics.Process p = new System.Diagnostics.Process();
//            p.StartInfo.Arguments = inFile;
//            p.StartInfo.WorkingDirectory = @"C:\Program Files\Apsim73-r1387";
//            p.StartInfo.FileName = @"C:\Program Files\Apsim73-r1387\Apsim73-r1387\Model\ApsimRun.exe";
//            //p.StartInfo.RedirectStandardOutput = true;
//            p.StartInfo.CreateNoWindow = true;
//            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
//            p.Start();
//
//            System.Threading.Thread.Sleep(5000);
//
//            lines = System.IO.File.ReadAllLines(outDir);
//            int rct = 0;
//            foreach (string line in lines)
//            {
//                if (rct > 1)
//                {
//                    if (rct < 4)
//                    {
//                        if (rct == 3)
//                        {
//                            string[] ad = new string[6];
//                            string[] sp = line.Split(' ');
//                            for (int y = 0; y < 6; y++)
//                            {
//                                units[y] = sp[y];
//                            }
//                        }
//                        else
//                        {
//                            string[] ad = new string[6];
//                            string[] sp = line.Split(' ');
//                            for (int y = 0; y < 6; y++)
//                            {
//                                variables[y] = sp[y];
//                            }
//                        }
//                    }
//                    else
//                    {
//                        dupe += line + "\n";
//                    }
//                }
//                rct++;
//            }
//
//            string[] sp1 = dupe.Split('\n');
//            foreach (string word in sp1)
//            {
//                string[] ot = new string[6];
//                string[] sp2 = word.Split(' ');
//                for (int y = 0; y < 6; y++)
//                {
//                    ot[y] = sp2[y];
//                }
//                ls.Add(ot);
//            }
//            ld = "Done...";
//        }
//        GUI.Box(new Rect(40, 100, 100, 30), dupe);
//    }
//}

public class Main : MonoBehaviour {

	// Use this for initialization
    Time timee;
	Vector3 ps;
    // corn models arranged by stages
    public Object[] cornMods;
    List<Patches> pinstance = new List<Patches>();
    GameObject tractor;
    string c;
	int count = 0;
    private float startTime;
    string textTime;
    //Patches patch;

    //DataRet ret;

	void Start () {
        startTime = Time.time;
        //ret = new DataRet();
        //MeshRenderer corn = new MeshRenderer();
        //patch = new Patches();
        //patch.addCornInstance(4, 0, 4);
        //for(int i = 0; i<300; i++){
        //    patch.addCornInstance(4, 0, i);
        //}
        //patch.ReAllocPatch(new Rect(0, 0, 20, 10));
        //ret.retrieve();
        //OnGUI();
    }

	// Update is called once per frame
	void Update () {
        int guiTime = (int)(Time.time - startTime);

       
 		int minutes = guiTime / 60;
        int seconds = guiTime % 60;
        int fraction = (guiTime * 100) % 100;
        int passTime = 10; // in seconds

        //if (seconds % passTime == 0 && seconds != 0){
        //    c = "ON";
        //    patch.agePatch();
        //}
        // bool patchState = false;
        //if (seconds % passTime == 0){
        //    if (minutes == 0){
        //        c = "ON";
        //        patchState = patch.agePatch();
        //    }
        //    else if(minutes == 1 && seconds == 1){
        //        c = "ON";
        //        patch.agePatch();
        //    }
        //}
        //else if (seconds % passTime != 0){
        //    if (minutes != 0)
        //    {
        //        c = "ON";
        //        patch.agePatch();
        //    }
        //    //else if(minutes == 1 && seconds == 1){
        //    //    c = "ON";
        //    //    patch.agePatch();
        //    //}
        //}
        //else c = "OFF";
		
		ps = GameObject.Find("mcam").transform.position;		
		
		if (Input.GetKeyDown("r")) {
			foreach(Patches patchd in pinstance){
				patchd.RemPatch();
			}
			pinstance = new List<Patches>();
		}
		
		if (Input.GetKeyDown("e")) {
			Patches patch = new Patches((int)ps.x, (int)ps.z, 1, 1);
			pinstance.Add(patch);
			count++;
		}
		
		if (Input.GetKeyDown("f")) {
			Patches patch = new Patches((int)ps.x, (int)ps.z, 10, 10);
			pinstance.Add(patch);
			count++;
		}
		
		if (Input.GetKeyDown("c")){
			foreach(Patches patchd in pinstance){
				patchd.agePatch();
			}
		}
		if (Input.GetKeyDown("x")){
			foreach(Patches patchd in pinstance){
				patchd.DeagePatch();
			}
		}
	}

    void OnGUI()
    {
        int guiTime = (int)(Time.time - startTime);
		 
        int minutes = guiTime / 60;
        int seconds = guiTime % 60;
        int fraction = (guiTime * 100) % 100;
		
		string controls = "e : plant(1) -- f : plant(10) -- c : age forward -- x : age back -- r : remove patches";
		GUI.Label(new Rect(20, 0, 550, 50), controls);
        string text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
        GUI.Label(new Rect(20, 20, 100, 50), "GameTime " + text);

        //GUI.Label(new Rect(50, 130, 100, 30), "stat : " + c);
		string ages = "";
		
		int count = 0;
		foreach(Patches patchd in pinstance){
			count++;
			ages += "st" + count.ToString() + ":" + patchd.Age.ToString() + "  ";
		}
		
        GUI.Label(new Rect(230, 20, 300, 60), "stages : \n" + (ages));
		GUI.Label(new Rect(140, 20, 80, 60), "pos : \nX " + ps.x + " \nY " + ps.z);

        //GUI.Label(new Rect(50, 50, 100, 100), Terrain.activeTerrain.terrainData.heightmapWidth.ToString() 
        //     + "X" + Terrain.activeTerrain.terrainData.heightmapHeight.ToString());
		
        //GUI.Label(new Rect(50, 100, 100, 20), "running time " + Time.timeSinceLevelLoad.ToString());
    }
}
