using UnityEngine;
using System.Collections;

using Parse;
using System.Collections.Generic;


public class parse_controller: MonoBehaviour {
	public static Texture2D [] pic_arr = new Texture2D[10];
	public static int [] rank_id_arr = new int[10];
	public static Texture2D [] rank_pic_arr = new Texture2D[10];

	public static int [] ids = new int[10];
	public static int j = 0;
	public GUIStyle return_icon;
	public GUIStyle views;
	int rank_count=0;

	public GUIStyle heading;
	public GUIStyle icon;
	public GUIStyle leader_bg;
	public GUIStyle title;
	public GUIStyle lead_pic;
	bool show_leader_board;

	string status = "";
	bool load_complete = false;
	int img_num =0;
	public GUIStyle info;
	float button_width = Screen.width/4;

	ParseObject test;
	string score = "";
	int num;
	string s;
	int count=0;
	string[] id = new string[4];
	bool next = false;
	public GUIStyle pic;
	public GUIStyle loading;
	int id_num;
	int size;

	// Use this for initialization
	void user_log_in() {
		//LoadFile ();
		/*
		//upload string/data types
		ParseObject testObject = new ParseObject ("TestObject");
		testObject ["foo"] = "bar";
		testObject.SaveAsync ();
*/
		//sign up users
		var user = new ParseUser ()
		{
			Username = "my name",
			Password = "my pass",
			Email = "email@example.com"
		};
	
		user.SignUpAsync ();
	}

	// Update is called once per frame
	void Update () 
	{		


		if(Input.GetKeyDown("down"))
		//if (Input.GetTouch(0).phase == TouchPhase.Began)
		{			
			//StopCoroutine(loadFile ());
			count++;
			status = "DOWN: " + count;
		}

		if (testing.download_more) 
		{
			StartCoroutine(wait(3));
			
			testing.download_more = false;
			download_photos (size);
			StartCoroutine(wait(3));		
		}

		if (testing.d) { // if dislike is true
			testing.d = false;
			incrementor(-1);
		}
		if (testing.l) { // if like is true
			testing.l = false;
			incrementor(1);
		}
	}



	void Start()
	{
		size = 10;
		id_num = 0;
		download_photos (10);
		wait (10);

		show_leader_board = false;

		show_leadership ();


	}


	void download_photos(int size)
	{

		for (int i=0; i< size; i++)
		{
			id_num = Random.Range (0,100);
			StartCoroutine (loadFile (i, id_num));
		}
	}

	void incrementor(int i) 
	{
		ParseQuery<ParseObject> votes = ParseObject.GetQuery("TestObject").WhereEqualTo("id_num", ids[j]); // Get the image id
		
		var votesTask = votes.FirstAsync().ContinueWith(t => {
			ParseObject obj = t.Result;
			obj.Increment("votes", i);
			obj.Increment("Views");
			Debug.Log("id_num: " + ids[j] + " vote: " + i);
			obj.SaveAsync();
		});
		
		j++;
		if (j == 10) {
			j = 0;
		}
	}

	IEnumerator wait(int time) {
		Debug.Log("Before Waiting 2 seconds");
		load_complete = false;
		yield return new WaitForSeconds(time);
		load_complete = true;
		Debug.Log("After Waiting 2 Seconds");
	}

	void show_leadership()
	{
		leadership();
		
		Debug.Log ("leadership:");
		for (int i=0; i< 10; i++) {
			Debug.Log (rank_id_arr[i]);
		}
		for (int i=0; i<10; i++) 
		{
			StartCoroutine (loadRank (i));
			//id_num++;		
		}
	}

	void leadership()
	{
		int counter = 0;
		var query = new ParseQuery<ParseObject> ("TestObject").OrderByDescending("vote").Limit (10);
		query.FindAsync().ContinueWith(t => 
		                               {
			if (t.IsCanceled || t.IsFaulted ) {
				Debug.Log("Leaderboard Listing Failed");
			} else 
			{
				Debug.Log("Top 10");
				IEnumerable<ParseObject> results = t.Result;
				foreach (ParseObject leader in results)  
				{
					rank_id_arr[counter] = leader.Get<int>("id_num");
					counter++;
					Debug.Log ("id_num" + leader.Get<int>("id_num") + "|   vote: " + leader.Get<int>("votes"));
				}
			}
		}
		);
	}

	IEnumerator loadRank(int count)
	{
		//Debug.Log ("RANKRANK: " + rank_id_arr[count]);
		int idd = rank_id_arr [count];
		ParseQuery<ParseObject> query = ParseObject.GetQuery("TestObject" ).WhereEqualTo( "id_num" , idd);
		
		var queryTask = query.FirstAsync();
		
		while (!queryTask.IsCompleted) yield return null;
		
		ParseObject obj = queryTask.Result;
		ParseFile pfile = obj.Get<ParseFile>("Image" );
		Debug.Log (pfile.Url.AbsoluteUri);
		var imageRequest = new WWW(pfile.Url.AbsoluteUri);
		yield return imageRequest;
		//Debug.Log ("imageRequest " + imageRequest.text);
		renderer.material.mainTexture = imageRequest.texture;
		
		rank_pic_arr[count] = imageRequest.texture;		
	}


	IEnumerator loadFile(int i, int idd)
	{
		//Debug.Log ("load file");
		ParseQuery<ParseObject> query = ParseObject.GetQuery("TestObject" ).WhereEqualTo( "id_num" , idd);

		var queryTask = query.FirstAsync();
			
		while (!queryTask.IsCompleted) yield return null;
			
		ParseObject obj = queryTask.Result;
		ParseFile pfile = obj.Get<ParseFile>("Image" );
		Debug.Log (pfile.Url.AbsoluteUri);
		var imageRequest = new WWW(pfile.Url.AbsoluteUri);
		yield return imageRequest;
		//Debug.Log ("imageRequest " + imageRequest.text);
		renderer.material.mainTexture = imageRequest.texture;

		pic_arr[i] = imageRequest.texture;
		ids [i] = idd + 1;

		
		load_complete = true;
	}

	void OnGUI()
	{
		if (!load_complete) {
			GUI.Box (new Rect (Screen.width/4, Screen.height/2 - Screen.height/8, Screen.width/2, Screen.height/6), "LOADING...", loading);
		}

		if (pic_arr[count]!=null) 
		{
			pic.normal.background = pic_arr[count];
		}

		//info
		if(GUI.Button(new Rect(Screen.width/2 - (float)((button_width * 0.75)/2), Screen.height - Screen.height/5 + (float)(button_width*0.15), (float)(button_width*0.75), (float)(button_width*0.75)), "", info))
			//if(GUI.Button(new Rect(Screen.width/2 - (button_width*0.75)/2, Screen.height - Screen.height/5 + button_width*0.15, button_width*0.75, button_width*0.75), "", info))
		{
			show_leadership();
			//Application.LoadLevel(1);
			show_leader_board = true;

		}

		float pic_width = Screen.width - Screen.width/3;
		float def_pic_x = Screen.width/2 - pic_width/2;
		float def_pic_y = (float)(Screen.height * 0.2);

		GUI.Label (new Rect (Screen.width / 2, (float)(def_pic_y * 0.8) + (float)(pic_width * 1.3), Screen.width/8, Screen.width/16), "", views);
		//GUI.Label (new Rect (Screen.width / 2, (float)(def_pic_y * 0.8) + (float)(pic_width * 1.3), Screen.width / 8, Screen.width / 8), "", views);


		if(show_leader_board == true)
		{
			GUI.Box (new Rect (0,0,Screen.width, Screen.height), "", leader_bg);

			pic.fontSize = Screen.width / 10;

			if (rank_count > 0) {
				if (GUI.Button (new Rect (Screen.width / 8 - Screen.width / 16, Screen.height / 2, Screen.width / 8, Screen.width / 8), "<", icon)) {
					rank_count--;
					Debug.Log ("count: " + rank_count + "||  ranking: " + rank_id_arr[rank_count]);
				}
			}
			if (rank_count < 9) {
				if (GUI.Button (new Rect ((float)(Screen.width * 0.875) - Screen.width / 16, Screen.height / 2, Screen.width / 8, Screen.width / 8), ">", icon)) {
					rank_count++;
					Debug.Log ("count: " + rank_count + "||  ranking: " + rank_id_arr[rank_count]);
				}
			}

			title.fontSize = Screen.width/8;
			GUI.Label (new Rect(Screen.width/4, Screen.width/8, Screen.width/2, Screen.height/12), "TOP 10 CAT!", title);

			if(GUI.Button(new Rect(Screen.width/2 - (float)(Screen.width*0.35), Screen.height - Screen.height/6, (float)(Screen.width*0.7), Screen.height/8), "Return to Menu", return_icon))
			{
				show_leader_board = false;
			}

			heading.fontSize = Screen.width/10;
			GUI.Label (new Rect (Screen.width / 4, (float)(Screen.height * 0.175), Screen.width / 2, Screen.width / 2),"Ranking: " + (rank_count+1), heading);

			lead_pic.normal.background = rank_pic_arr[rank_count];

			GUI.Box (new Rect (Screen.width/2 - (float)(Screen.width*0.3), Screen.height/2 - Screen.width/4, (float)(Screen.width*0.6), (float)(Screen.width*0.6)),"", lead_pic);
		}






	}

}
