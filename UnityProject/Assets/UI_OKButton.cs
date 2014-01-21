using UnityEngine;
using System.Collections;

public class UI_OKButton : SingletonMonoBehaviour<UI_OKButton> {

	public	UI_Buttons			button;
	public	UI_Sprite_Fade[]	sprites;
	private	bool				isActive;
	
	public	void	On(){
		if(!isActive)
		{
			button.active = true;
			for(int i = 0; i<sprites.Length; i++)
			{
				sprites[i].FadeIn(0.5f);
			}
			
			isActive = true;
			
			iTweenEvent.GetEvent(GameObject.Find("UI_Select"), "FadeOut").Play();
		}
	}
	
	public	void	Off(){
		if(isActive)
		{
			button.active = false;
			for(int i = 0; i<sprites.Length; i++)
			{
				sprites[i].FadeOut(0.5f);
			}
			
			isActive = false;
		}
	}

}
