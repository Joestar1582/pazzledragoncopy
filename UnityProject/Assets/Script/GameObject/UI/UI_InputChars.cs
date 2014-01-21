using UnityEngine;
using System.Collections;

public class UI_InputChars : MonoBehaviour {
	
	UILabel[]		labels;
	UI_Buttons[]	buttons;
	
	void Start(){
		labels  = GetComponentsInChildren<UILabel>();
		buttons = GetComponentsInChildren<UI_Buttons>();
		OffDisp();
	}
	
	public void FadeIn(float time, string oncomplete, GameObject oncompletetarget){
		Hashtable ht = new Hashtable();
		ht.Add("name", "LabelFadeIn");
		ht.Add("time", time);
		ht.Add("from", 0.0f);
		ht.Add("to"  , 1.0f);
		ht.Add("oncomplete", oncomplete);
		ht.Add("oncompletetarget", oncompletetarget);
		ht.Add("onupdate", "OnAlphaUpdate");
		iTween.ValueTo(gameObject, ht);
	}
	
	public void FadeOut(float time, string oncomplete, GameObject oncompletetarget){
		Hashtable ht = new Hashtable();
		ht.Add("name", "LabelFadeOut");
		ht.Add("time", time);
		ht.Add("from", 1.0f);
		ht.Add("to"  , 0.0f);
		ht.Add("oncomplete", oncomplete);
		ht.Add("oncompletetarget", oncompletetarget);
		ht.Add("onupdate", "OnAlphaUpdate");
		iTween.ValueTo(gameObject, ht);
	}
	
	public void OffDisp(){
		OnAlphaUpdate(0);
/*		foreach(UILabel label in labels){
			label.enabled = false;
			// cut off input
		}*/
	}
	
	public void ButtonActive(bool b){
		foreach(UI_Buttons button in buttons){
			button.active = b;
			// cut off input
		}
	}
	
	void OnAlphaUpdate(float alpha){
		foreach(UILabel label in labels){
			label.alpha = alpha;
		}
		
		if(alpha == 1.0f)
		{
			ButtonActive(true);
		}else if(buttons[0].active){
			ButtonActive(false);
		}
	}
	
}
