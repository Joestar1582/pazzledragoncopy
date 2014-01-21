using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public enum STATE{
		TUTORIAL,
		GAME,
	}
	
	private STATE State = STATE.TUTORIAL;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
