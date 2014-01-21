using UnityEngine;
using System.Collections;

public class UI_Combo : SingletonMonoBehaviour<UI_Combo> {
	
	private	UILabel	label;
	
	void Start(){
		label = gameObject.GetComponent<UILabel>();
	}
	
	public	void	SetCombo(int combo){
		label.text = combo.ToString() + " combo";
		Tween();
		Tween_Fade();
	}
	
	private	void	Tween(){
		gameObject.transform.SetLocalPosY(200);
		Hashtable ht = new Hashtable();
		ht.Add("name", "combo");
		ht.Add("time", 0.6f);
		ht.Add("amount", Vector3.up * 0.075f);
//		ht.Add("y", 0.05f);
		ht.Add("islocal", true);
		iTween.MoveAdd(gameObject,ht);
	}
	
	private	void	Tween_Fade(){
		label.alpha = 1.0f;
//		label.effectColor.SetAlpha(0.3f);
		
		Hashtable ht = new Hashtable();
		
		ht.Add("from", 1.0f);
		ht.Add("to", 0.0f);
		ht.Add("time", 2.0f);
		ht.Add("onupdate", "UpdateHandler_Fade");
		
		iTween.ValueTo(gameObject, ht);
	}
	
	private	void	UpdateHandler_Fade(float arg){
		label.alpha = arg;
//		label.effectColor.SetAlpha(arg * 0.3f);
	}
}
