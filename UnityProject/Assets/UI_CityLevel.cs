using UnityEngine;
using System.Collections;

public class UI_CityLevel : SingletonMonoBehaviour<UI_CityLevel> {

	enum STATE{
		eCityLevel_Init = 0,
		eCityLevel_Wait,
		
		eUp_Init,
		eUp_Wait,
		
		eAllFadeOut,
		
		eEnumEnd
	}
	
	public	UI_Sprite_Fade	spFd_CityLebel;
	public	UI_Sprite_Fade	spFd_Up;
	private	STATE state = STATE.eEnumEnd;

	void Update () {
		switch(state)
		{
		case STATE.eCityLevel_Init:
			spFd_CityLebel.FadeIn(0.5f,0.5f,"OnComplete",gameObject);
			state++;
			break;
		case STATE.eCityLevel_Wait:
			break;
			
		case STATE.eUp_Init:
			spFd_Up.FadeOut(0.5f,0.0f);
			spFd_Up.transform.localScale = new Vector3(1000,120,1);
			spFd_CityLebel.transform.localScale = new Vector3(900,108,1); 
			iTweenEvent.GetEvent(spFd_Up.gameObject,"Scaling").Play();
			iTweenEvent.GetEvent(spFd_CityLebel.gameObject,"Scaling_1").Play();
			state++;
			break;
		case STATE.eUp_Wait:
			state++;
			break;
			
		case STATE.eAllFadeOut:
			spFd_CityLebel.FadeOut(0.5f, 1.0f);
			state++;
			break;
			
		case STATE.eEnumEnd:
			break;
		}
			
	}
	
	public	void Play(){
		if(state == STATE.eEnumEnd)
			state = STATE.eCityLevel_Init;
	}
	
	void OnComplete(){
		state++;
	}
	
	void SetScaling_2(){
		iTweenEvent.GetEvent(spFd_CityLebel.gameObject, "Scaling_2").Play();
	}
}
