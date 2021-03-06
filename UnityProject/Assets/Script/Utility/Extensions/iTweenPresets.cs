using UnityEngine;
using System.Collections;

//========================================================
/*!
 *	@brief		iTweenのよく使うパターンをまとめたもの.
 *
 *	@date		2013/05/02
 *	@author		Daisuke Kojima
 */
 //=======================================================
public static class iTweenPresets {
	
	// ********* FadeIn/Out **********
	
	/*! フェードイン(時間指定のみ) */
	public static void FadeIn(GameObject obj, float time)
	{
		Hashtable ht = new Hashtable();
		
		ht.Add("name", "FadeIn");
		
		Fade(obj,time,1.0f,ht);
	}
	
	/*! フェードイン(oncompleteを指定) */
	public static void FadeIn(GameObject obj, float time, string oncomplete)
	{
		Hashtable ht = new Hashtable();
		
		ht.Add("name", "FadeIn");
		
		Fade(obj, time, 1.0f, oncomplete, ht);
	}
	
	/*! フェードイン(oncomplete, oncompletetargetを指定) */
	public static void FadeIn(GameObject obj, float time, string oncomplete, GameObject oncompletetarget)
	{
		Hashtable ht = new Hashtable();
		
		ht.Add("name", "FadeIn");
		
		Fade(obj, time, 1.0f, oncomplete, oncompletetarget, ht);
	}
	
	/*! フェードアウト(時間指定のみ) */
	public static void FadeOut(GameObject obj, float time)
	{
		Hashtable ht = new Hashtable();
		
		ht.Add("name", "FadeOut");
		
		Fade(obj,time,0.0f,ht);
	}
	
	/*! フェードアウト(oncompleteを指定) */
	public static void FadeOut(GameObject obj, float time, string oncomplete)
	{
		Hashtable ht = new Hashtable();
		
		ht.Add("name", "FadeOut");
		
		Fade(obj, time, 0.0f, oncomplete, ht);
	}
	
	/*! フェードアウト(oncomplete, oncompletetargetを指定) */
	public static void FadeOut(GameObject obj, float time, string oncomplete, GameObject oncompletetarget)
	{
		Hashtable ht = new Hashtable();
		
		ht.Add("name", "FadeOut");
		
		Fade(obj, time, 0.0f, oncomplete, oncompletetarget, ht);
	}
	
	private static void Fade(GameObject obj, float time, float alpha, string oncomplete, Hashtable ht)
	{
		ht.Add("oncomplete", oncomplete);
		
		Fade(obj,time,alpha,ht);
	}
	
	private static void Fade(GameObject obj, float time, float alpha, string oncomplete, GameObject oncompletetarget, Hashtable ht)
	{
		ht.Add("oncomplete", oncomplete);
		ht.Add("oncompletetarget", oncompletetarget);
		
		Fade(obj,time,alpha,ht);
	}
	
	private static void Fade(GameObject obj, float time, float alpha, Hashtable ht)
	{
		Color color  = obj.renderer.material.color;
		color.a = alpha;
				
		ht.Add("color", color);
		ht.Add("time", time);
		
		iTween.ColorTo(obj,ht);
	}
	
	
}