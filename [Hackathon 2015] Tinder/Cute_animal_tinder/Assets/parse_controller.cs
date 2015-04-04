using UnityEngine;
using System.Collections;

using Parse;
using System.Collections.Generic;


public class parse_controller: MonoBehaviour {

	ParseObject test;
	string score = "";

	// Use this for initialization
	void Start () {

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
			int number = bigObject2.Get<int>("myNumber");
			string str = bigObject2.Get<string>("myString");
			Debug.Log ("Number: " + number);
			Debug.Log ("String: " + str);
		});
	}
}
