using UnityEngine;
using System.Collections;

public class UI_WaveCount : SingletonMonoBehaviour<UI_WaveCount> {
	
	enum STATE 
	{
		eLbWaveFadeIn_Init = 0,
		eLbWaveFadeIn_Wait,
		
		eLbNumFadeIn_Init,
		eLbNumFadeIn_Wait,
		
		eSpReadyFadeIn_Init,
		eSpReadyFadeIn_Wait,
		
		eSpReadyFadeOut,
		
		eEnumEnd
	}
	

	public	UI_Sprite_Fade	spFd_ready;
	
	public	UI_Sprite_Fade	spFd_wave;
	public	UI_Sprite_Fade	spFd_num_oneDigit;
	public	UI_Sprite_Fade	spFd_num_tenDigit;
	
	private	int				count = 0;
	
	private	STATE	state = STATE.eEnumEnd;
	
	void	Update(){
		switch(state)
		{
		case STATE.eLbWaveFadeIn_Init:
			count++;
			spFd_wave.FadeIn(0.5f, 0.0f, "OnComplete", gameObject);
			state++;
			break;
		case STATE.eLbWaveFadeIn_Wait:
			break;
			
		case STATE.eLbNumFadeIn_Init:
			if(count > 9 && count < 100)
			{
				int ten = count/10;
				UISprite spTen = spFd_num_tenDigit.gameObject.GetComponent<UISprite>();

				spTen.spriteName = ten.ToString();
				spTen.MakePixelPerfect();
				
				Vector3 scaleTen = spTen.transform.localScale;
				scaleTen.x *= 1.5f;
				scaleTen.y *= 1.5f;
				spTen.transform.localScale = scaleTen;
				spFd_num_tenDigit.FadeIn(0.5f, 0.0f);
			}
			
			int one = count - count/10*10;
			UISprite spOne = spFd_num_oneDigit.gameObject.GetComponent<UISprite>();
			
			spOne.spriteName = one.ToString();
			spOne.MakePixelPerfect();
			
			Vector3 scaleOne = spOne.transform.localScale;
			scaleOne.x *= 1.5f;
			scaleOne.y *= 1.5f;
			spOne.transform.localScale = scaleOne;
			
			spFd_num_oneDigit.FadeIn(0.5f, 0.0f, "OnComplete", gameObject);
			
			state++;
			break;
		case STATE.eLbNumFadeIn_Wait:
			break;
			
		case STATE.eSpReadyFadeIn_Init:
			spFd_wave.FadeOut(0.4f, 0.0f);
			if(count > 9 && count < 100)
			{
				spFd_num_tenDigit.FadeOut(0.4f,0.0f);
			}
			spFd_num_oneDigit.FadeOut(0.4f,0.0f);
			spFd_ready.FadeIn(0.5f, 0.3f , "OnComplete", gameObject);
			state++;
			break;
		case STATE.eSpReadyFadeIn_Wait:
			break;
			
		case STATE.eSpReadyFadeOut:
			spFd_ready.FadeOut(0.5f, 0.5f);
			state++;
			break;
			
		case STATE.eEnumEnd:
			break;
		}
	}
	
	void OnComplete(){
		state++;
	}
	
	public	void Play(){
		if(state == STATE.eEnumEnd)
			state = STATE.eLbWaveFadeIn_Init;
	}
}
