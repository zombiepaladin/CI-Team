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
		/*
		 * this if showes the login screen.
		 * 
		 */
		if(!newUser){
			GUI.Label(new Rect((Screen.width/2) - 90,10,80,20),"Username:");
			GUI.Label(new Rect((Screen.width/2) - 90,30,80,20),"Password:");
	
			username = GUI.TextField(new Rect((Screen.width/2) - 10,10,100,20),username);
			password = GUI.PasswordField(new Rect((Screen.width/2) - 10,30,100,20),password,'*');
		
			if(GUI.Button(new Rect((Screen.width/2) - 90,60,100,20),"Login")){
				bool sucess = LoginWeb();
				if(sucess){
					Application.LoadLevel("Test_Main_Menu");
				}
				else{
					formText += "Login Unsucessfull!\n";
				}
			}
			if(GUI.Button(new Rect((Screen.width/2)+10,60,100,20),"Register")){
				newUser = true;
			}
			GUI.TextArea(new Rect((Screen.width/2) - 125,150,300,300),formText);
		}
		/*
		 * this shows the create new user screen.
		 * 
		 */
		else{

			
			GUI.Label(new Rect((Screen.width/2) - 90,10,80,20),"Username:");
			GUI.Label(new Rect((Screen.width/2) - 90,30,80,20),"Password:");
			GUI.Label(new Rect((Screen.width/2) - 90,50,80,20),"Confirm password");
			GUI.Label(new Rect((Screen.width/2) - 90,70,80,20),"E-mail:");
			
			username = GUI.TextField(new Rect((Screen.width/2) - 10,10,100,20),username);
			password = GUI.PasswordField(new Rect((Screen.width/2) - 10,30,100,20),password, '*');
			cPassword = GUI.PasswordField(new Rect((Screen.width/2) - 10,50,100,20),cPassword, '*');
			email = GUI.TextField(new Rect((Screen.width/2) - 10,70,100,20),email);
			
			
			if(GUI.Button(new Rect((Screen.width/2) - 90,100,100,20),"Submit")){
				bool sucess = AddUser();
				if(sucess){
					newUser = false;
				}
			}
			if(GUI.Button(new Rect((Screen.width/2) + 10,100,100,20),"Cancel")){
				newUser = false;
			}
			GUI.TextArea(new Rect((Screen.width/2) - 125,150,300,300),formText);
		}
	}
	
	/// <summary>
	/// Logins the web.
	/// </summary>
	/// <returns>
	/// if sucessfull.
	/// </returns>
	bool LoginWeb(){
		
		return true;
	}
	
	/// <summary>
	/// Adds the user.
	/// </summary>
	/// <returns>
	/// if Sucessfull.
	/// </returns>
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
			//add user after seeing if it is not in the database first. 
			return addU;
		}
		else{
			return addU;
		}
	}
}
