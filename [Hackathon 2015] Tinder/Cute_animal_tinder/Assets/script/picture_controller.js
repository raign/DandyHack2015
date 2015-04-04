#pragma strict

var pic_one: Sprite;
var pic_two: Sprite;

var pic_control: SpriteRenderer;
var count:int =0;


function Start () 
{
		pic_control = GetComponent(SpriteRenderer);
}

function Update () 
{
	picture_control();
}

function picture_control()
{
	
	/*
	if(count==1)
	{
		pic_control.sprite = pic_one;
	}
	
	if(count==2)
	{
		pic_control.sprite = pic_two;
	}
	*/
}