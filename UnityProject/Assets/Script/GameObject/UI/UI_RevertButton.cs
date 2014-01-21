using UnityEngine;
using System.Collections;

public class UI_RevertButton : SingletonMonoBehaviour<UI_RevertButton> {

	public	UI_Buttons			button;
	public	UI_Sprite_Fade[]	sprites;
	private	bool				isActive;
	private	bool				isPermitted;
	
	void Update(){
		if(isPermitted){
			if(OrigamiController.Instance.GetRevertFlg()){
				Permit();
			}
		}
	}
	
	public	void	On(){
		isPermitted = true;
	}
	
	private	void	Permit(){
		if(!isActive)
		{
			button.active = true;
			for(int i = 0; i<sprites.Length; i++)
			{
				sprites[i].FadeIn(0.5f);
			}
			
			isActive = true;
		}
	}
	
	public	void	Off(){
		isPermitted = false;
		
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
