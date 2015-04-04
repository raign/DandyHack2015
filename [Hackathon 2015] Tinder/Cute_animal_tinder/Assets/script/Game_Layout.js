///DRAG -> SWIPE -> status message doesn't work when you drag it.


#pragma strict

//GUIStyles
var heart:GUIStyle;
var cross:GUIStyle;
var info:GUIStyle;
var pic:GUIStyle;
var nav: GUIStyle;
var message:GUIStyle;
var inv:GUIStyle;

var mainCam:Camera;


//access array of images
var size:int;
var pic_arr: Texture2D[]= new Texture2D[5];
var count:int;

//status_message
var user_name:String;
var status_message:String;
var hate_like:String;

//drag
var drag:boolean;
var drag_status:int;//-1:dislike, 1:like

//Picture Frame
var pic_width:float;
var pic_x:float;
var pic_y:float;
//default pic location
var def_pic_x:float;
var def_pic_y:float;
var click_x:float;
var click_y:float;
var offset_x:float;
var offset_y:float;


//mouse
var mouse:boolean;


function Start () 
{
	mouse = true;
	
	//picture frame:
	pic_width = Screen.width - Screen.width/3;
	//mainCam.WorldToScreenPoint(new Vector3(Screen.width - Screen.width/3,0,0)).x; // = 2/3
	
	def_pic_x = Screen.width/2 - pic_width/2;
	def_pic_y = Screen.height/2 - pic_width/2;
	
	user_name = "User";
	hate_like = "";
	size = 5;
	status_message = "";
	count = 0;
	drag = false;
	drag_status=0;
}

function Update () 
{
	// Just traverse left and right into the list of photos easier ===> 
	if(Input.GetKeyDown("left") && count !=0)
	{
		count--;
		//Debug.Log(count);
		reset_message();
	}
	if(Input.GetKeyDown("right"))
	{
		count++;
		//Debug.Log(count);
		reset_message();
	}
	
	
	// DRAG ===> 
	//only can drag if click on photo.
	if(mouse)
	{
		if(Input.GetMouseButtonDown(0)
		&& (Input.mousePosition.x > def_pic_x) 
		&& (Input.mousePosition.x < (def_pic_x + pic_width))
		&& (Input.mousePosition.y > def_pic_y)
		&& (Input.mousePosition.y < def_pic_y + pic_width)
		&& drag==false
		)
		{
			drag = true;
			
			offset_x = Input.mousePosition.x - def_pic_x;
			offset_y = (Screen.height - Input.mousePosition.y) - def_pic_y;
		}
	}
	//mouse == false
	else
	{
		if (Input.GetTouch(0).phase == TouchPhase.Began
		&& (Input.GetTouch(0).position.x > def_pic_x) 
		&& (Input.GetTouch(0).position.x < (def_pic_x + pic_width))
		&& (Input.GetTouch(0).position.y > def_pic_y)
		&& (Input.GetTouch(0).position.y < def_pic_y + pic_width)
		&& drag==false
		)
		{
			drag = true;
			
			offset_x = Input.GetTouch(0).position.x - def_pic_x;
			offset_y = (Screen.height - Input.GetTouch(0).position.y) - def_pic_y;
		}
	}


	//Input.GetMouseButton(0) == true when pressed down. vice versa.
	//when let go mouse, and drag is still on, status_check move to next photo (count++)
	if(mouse)
	{
		if(!Input.GetMouseButton(0) && drag==true)
		{
			drag = false;
			status_check();
			reset_message();
		}	
	}
	else
	{
		if((Input.GetTouch(0).phase == TouchPhase.Ended) && drag==true)
		{
			drag = false;
			status_check();
			reset_message();
		}
	}

	
	//PICTURE FRAME =====> 
	//Drag PART I: when drag is on, follow mouse.
	if(drag)
	{
		if(mouse)
		{
			pic_x = Input.mousePosition.x - offset_x;
			pic_y = (Screen.height - Input.mousePosition.y) - offset_y;
		}
		else
		{
			pic_x = Input.GetTouch(0).position.x;
			pic_y = (Screen.height - Input.GetTouch(0).position.y) - offset_y;
		}
		
		//Debug.Log(Input.mousePosition.x);
		//- pic_width/2;
		//pic_width/2;
	}
	//when drag not on, move to default position.
	else
	{
		pic_x = def_pic_x;
		pic_y = def_pic_y;
	}
	
	
	//Drag PART II: Swipe => likes and dislikes.
	//when dragged to left... 
	//when left side of pic is past the left side.
	
	
	//var left_screen:float = mainCam.
	if(pic_x < mainCam.ScreenToWorldPoint(new Vector3(0,0,0)).x)
	{
		Debug.Log("dislike");
		
		drag_status = -1;
		submit();
		// TODO: [SERVER]: send message to server!!
	}
	//when right is is past the right side.
	else if(pic_x + pic_width > Screen.width)
	//mainCam.ScreenToWorldPoint(new Vector3(Screen.width,0,0)).x)
	{
//		Debug.Log("Like");
		
		drag_status = 1;
		submit();
		// TODO: [SERVER]: send message to server!!
	}
	else
	{
		drag_status = 0;
	}
	
		
	
	//CHANGING photos => move along array
	// make sure texture2D array won't go out of bounds.
	if(count < size)
	{
		pic.normal.background = pic_arr[count];
	}
	else
	{
		count = 0;
	}
}

function reset_message()
{
	status_message = "";
	hate_like = "";
}

function status_check()
{
	if(drag_status==-1)
	{
		dislike();
		Debug.Log("Next photo... ");

	}
	else if(drag_status==1)
	{
		like();
		Debug.Log("Next photo... ");
	}
	//no like nor dislike
	else
	{
	
	}
}


function dislike()
{
		hate_like = "dislikes";
		status_message = user_name + " " + hate_like + " " + "photo " + count;
		yield WaitForSeconds(0.5);
		reset_message();

		count++;
		
		submit();
		// TODO: [SERVER]: send message to server!!
}

function like()
{
		hate_like = "likes";
		status_message = user_name + " " + hate_like + " " + "photo " + count;
		yield WaitForSeconds(0.5);
		reset_message();
		
		count++;
		
		submit();
		// TODO: [SERVER]: send message to server!!
}

function OnGUI()
{
	//INTERFACE BUTTONS ====>
	
	//cross
	var button_width:float = Screen.width/8;
	if(GUI.Button(new Rect(Screen.width/4, Screen.height - Screen.height/5, button_width, button_width), "", cross))
	{
		dislike();
	}
	
	//heart
	if(GUI.Button(new Rect(Screen.width/2 + button_width, Screen.height - Screen.height/5, button_width, button_width), "", heart))
	{
		like();
	}
	
	//info
	if(GUI.Button(new Rect(Screen.width/2 - button_width/2, Screen.height - Screen.height/5, button_width, button_width), "", info))
	{
		//TODO: info button.
		
		submit();
		// TODO: [SERVER]: send message to server!!
	}
	
	//nav (top of app)
	GUI.Label(new Rect(0, 0, Screen.width, Screen.height/15), "", nav);

	//THE ACTUAL PICTURE -> location.
	GUI.Box(new Rect(pic_x, pic_y, pic_width, pic_width), "", pic);

	//STATUS MESSAGE...
	GUI.Label(new Rect(0, Screen.height - Screen.height/12, Screen.width, Screen.height/8), "Status: " + status_message, message);
	message.fontSize = Screen.width/15;
	
	
	inv.fontSize = Screen.width/6;
	//Prompt User to release to like/dislike photo
	if(pic_x < mainCam.ScreenToWorldPoint(new Vector3(0,0,0)).x)
	{
		GUI.Label(new Rect(0,Screen.height/4, Screen.width, Screen.height/2), "RELEASE \n TO \n  LIKE!" , inv);
	}
	//when right is is past the right side.
	else if(pic_x + pic_width > Screen.width)
	//mainCam.ScreenToWorldPoint(new Vector3(Screen.width,0,0)).x)
	{
		GUI.Label(new Rect(0,Screen.height/4, Screen.width, Screen.height/2), "RELEASE TO DISLIKE!" , inv);
	}
	else
	{
		GUI.Label(new Rect(0,Screen.height/4, Screen.width, Screen.height/2), "" , inv);
	}
}




function submit()
{
	
	//count++;
}





