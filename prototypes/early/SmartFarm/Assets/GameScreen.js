// Declare month button textures
var gameBackground : Texture2D;
var januaryButton : Texture2D;
var februaryButton : Texture2D;
var marchButton : Texture2D;
var aprilButton : Texture2D;
var mayButton : Texture2D;
var juneButton : Texture2D;
var julyButton : Texture2D;
var augustButton : Texture2D;
var septemberButton : Texture2D;
var octoberButton : Texture2D;
var novemberButton : Texture2D;
var decemberButton : Texture2D;

// Declre weather button textures
var cloudyButtonNormal : Texture2D;
var cloudyButtonHover : Texture2D;
var cloudyButtonClick : Texture2D;
var partlycloudyButtonNormal : Texture2D;
var partlycloudyButtonHover : Texture2D;
var partlycloudyButtonClick : Texture2D;
var rainyButtonNormal : Texture2D;
var rainyButtonHover : Texture2D;
var rainyButtonClick : Texture2D;
var sunnyButtonNormal : Texture2D;
var sunnyButtonHover : Texture2D;
var sunnyButtonClick : Texture2D;
var snowButtonNormal : Texture2D;
var snowButtonHover : Texture2D;
var snowButtonClick : Texture2D;

// Declare feature button textures
var homeButtonNormal : Texture2D;
var homeButtonHover : Texture2D;
var homeButtonClick : Texture2D;
var leaveButtonNormal : Texture2D;
var leaveButtonHover : Texture2D;
var leaveButtonClick : Texture2D;
var helpButtonNormal : Texture2D;
var helpButtonHover : Texture2D;
var helpButtonClick : Texture2D;
var statsButtonNormal : Texture2D;
var statsButtonHover : Texture2D;
var statsButtonClick : Texture2D;
var backButton : Texture2D;
var forwardButton : Texture2D;

// Declare rectangles
var backRect = 		Rect (340, 765, 80, 80);
var forwardRect = 	Rect (630, 765, 80, 80);

var weatherRect = 		Rect (835, 104, 128, 128);

var month = 1;
var weather = 4;

function GameMenu() {
	GUI.Label(Rect(0, 0, 1024, 1024), gameBackground);
	
	GUI.Label(Rect (512-150, 20, 80, 80), backButton);
	GUI.Label(Rect (512+110, 20, 80, 80), forwardButton);
	
	if(month == 1)
		GUI.Label(Rect(512-70, -25, 175, 175), januaryButton);
	if(month == 2)
		GUI.Label(Rect(512-70, -25, 175, 175), februaryButton);
	if(month == 3)
		GUI.Label(Rect(512-70, -25, 175, 175), marchButton);
	if(month == 4)
		GUI.Label(Rect(512-70, -25, 175, 175), aprilButton);
	if(month == 5)
		GUI.Label(Rect(512-70, -25, 175, 175), mayButton);
	if(month == 6)
		GUI.Label(Rect(512-70, -25, 175, 175), juneButton);
	if(month == 7)
		GUI.Label(Rect(512-70, -25, 175, 175), julyButton);
	if(month == 8)
		GUI.Label(Rect(512-70, -25, 175, 175), augustButton);
	if(month == 9)
		GUI.Label(Rect(512-70, -25, 175, 175), septemberButton);
	if(month == 10)
		GUI.Label(Rect(512-70, -25, 175, 175), octoberButton);
	if(month == 11)
		GUI.Label(Rect(512-70, -25, 175, 175), novemberButton);
	if(month == 12)
		GUI.Label(Rect(512-70, -25, 175, 175), decemberButton);
	
	
	
	
	
	
	if(weather == 1){
		if (weatherRect.Contains(Input.mousePosition) && Input.GetMouseButtonDown(0)){
			GUI.Label(Rect (835, 600, 128, 128), cloudyButtonClick);
			print ("You clicked cloudyRect!");
			//Add functionality here
		}
		else if(weatherRect.Contains(Input.mousePosition)){
			GUI.Label(Rect (835, 600, 128, 128), cloudyButtonHover);
		}
		else{
			GUI.Label(Rect (835, 600, 128, 128), cloudyButtonNormal);
		}
	}
	if(weather == 2){
		if (weatherRect.Contains(Input.mousePosition) && Input.GetMouseButtonDown(0)){
			GUI.Label(Rect (835, 600, 128, 128), partlycloudyButtonClick);
			print ("You clicked partlycloudyRect!");
			//Add functionality here
		}
		else if(weatherRect.Contains(Input.mousePosition)){
			GUI.Label(Rect (835, 600, 128, 128), partlycloudyButtonHover);
		}
		else{
			GUI.Label(Rect (835, 600, 128, 128), partlycloudyButtonNormal);
		}
	}
	if(weather == 3){
		if (weatherRect.Contains(Input.mousePosition) && Input.GetMouseButtonDown(0)){
			GUI.Label(Rect (835, 600, 128, 128), rainyButtonClick);
			print ("You clicked rainyRect!");
			//Add functionality here
		}
		else if(weatherRect.Contains(Input.mousePosition)){
			GUI.Label(Rect (835, 600, 128, 128), rainyButtonHover);
		}
		else{
			GUI.Label(Rect (835, 600, 128, 128), rainyButtonNormal);
		}
	}
	if(weather == 4){
		if (weatherRect.Contains(Input.mousePosition) && Input.GetMouseButtonDown(0)){
			GUI.Label(Rect (535, 620, 75, 75), sunnyButtonClick);
			print ("You clicked sunnyRect!");
			//Add functionality here
		}
		else if(weatherRect.Contains(Input.mousePosition)){
			GUI.Label(Rect (835, 600, 128, 128), sunnyButtonHover);
		}
		else{
			GUI.Label(Rect (835, 600, 128, 128), sunnyButtonNormal);
		}
	}
	if(weather == 5){
		if (weatherRect.Contains(Input.mousePosition) && Input.GetMouseButtonDown(0)){
			GUI.Label(Rect (835, 600, 128, 128), snowButtonClick);
			print ("You clicked snowRect!");
			//Add functionality here
		}
		else if(weatherRect.Contains(Input.mousePosition)){
			GUI.Label(Rect (835, 600, 128, 128), snowButtonHover);
		}
		else{
			GUI.Label(Rect (835, 600, 128, 128), snowButtonNormal);
		}
	}
	
	
}


function OnGUI () {
    //load GUI skin
    //GUI.skin = menuSkin;
    
    GameMenu();
    
}

function HandleInput(){
	if (backRect.Contains(Input.mousePosition) && Input.GetMouseButtonDown(0)){
		print ("You clicked backRect!");
		if(month > 1){
			month = month - 1;
		}
		//Add functionality here
	}
	
	if (forwardRect.Contains(Input.mousePosition) && Input.GetMouseButtonDown(0)){
		print ("You clicked forwardRect!");
		if(month < 12){
			month = month + 1;
		}
		//Add functionality here
	}
	
}

function Update () {
	HandleInput();
	
}