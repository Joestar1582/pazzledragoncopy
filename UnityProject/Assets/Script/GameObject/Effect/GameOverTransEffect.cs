using UnityEngine;
using System.Collections;

public class GameOverTransEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		particleSystem.renderer.material.SetTexture("ParticleTexture",Resources.Load("Texture/rendertexture") as Texture2D);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
