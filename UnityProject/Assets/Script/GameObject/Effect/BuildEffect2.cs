using UnityEngine;
using System.Collections;

public class BuildEffect2 : MonoBehaviour {
	
	public float	rotateSpeed = 2.0f;
	public float 	spreadTime 	= 3.0f;
	public Vector3 	maxRange; 
	private float 	time;
	public GameObject	CubeEffect;
	public GameObject	SparkEffect;
	public GameObject	LineEffect;
	// Use this for initialization
	void Start () {
		time = spreadTime;
//		foreach (Transform child in transform) {
//			iTween.MoveBy(child.gameObject,iTween.Hash("x",maxRange.x,"y",maxRange.y,"time",spreadTime, "easetype", iTween.EaseType.easeInOutCubic));
//		}
		
		iTween.MoveBy(CubeEffect,iTween.Hash("x",maxRange.x,"time",spreadTime, "easetype", iTween.EaseType.easeInOutCubic));
		iTween.MoveBy(SparkEffect,iTween.Hash("x",maxRange.x,"time",spreadTime, "easetype", iTween.EaseType.easeInOutCubic));
		iTween.MoveBy(LineEffect,iTween.Hash("x",maxRange.x,"time",spreadTime, "easetype", iTween.EaseType.easeInOutCubic));
		iTween.MoveBy(gameObject,iTween.Hash("y",maxRange.y,"time",spreadTime, "easetype", iTween.EaseType.easeInOutCubic));

	}
	
	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;
		if(time <= 0)
		{
			foreach (Transform child in SparkEffect.transform) 
			{
				child.particleEmitter.emit = false;
			}
			
		}
		else
			gameObject.transform.Rotate(0,rotateSpeed,0);

		if(!CubeEffect.particleSystem.IsAlive())
			Destroy(gameObject);
	}
}
