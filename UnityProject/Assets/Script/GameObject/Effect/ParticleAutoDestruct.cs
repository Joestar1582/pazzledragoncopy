using UnityEngine;
using System.Collections;

public class ParticleAutoDestruct : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.particleSystem.IsAlive() == false)
			Destroy(gameObject);
	}
}
