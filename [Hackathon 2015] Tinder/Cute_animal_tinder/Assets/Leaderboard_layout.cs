using UnityEngine;
using System.Collections;

public class Leaderboard_layout : MonoBehaviour {

	public GUIStyle bg;
	public GUIStyle nav;
	public GUIStyle title;
	public GUIStyle pic;
	public GUIStyle Leader_title;
	public GUIStyle chick_icon;
	public Camera mainCam;
	public GUIStyle icon;

	int count=0;
	int rank=0;
	Texture2D[] rank_pic_server;

	// Use this for initialization
	void Start () 
	{
		rank_pic_server = parse_controller.rank_pic_arr;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void OnGUI()
	{
		GUI.Box(new Rect(0,0,Screen.width, Screen.height), "", bg);
		GUI.Label(new Rect(0, 0, Screen.width, Screen.height/10), "", nav);
		GUI.Box(new Rect(Screen.width/15, 0, Screen.width/8, Screen.width/8), "", chick_icon);
		GUI.Box(new Rect(Screen.width/15 + Screen.width/8 + Screen.width/25, Screen.height/40, (float)(Screen.width*0.75), (float)(Screen.height/20)), "", title);
		GUI.Box(new Rect(0, Screen.height/8, (float)(Screen.width*0.75), (float)(Screen.height/20)), "TOP 10 CAT!", Leader_title);

		pic.normal.background = parse_controller.rank_pic_arr [count];

		//pic ==> 
		GUI.Box(new Rect(0, Screen.height/4, Screen.width/2, Screen.width/2), "" + parse_controller.rank_id_arr[count], pic);
		

		icon.fontSize = Screen.width / 10;
		if (count > 0) {
			if (GUI.Button (new Rect (Screen.width / 8 - Screen.width / 16, Screen.height / 2, Screen.width / 8, Screen.width / 8), "<", icon)) {
				count--;
				Debug.Log ("count: " + count);
			}
		}
		if (count < 10) {
			if (GUI.Button (new Rect ((float)(Screen.width * 0.875) - Screen.width / 16, Screen.height / 2, Screen.width / 8, Screen.width / 8), ">", icon)) {
				count++;
				Debug.Log ("count: " + count);
			}
		}

	}
}