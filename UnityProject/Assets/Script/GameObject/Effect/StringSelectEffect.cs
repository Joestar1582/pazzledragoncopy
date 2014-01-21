using UnityEngine;
using System.Collections;

public class StringSelectEffect : MonoBehaviour {

	public	Vector3				target = new Vector3(0,0,0);
	public	GameObject			targetObject;
	
	bool Checked = false;			

	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(particleSystem.isPlaying && gameObject.particleSystem.time > gameObject.particleSystem.duration / 2 && !Checked)
		{			
			ParticleSystem.Particle[] ParticleList = new ParticleSystem.Particle[gameObject.particleSystem.particleCount];
			gameObject.particleSystem.GetParticles(ParticleList);
			gameObject.particleSystem.emissionRate = 0;
			gameObject.particleSystem.startSpeed = 0;
			for(int i = 0; i < ParticleList.Length; ++i)
			{
				ParticleList[i].velocity = (target - ParticleList[i].position) / ParticleList[i].lifetime;
			}
			gameObject.particleSystem.SetParticles(ParticleList,gameObject.particleSystem.particleCount);
			Checked = true;
		}
	}
	
	public void Play(){
		Checked = false;
		particleSystem.Play();
		target = targetObject.transform.position;
		//target.x += targetObject.transform.lossyScale.x * (targetObject.GetComponent<UILabel>().text.Length/2);
	}
}
