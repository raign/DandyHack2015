using UnityEngine;
using System.Collections;

using Parse;
using System.Collections.Generic;



public class parse_controller: MonoBehaviour {
	Texture2D asdf;
	Texture2D ass;
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

	// Use this for initialization
	void getString() {
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

		getParseResults ();
	}


	
	// Update is called once per frame
	void Update () 
	{		
		if(Input.GetKeyDown("down"))
		//if (Input.GetTouch(0).phase == TouchPhase.Began)
		{			
			//StopCoroutine(loadFile ());
			count++;
			Debug.Log ("down: " + count);
			status = "DOWN" + count;
			//StartCoroutine (loadFile());
		}
	}
	
	void getParseResults()
	{
		Debug.Log ("dd");

		var bigObject = new ParseObject("BigObject");
		bigObject["myNumber"] = 1;
		bigObject["myString"] = "a";
		bigObject.SaveAsync();

		ParseQuery<ParseObject> query = ParseObject.GetQuery("BigObject");
		query.GetAsync("D3pswTQZJN").ContinueWith(t =>
		                                        {
			ParseObject bigObject2 = t.Result;
			num = bigObject2.Get<int>("myNumber");
			s = bigObject2.Get<string>("myString");
			Debug.Log ("Number: " + num);
			Debug.Log ("String: " + s);
		});
	}

	void getID()
	{
		
	
	}
	
	void Awake()
	{
	}

	Texture2D [] pic_arr = new Texture2D[11];

	void Start()   //single method downloads file
	{
		//id [1] = "bOL9HnLMqn";
		//id [2] = "j1HBJC9b4T";



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

		//yield return ass;
	}

	void OnGUI()
	{
		if (!load_complete) {
			GUI.Box (new Rect (Screen.width / 4, Screen.height /12, Screen.width / 2, Screen.height / 2), "LOADING...");
		}

		if (pic_arr[count]!=null) 
		{
			sample.normal.background = pic_arr[count];
				//pic_arr[0];
		}
		GUI.Box (new Rect (Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2), "asdf: "+ status, sample);

	}

}
