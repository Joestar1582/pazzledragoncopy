using UnityEngine;
using System.Collections;

public class UI_FilledSprite_Fade : MonoBehaviour {

	UIFilledSprite sprite;
	
	void Start(){
		sprite = GetComponent<UIFilledSprite>();
	}
	
	public void FadeIn(float time){
		Hashtable ht = new Hashtable();
		ht.Add("name", "LabelFadeIn");
		ht.Add("time", time);
		ht.Add("from", 0.0f);
		ht.Add("to"  , 1.0f);
		ht.Add("onupdate", "OnAlphaUpdate");
		iTween.ValueTo(gameObject, ht);
	}
	
	public void FadeOut(float time){
		Hashtable ht = new Hashtable();
		ht.Add("name", "LabelFadeIn");
		ht.Add("time", time);
		ht.Add("from", 1.0f);
		ht.Add("to"  , 0.0f);
		ht.Add("onupdate", "OnAlphaUpdate");
		iTween.ValueTo(gameObject, ht);
	}
	
	public void FadeIn(float time, float delay){
		Hashtable ht = new Hashtable();
		ht.Add("name", "LabelFadeIn");
		ht.Add("time", time);
		ht.Add("delay", delay);
		ht.Add("from", 0.0f);
		ht.Add("to"  , 1.0f);
		ht.Add("onupdate", "OnAlphaUpdate");
		iTween.ValueTo(gameObject, ht);
	}
	
	public void FadeOut(float time, float delay){
		Hashtable ht = new Hashtable();
		ht.Add("name", "LabelFadeOut");
		ht.Add("time", time);
		ht.Add("delay", delay);
		ht.Add("from", 1.0f);
		ht.Add("to"  , 0.0f);
		ht.Add("onupdate", "OnAlphaUpdate");
		iTween.ValueTo(gameObject, ht);
	}
	
	public void FadeIn(float time, float delay, string oncomplete, GameObject oncompletetarget){
		Hashtable ht = new Hashtable();
		ht.Add("name", "LabelFadeIn");
		ht.Add("time", time);
		ht.Add("delay", delay);
		ht.Add("from", 0.0f);
		ht.Add("to"  , 1.0f);
		ht.Add("oncomplete", oncomplete);
		ht.Add("oncompletetarget", oncompletetarget);
		ht.Add("onupdate", "OnAlphaUpdate");
		iTween.ValueTo(gameObject, ht);
	}
	
	public void FadeOut(float time, float delay, string oncomplete, GameObject oncompletetarget){
		Hashtable ht = new Hashtable();
		ht.Add("name", "LabelFadeOut");
		ht.Add("time", time);
		ht.Add("delay", delay);
		ht.Add("from", 1.0f);
		ht.Add("to"  , 0.0f);
		ht.Add("oncomplete", oncomplete);
		ht.Add("oncompletetarget", oncompletetarget);
		ht.Add("onupdate", "OnAlphaUpdate");
		iTween.ValueTo(gameObject, ht);
	}
	
	void OnAlphaUpdate(float alpha){
		sprite.alpha = alpha;
	}
	
}
