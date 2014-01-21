using UnityEngine;
using System.Collections;

public class LineEffect : MonoBehaviour {
	
	public Vector3 targetPositionStart; 
	public Vector3 targetPositionEnd;
	public float	moveTime = 0.1f;

	Vector3 oldPos;
	int counter = 0;
	// Use this for initialization
	void Start () {
		oldPos = targetPositionEnd;
		gameObject.renderer.enabled = false;
		gameObject.GetComponent<TrailRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(oldPos != targetPositionEnd)
			gameObject.particleSystem.renderer.enabled = false;
		else
			gameObject.particleSystem.renderer.enabled = true;

		oldPos = targetPositionEnd;
		counter++;
		if(counter > 50)
			gameObject.GetComponent<TrailRenderer>().enabled = true;
	}
	
	void End()
	{
		iTween.Stop(gameObject);
	}
		
	public void MoveToTargetPositionEnd()
	{
//		gameObject.particleSystem.renderer.enabled = true;
		iTween.MoveTo(gameObject,iTween.Hash("position", targetPositionEnd,"time", moveTime,"oncomplete","MoveToTargetPositionStart","easetype",iTween.EaseType.linear,"islocal",true));

	}
	public void MoveToTargetPositionStart()
	{
//		gameObject.particleSystem.renderer.enabled = true;
		iTween.MoveTo(gameObject,iTween.Hash("position", targetPositionStart,"time", moveTime,"oncomplete","MoveToTargetPositionEnd","easetype",iTween.EaseType.linear,"islocal",true));		
		//gameObject.GetComponent<TrailRenderer>().enabled = true;
	}
	
}
