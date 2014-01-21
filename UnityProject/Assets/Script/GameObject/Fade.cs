using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour_Extends {
	
	public void Tween_FadeIn(GameObject oncompletetarget, string oncomplete,float time){
//		iTween.StopByName("FadeOut");
		
		Hashtable ht = new Hashtable();
		
		ht.Add("name", "FadeIn");
		ht.Add("oncompletetarget", oncompletetarget);
		ht.Add("oncomplete", oncomplete);
		ht.Add("time", time);
		ht.Add("color", new Color(	renderer.material.color.r, renderer.material.color.g, renderer.material.color.b,
									0.0f));
		
		iTween.ColorTo(gameObject, ht);
	}
	
	public void Tween_FadeOut(GameObject oncompletetarget, string oncomplete,float time){
//		iTween.StopByName("FadeIn");
		
		Hashtable ht = new Hashtable();
		
		ht.Add("name", "FadeOut");
		ht.Add("oncompletetarget", oncompletetarget);
		ht.Add("oncomplete", oncomplete);
		ht.Add("time", time);
		ht.Add("color", new Color(	renderer.material.color.r, renderer.material.color.g, renderer.material.color.b,
									1.0f));
		
		iTween.ColorTo(gameObject, ht);
	}
	
	public void Tween_FadeOut(GameObject oncompletetarget, string oncomplete, Color color,float time){
//		iTween.StopByName("FadeIn");
		
		Hashtable ht = new Hashtable();
		
		ht.Add("name", "FadeOut");
		ht.Add("oncompletetarget", oncompletetarget);
		ht.Add("oncomplete", oncomplete);
		ht.Add("time", time);
		ht.Add("color", color);
		
		iTween.ColorTo(gameObject, ht);
	}
	
	void OnDestroy(){
		//iTween.StopByName("FadeOut");
		//iTween.StopByName("FadeIn");
	}
}
