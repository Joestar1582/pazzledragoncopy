using UnityEngine;
using System.Collections;

public class TransEffect : MonoBehaviour {
	
	int state = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		switch(state)
		{
		// Preparation. 
		case 0:
			break;

		// Open. 
		case 1:
			if(gameObject.particleSystem.time > gameObject.particleSystem.duration / 2)
			{			
				gameObject.particleSystem.Pause();
				state = 2;
			}
			break;

		// Wait. 
		case 2:
			break;

		// Close. 
		case 3:
			gameObject.particleSystem.Play();
			state = 4;
			break;
			
		// End. 
		case 4:
			break;
		}	
	}
}
