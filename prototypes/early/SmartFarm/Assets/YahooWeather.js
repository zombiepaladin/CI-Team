var url = "http://weather.yahooapis.com/forecastrss?w=12786862&u=f";
var node;
var zip;
function Start(){
	if(PlayerPrefs.HasKey("zipcode"))
		zip=PlayerPrefs.GetString("zipcode");
	else
		Application.LoadLevel("getzip");
	url = "http://xml.weather.yahoo.com/forecastrss?p="+zip;
	var www : WWW = new WWW(url);
	yield www;
	parse= new XMLParser();
	node = parse.Parse(www.text);
	var lat= node["rss"][0]["channel"][0]["item"][0]["geo:lat"][0]["_text"];
	var lon= node["rss"][0]["channel"][0]["item"][0]["geo:long"][0]["_text"];
	url = "http://forecast.weather.gov/MapClick.php?lat="+lat+"0000&lon="+lon+"0000&FcstType=dwml";
	var www1: WWW = new WWW(url);
	yield www1;
	parse = new XMLParser();
	node = parse.Parse(www1.text);
	
}
function Update(){
}

function OnGUI(){
	var current= node["dwml"][0]["data"][1];
	var temperature = current["parameters"][0]["temperature"][0]["value"][0]["_text"];
	var rhumid= current["parameters"][0]["humidity"][0]["value"][0]["_text"];
	var pressure= current["parameters"][0]["pressure"][0]["value"][0]["_text"]+ "in";
	var condit= current["parameters"][0]["weather"][0]["weather-conditions"][0]["@weather-summary"];
	var windspeed = current["parameters"][0]["wind-speed"][1]["value"][0]["_text"]+"mph";
	
	
	GUI.Box(Rect(20,145, 300, 300), "Weather");
	GUI.Label(Rect(40,170, 200,50), "Temperature");
	GUI.Label(Rect(220, 170, 200,50), temperature);
	GUI.Label(Rect(40,220, 200,50), "Relative Humidity");
	GUI.Label(Rect(220,220, 200,50), rhumid);
	GUI.Label(Rect(40,260, 200,50), "Pressure");
	GUI.Label(Rect(220,260, 200,50), pressure);
	GUI.Label(Rect(40,320, 200,50), "How's the day?");
	GUI.Label(Rect(220,320, 200,50), condit);
	GUI.Label(Rect(40,370, 200,50), "Wind");
	GUI.Label(Rect(220,370, 200,50), windspeed);
	
	if(GUI.Button(Rect(110, 390, 100, 25), "Refresh"))
		Application.LoadLevel("weather");
}
	
	