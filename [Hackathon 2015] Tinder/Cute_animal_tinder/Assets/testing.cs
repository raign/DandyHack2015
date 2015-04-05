using UnityEngine;
using System.Collections;

public class testing : MonoBehaviour {
	public GUIStyle heart;
	public GUIStyle cross;
	public GUIStyle info;
	public GUIStyle pic;
	public GUIStyle nav;
	public GUIStyle message;
	public GUIStyle frame;
	public GUIStyle bg;
	public GUIStyle chick_icon;
	public GUIStyle title;
	public GUIStyle inv;

	public Camera mainCam;

	int size;
	int count;
	public static bool download_more;
	public Texture2D[] pic_server;

	bool drag;
	int drag_status; //-1:dislike, 1:like.

	//Like/Dislike
	public static bool l; 
	public static bool d;

	//USER
	string user_name;
	string status_message;
	string hate_like;

	//Download
	public static bool load_photo = false;

	//PICTURE FRAME
	float pic_width;
	float pic_x;
	float pic_y;
	//default pic_location
	float def_pic_x;
	float def_pic_y;
	float click_x;
	float click_y;
	float offset_x;
	float offset_y;

	bool mouse;

	// Use this for initialization
	void Start () 
	{

		//Texture2D omg = parse_controller.pic_arr[0];
		mouse = true;
		pic_width = Screen.width - Screen.width/3;
		def_pic_x = Screen.width/2 - pic_width/2;
		def_pic_y = (float)(Screen.height * 0.2);

		//STATUS
		user_name = "User";
		hate_like = "";
		status_message = "";

		l = false;
		d = false;

		//Pic_arr
		size = 10;

		drag = false;
		drag_status=0;

		download_more = false;




		pic_server = parse_controller.pic_arr;
		load_photo = true;
		count++;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown ("right"))
		{
			Debug.Log ("Hit right."  + "count: " + count);
			pic_server = parse_controller.pic_arr;
			load_photo = true;
			count++;
		}

		if(Input.GetKeyDown ("up"))
		{
			download_more = true;
			Debug.Log ("up");
		}

		if(Input.GetKeyDown ("down"))
		{
			Debug.Log ("Hit down. " + "count: " + count);
			count++;
		}


		// DRAG ===> 
		//only can drag if click on photo.
		if(mouse)
		{
			if(Input.GetMouseButtonDown(0)
			&& (Input.mousePosition.x > def_pic_x) 
			&& (Input.mousePosition.x < (def_pic_x + pic_width))
			&& (Input.mousePosition.y < (Screen.height- def_pic_y))
			&& (Input.mousePosition.y > (Screen.height - (def_pic_y + pic_width)))
			&& drag==false
			)
			{
				Debug.Log ("drag..ON");
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
			&& (Input.GetTouch(0).position.y < (Screen.height- def_pic_y))
			&& (Input.GetTouch(0).position.y > (Screen.height - (def_pic_y + pic_width)))
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
				pic_x = Input.GetTouch(0).position.x - offset_x;
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
		//	submit();
			// TODO: [SERVER]: send message to server!!
		}
		//when right is is past the right side.
		else if(pic_x + pic_width > Screen.width)
		//mainCam.ScreenToWorldPoint(new Vector3(Screen.width,0,0)).x)
		{
	//		Debug.Log("Like");
			
			drag_status = 1;
		//	submit();
			// TODO: [SERVER]: send message to server!!
		}
		else
		{
			drag_status = 0;
		}
		
			
		
		//CHANGING photos => move along array
		// make sure texture2D array won't go out of bounds.
		if (load_photo && count<size)
		{
			pic.normal.background = pic_server[count];
		}
		else if(download_more == false)
		{
			Debug.Log ("download_more");
			download_more = true;
			count = 0;
		}
	}

	void reset_message()
	{
		status_message = "";
		hate_like = "";
	}

	void status_check()
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

	void dislike()
	{
			hate_like = "dislikes";
			status_message = user_name + " " + hate_like + " " + "photo " + count;
			//yield WaitForSeconds(0.5);
			reset_message();
		d = true;

		count++;
			
			//submit();
			// TODO: [SERVER]: send message to server!!
	}

	void like()
	{
		l = true;

		hate_like = "likes";
			status_message = user_name + " " + hate_like + " " + "photo " + count;
			//yield WaitForSeconds(0.5);
			reset_message();
			
			count++;
			
			//submit();
			// TODO: [SERVER]: send message to server!!
	}

	void OnGUI()
	{

		GUI.Box(new Rect(0,0,Screen.width, Screen.height), "", bg);

		//INTERFACE BUTTONS ====>
		
		//cross
		float button_width = Screen.width/4;
		if(GUI.Button(new Rect(Screen.width/8, Screen.height - Screen.height/5, button_width, button_width), "", cross))
		{
			dislike();
		}
		
		//heart
		if(GUI.Button(new Rect(Screen.width - Screen.width/8 - button_width, Screen.height - Screen.height/5, button_width, button_width), "", heart))
		{
			like();
		}
		
		//info
		if(GUI.Button(new Rect(Screen.width/2 - (float)((button_width * 0.75)/2), Screen.height - Screen.height/5 + (float)(button_width*0.15), (float)(button_width*0.75), (float)(button_width*0.75)), "", info))
		//if(GUI.Button(new Rect(Screen.width/2 - (button_width*0.75)/2, Screen.height - Screen.height/5 + button_width*0.15, button_width*0.75, button_width*0.75), "", info))
		{
			
		}
		

		
		//nav (top of app)
		GUI.Label(new Rect(0, 0, Screen.width, Screen.height/10), "", nav);
		GUI.Box(new Rect(Screen.width/15, 0, Screen.width/8, Screen.width/8), "", chick_icon);
		GUI.Box(new Rect(Screen.width/15 + Screen.width/8 + Screen.width/25, Screen.height/40, (float)(Screen.width*0.75), (float)(Screen.height/20)), "", title);
		
		
		GUI.Box(new Rect((float)(def_pic_x*0.7), (float)(def_pic_y*0.8), Screen.width - (float)(def_pic_x * 1.4), (float)(pic_width * 1.3)), "", frame);

			//THE ACTUAL PICTURE -> location.
		//GUI.Box(new Rect(pic_x, pic_y, pic_width, pic_width), "", pic);

		//STATUS MESSAGE...
		//GUI.Label(new Rect(0, Screen.height - Screen.height/12, Screen.width, Screen.height/8), "Status: " + status_message, message);
		//message.fontSize = Screen.width/15;
		

		//PICTURE ==>

		GUI.Box(new Rect(pic_x, pic_y, pic_width, pic_width), "", pic);

		//MESSAGE -> DISLIKE_LIKE DRAG ==>

		inv.fontSize = Screen.width/6;
		//Prompt User to release to like/dislike photo
		if(pic_x < mainCam.ScreenToWorldPoint(new Vector3(0,0,0)).x)
		{
			GUI.Label(new Rect(0,Screen.height/4, Screen.width, Screen.height/2), "RELEASE TO DISLIKE!" , inv);
		}
		//when right is is past the right side.
		else if(pic_x + pic_width > Screen.width)
		//mainCam.ScreenToWorldPoint(new Vector3(Screen.width,0,0)).x)
		{
			GUI.Label(new Rect(0,Screen.height/4, Screen.width, Screen.height/2), "RELEASE TO LIKE!" , inv);
		}
		else
		{
			GUI.Label(new Rect(0,Screen.height/4, Screen.width, Screen.height/2), "" , inv);
		}



		


	}
}
