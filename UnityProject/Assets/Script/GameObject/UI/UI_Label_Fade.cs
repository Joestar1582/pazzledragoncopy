using UnityEngine;
using System.Collections;

public class UI_Label_Fade : MonoBehaviour {
	
	UILabel label;
	
	void Start(){
		label = GetComponent<UILabel>();
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
		label.alpha = alpha;
		if(label.effectStyle != UILabel.Effect.None)
			label.effectColor = new Color(label.effectColor.r, label.effectColor.g, label.effectColor.b, alpha);
	}
	
	void OnDestroy(){
//		iTween.StopByName("LabelFadeIn");
//		iTween.StopByName("LabelFadeOut");
	}
}
