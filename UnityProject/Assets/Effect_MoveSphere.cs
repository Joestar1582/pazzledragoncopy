using UnityEngine;
using System.Collections;

public class Effect_MoveSphere : MonoBehaviour_Extends {
	
	private	Vector3	movedir = Vector3.zero;
	
	void Start(){
		transform.position = new Vector3(0,10000,0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition += (movedir * 750)*Time.deltaTime;
	}
	
	public	void	SetMove(Vector3 pos, Vector3 dir){
		transform.localPosition = pos;
		movedir = dir;
	}
}
