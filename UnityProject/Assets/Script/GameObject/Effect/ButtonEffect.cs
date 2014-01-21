using UnityEngine;
using System.Collections;

public class ButtonEffect : MonoBehaviour {
	
//	bool	isActive = false;
	
	// Update is called once per frame
	void Update () {
		transform.localEulerAngles -= (Vector3.forward * 360.0f) * (Time.deltaTime * 0.5f);
	}
	
/*	void Init(){
		transform.localEulerAngles = Vector3.zero;
	}
	
	public void Enable(){
		Init();
		isActive = true;
		ParticleRenderer[] ren = GetComponentsInChildren<ParticleRenderer>();
		foreach(ParticleRenderer r in ren)
		{
			r.enabled = true;
		}
	}
	
	public void Disable(){
		isActive = false;
		ParticleRenderer[] ren = GetComponentsInChildren<ParticleRenderer>();
		foreach(ParticleRenderer r in ren)
		{
			r.enabled = false;
		}
	}*/
}
