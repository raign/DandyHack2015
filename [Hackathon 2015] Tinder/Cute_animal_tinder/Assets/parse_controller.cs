using UnityEngine;
using System.Collections;

using Parse;
using System.Collections.Generic;


public class parse_controller: MonoBehaviour {
	public static Texture2D [] pic_arr = new Texture2D[10];

	public static int [] ids = new int[10];
	public static int j = 0;

	string status = "";
	bool load_complete = false;
	int img_num =0;
	ParseObject test;
	string score = "";
	int num;
	string s;
	int count=0;
	string[] id = new string[4];
	bool next = false;
	public GUIStyle sample;
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

		leadership ();

	}

	void download_photos(int size)
	{
		for (int i=0; i< size; i++)
		{
			StartCoroutine (loadFile (i, id_num));
			id_num++;
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
	

	void leadership()
	{
		/*
		Debug.Log ("leader");
		ParseQuery<ParseObject> query = ParseObject.GetQuery ("TestObject").OrderBy("vote").Limit (10);

		query.FindAsync (t =>
		{
			
		});
		
		query.GetAsync("WYAnKqCN3X").ContinueWith(t =>
		{
			ParseObject gameScore = t.Result;
			int iid = gameScore.Get<int>("id_num");
			Debug.Log ("1st place: " + iid);
		});

*/
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
					Debug.Log ("id_num" + leader.Get<int>("id_num") + "|   vote: " + leader.Get<int>("votes"));
				}
			}
		}
		);
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
			sample.normal.background = pic_arr[count];
		}

		//GUI.Box (new Rect (Screen.width / 4, Screen.height /8, Screen.width / 2, Screen.height / 2), "asdf: "+ status, sample);

	}

}
