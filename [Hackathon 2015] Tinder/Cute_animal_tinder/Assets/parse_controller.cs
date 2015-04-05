using UnityEngine;
using System.Collections;

using Parse;
using System.Collections.Generic;




public class parse_controller: MonoBehaviour {
	public static Texture2D [] pic_arr = new Texture2D[11];

	string status = "";
	bool load_complete = false;
	int img_num =0;
	ParseObject test;
	string score = "";
	int num;
	string s;
	public static int count=0;
	string[] id = new string[4];
	bool next = false;
	public GUIStyle sample;

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
	}



	void Start()
	{
		for (int i=0; i< 11; i++) 
		{
			StartCoroutine (loadFile (i));
		}

	}
	
	IEnumerator loadFile(int ii)
	{
		Debug.Log ("load file");
		ParseQuery<ParseObject> query = ParseObject.GetQuery("TestObject" ).WhereEqualTo( "id_num" , ii);

		var queryTask = query.FirstAsync();
			
		while (!queryTask.IsCompleted) yield return null;
			
		ParseObject obj = queryTask.Result;
		ParseFile pfile = obj.Get<ParseFile>("Image" );
		Debug.Log (pfile.Url.AbsoluteUri);
		var imageRequest = new WWW(pfile.Url.AbsoluteUri);
		yield return imageRequest;
		Debug.Log ("imageRequest " + imageRequest.text);
		//renderer.material.mainTexture = imageRequest.texture;
		renderer.material.mainTexture = imageRequest.texture;

		pic_arr[ii] = imageRequest.texture;

		load_complete = true;
	}

	void OnGUI()
	{
		if (!load_complete) {
			GUI.Box (new Rect (Screen.width / 4, Screen.height /12, Screen.width / 2, Screen.height / 2), "LOADING...");
		}

		if (pic_arr[count]!=null) 
		{
			sample.normal.background = pic_arr[count];
		}

		//GUI.Box (new Rect (Screen.width / 4, Screen.height /8, Screen.width / 2, Screen.height / 2), "asdf: "+ status, sample);

	}

}
