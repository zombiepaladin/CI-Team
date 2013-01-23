var menuSkin : GUISkin;
var menuBackground : Texture2D;

//Declare button images
var playButtonStandard : Texture2D;

function onStart() {
Screen.SetResolution (1024, 768, false);
}

function MainMenu() {
    
    //background picture
    GUI.Label(Rect(0, 0, 1024, 1024), menuBackground);
    
     //layout start
    GUI.BeginGroup(Rect(0, 0, 1024, 768));
    
    ///////main menu buttons
    //start button
    if(GUI.Button(Rect(512 - 90, 250, 209, 59), "Play")) {
    	Application.LoadLevel("coresim");
    }
    
    //advanced start button
    if(GUI.Button(Rect(512 - 90, 325, 209, 59), "Advanced Start")) {
    	Application.LoadLevel(1);
    }
    
    //languages button
    if(GUI.Button(Rect(512 - 90, 400, 209, 59), "Languages")) {
    	//Add functionality here
    }
    
    //options button
    if(GUI.Button(Rect(512 - 90, 475, 209, 59), "Options")) {
    	//Add functionality here
    }
    
    //exit button
    if(GUI.Button(Rect(512 - 90, 550, 209, 59), "Exit")) {
    	Application.Quit();
    }
    
    //layout end
    GUI.EndGroup(); 
}

function OnGUI () {
    //load GUI skin
    GUI.skin = menuSkin;
    
    //execute theFirstMenu function
    MainMenu();
}