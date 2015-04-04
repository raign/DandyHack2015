#pragma strict

var heart:GUIStyle;
var cross:GUIStyle;
var info:GUIStyle;
var pic:GUIStyle;
var nav: GUIStyle;
var message:GUIStyle;

var size:int;
var pic_arr: Texture2D[]= new Texture2D[5];
var count:int;

//status
var user_name:String;
var status_message:String;
var hate_like:String;


function Start () 
{
	user_name = "Jinsi";
	hate_like = "";
	size = 5;
	status_message = "";
	count = 0;
}

function Update () 
{
	if(Input.GetKeyDown("left") && count !=0)
	{
		count--;
		Debug.Log(count);
		reset_message();
	}
	if(Input.GetKeyDown("right"))
	{
		count++;
		Debug.Log(count);
		reset_message();
	}
}

function reset_message()
{
	status_message = "";
	hate_like = "";
}


function OnGUI()
{
	//INTERFACE BUTTONS ====>
	
	//cross
	var button_width:float = Screen.width/8;
	if(GUI.Button(new Rect(Screen.width/4, Screen.height - Screen.height/5, button_width, button_width), "", cross))
	{
		hate_like = "dislikes";
		status_message = user_name + " " + hate_like + " " + "photo " + count;
		count++;
		
		// TODO: [SERVER]: send message to server!!
	}
	
	//heart
	if(GUI.Button(new Rect(Screen.width/2 + button_width, Screen.height - Screen.height/5, button_width, button_width), "", heart))
	{
		hate_like = "likes";
		status_message = user_name + " " + hate_like + " " + "photo " + count;
		if(count != 0)
		{
			count--;
		}
		
		// TODO: [SERVER]: send message to server!!
	}
	
	//info
	if(GUI.Button(new Rect(Screen.width/2 - button_width/2, Screen.height - Screen.height/5, button_width, button_width), "", info))
	{
		//TODO: info button.
		
		// TODO: [SERVER]: send message to server!!
	}
	
	//nav
	GUI.Label(new Rect(0, 0, Screen.width, Screen.height/15), "", nav);
	
	//PICTURE FRAME =====> 
	var pic_width:float = Screen.width - Screen.width/3; // = 2/3
	GUI.Box(new Rect(Screen.width/2 - pic_width/2, Screen.height/2 - pic_width/2, pic_width, pic_width), "", pic);
	
	// make sure texture2D array won't go out of bounds.
	if(count < size)
	{
		pic.normal.background = pic_arr[count];
	}
	
	GUI.Label(new Rect(0, Screen.height - Screen.height/8, Screen.width, Screen.height/8), "Status: " + status_message, message);
}







