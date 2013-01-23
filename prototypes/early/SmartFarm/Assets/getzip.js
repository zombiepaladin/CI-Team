#pragma strict
public var zip = "";


function OnGUI(){
	//zip="";
	GUI.Label(Rect((Screen.width/2)-100, (Screen.height/2)-30, 100, 30), "Enter zip");
	//while(textField.Length!=6)
		zip= GUI.TextField(Rect((Screen.width/2)+20, (Screen.height/2)-30, 80, 30), zip);
		
	if(GUI.Button(Rect((Screen.width/2)-20, (Screen.height/2)+20, 100, 30), "Submit")){
		PlayerPrefs.SetString("zipcode", zip);
		Application.LoadLevel("coresim");
	}
	
}
function Update () {

}
