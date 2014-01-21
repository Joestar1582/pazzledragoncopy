using UnityEngine;
using System.Collections;

public class ConvergenceEffect : MonoBehaviour {
	public float				distance = 10;
	
	Vector3[]					ParticlePositionList;
	bool Checked = false;					
		
	
	// Use this for initialization
	void Start () {
		gameObject.particleSystem.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.particleSystem.particleCount > gameObject.particleSystem.emissionRate - 5 && !Checked)
		{
			gameObject.particleSystem.renderer.enabled = true;

			
			ParticleSystem.Particle[] ParticleList = new ParticleSystem.Particle[gameObject.particleSystem.particleCount];
			ParticlePositionList = new Vector3[gameObject.particleSystem.particleCount];
			gameObject.particleSystem.GetParticles(ParticleList);
			for(int i = 0; i < ParticleList.Length; ++i)
			{
				ParticlePositionList[i] = ParticleList[i].position;
				// 適当な位置にパーティクルを配置.
				ParticleList[i].position += (ParticleList[i].position - gameObject.transform.position) * distance;
				// LifeTimeに応じた速度を設定.
				ParticleList[i].lifetime /= 2;
				ParticleList[i].velocity = (ParticlePositionList[i] - ParticleList[i].position) / ParticleList[i].lifetime;
			}
			gameObject.particleSystem.SetParticles(ParticleList,gameObject.particleSystem.particleCount);
			Checked = true;
		}
	}
}
