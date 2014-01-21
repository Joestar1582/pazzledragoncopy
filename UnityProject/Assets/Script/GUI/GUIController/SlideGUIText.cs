using UnityEngine;
using System.Collections;

public class SlideGUIText : GUITextBase {
	
	public Vector2	InitPos;
	public Vector2	BasePos;
	
	// Use this for initialization
	void Start () {
		SetPos( InitPos );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// スライドさせる
	public void Slide ( float t ) {
		SetPos( Vector2.Lerp( InitPos, BasePos, t ) );
	}
}
