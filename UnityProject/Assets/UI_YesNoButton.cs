using UnityEngine;
using System.Collections;

public class UI_YesNoButton : MonoBehaviour {
	
	public TutorialText tutorialText;
	
	public UI_Sprite_Fade[] spFade_yes;
	public UI_Sprite_Fade[] spFade_no;
	
	void Start(){
		for(int i=0; i<2; i++)
		{
			spFade_yes[i].FadeIn(0.5f);
			spFade_no[i].FadeIn(0.5f);
		}
		
		spFade_yes[1].GetComponent<UI_Buttons>().active = spFade_no[1].GetComponent<UI_Buttons>().active = true;
	}
	
	void OnYesButtonPressed()
	{
		spFade_yes[0].FadeOut(0.5f);
		spFade_yes[1].FadeOut(0.5f);
		spFade_no[0].FadeOut(0.5f);
		spFade_no[1].FadeOut(0.5f);
		tutorialText.OnYesButtonPressed();
		
		spFade_yes[1].GetComponent<UI_Buttons>().active = spFade_no[1].GetComponent<UI_Buttons>().active = false;
	}
	
	void OnNoButtonPressed()
	{
		spFade_yes[0].FadeOut(0.5f);
		spFade_yes[1].FadeOut(0.5f);
		spFade_no[0].FadeOut(0.5f);
		spFade_no[1].FadeOut(0.5f);
		tutorialText.OnNoButtonPressed();
		
		spFade_yes[1].GetComponent<UI_Buttons>().active = spFade_no[1].GetComponent<UI_Buttons>().active = false;
	}
	
}
