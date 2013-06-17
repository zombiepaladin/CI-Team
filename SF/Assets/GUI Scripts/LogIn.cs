using UnityEngine;
using System.Collections;

public class LogIn : MonoBehaviour {
	
	string username = "";
	string password = "";
	string email = "";
	string cPassword = "";
	string formText = "";
	
	
	string URL = "";
	string hash = "";
	
	bool newUser = false;
	bool displayed1 = false;
	bool displayed2 = false;
	void OnGUI(){
		if(!newUser){
			GUI.Label(new Rect(10,10,80,20),"Username:");
			GUI.Label(new Rect(10,30,80,20),"Password:");
	
			username = GUI.TextField(new Rect(90,10,100,20),username);
			password = GUI.PasswordField(new Rect(90,30,100,20),password,'*');
		
			if(GUI.Button(new Rect(10,60,100,20),"Login")){
			
			}
			if(GUI.Button(new Rect(110,60,100,20),"Register")){
				newUser = true;
			}
			GUI.TextArea(new Rect(10,150,500,500),formText);
		}
		else{

			
			GUI.Label(new Rect(10,10,80,20),"Username:");
			GUI.Label(new Rect(10,30,80,20),"Password:");
			GUI.Label(new Rect(10,50,80,20),"Confirm password");
			GUI.Label(new Rect(10,70,80,20),"E-mail:");
			
			username = GUI.TextField(new Rect(90,10,100,20),username);
			password = GUI.PasswordField(new Rect(90,30,100,20),password, '*');
			cPassword = GUI.PasswordField(new Rect(90,50,100,20),cPassword, '*');
			email = GUI.TextField(new Rect(90,70,100,20),email);
			
			
			if(GUI.Button(new Rect(10,100,100,20),"Submit")){
				bool sucess = AddUser();
				if(sucess){
					newUser = false;
				}
			}
			if(GUI.Button(new Rect(110,100,100,20),"Cancel")){
				newUser = false;
			}
			GUI.TextArea(new Rect(10,150,500,500),formText);
		}
	}
	void Login(){
//		WWWForm form = new WWWForm();
//		form.AddField("hash",hash);
//		form.AddField("un",username);
//		form.AddField("password",password);
//		WWW w = new WWW(URL,form);
//		if(w.error != null){
//			Debug.Log(w.error);
//		}
//		else{
//			
//		}
	}
	
	bool AddUser(){
		bool addU = true;
		if(password.Length <= 6){
			formText += "Passwords need to have a length of 6!\n";
			addU = false;
		}
		
		if(cPassword != password){
			formText += "Passwords do not match!\n";
			addU = false;
		}
		
		if(addU){
			//add user after seeing if it is in the data base first. 
			return addU;
		}
		else{
			return addU;
		}
	}
}
