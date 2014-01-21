using UnityEngine;
using System.Collections;

public class StateBase : MonoBehaviour_Extends {
	
	private bool	isQuit = false;
	public	Fade	fade;
	
	protected virtual void Start(){
		//fade = GetComponentInChildren<Fade>();
	}
	
	public virtual void Exec () {
	
	}
	
	protected virtual void OnDestruct(){
		
	}
	
	protected void FadeIn(float time){
		fade.Tween_FadeIn(gameObject, "OnCompleteFade", time);
	}
	
	protected void FadeOut(float time){
		fade.Tween_FadeOut(gameObject, "OnCompleteFade", time);
	}
	
	protected void FadeOut(Color color, float time){
		fade.Tween_FadeOut(gameObject, "OnCompleteFade", color, time);
	}
	
	protected virtual void OnCompleteFade(){
		
	}
	
	void OnApplicationQuit(){
		isQuit = true;
	}
	
	void OnDestroy(){
		if(!isQuit)
		{
			OnDestruct();
		}
	}
}
