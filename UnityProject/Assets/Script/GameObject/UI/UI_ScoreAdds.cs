using UnityEngine;
using System.Collections;

public class UI_ScoreAdds : SingletonMonoBehaviour<UI_ScoreAdds> {
	
	private	UILabel	label;
	
	void Start(){
		label = gameObject.GetComponent<UILabel>();
	}
	
	public	void	SetScore(int score){
		label.text = "+" + score.ToString();
		Tween();
		Tween_Fade();
	}
	
	private	void	Tween(){
		gameObject.transform.localPosition = new Vector3(transform.localPosition.x, 260, transform.localPosition.z);
		Hashtable ht = new Hashtable();
		ht.Add("name", "scadd");
		ht.Add("time", 1.0f);
		ht.Add("position", gameObject.transform.localPosition + new Vector3(0,50,0));
//		ht.Add("amount", Vector3.up * 0.05f);
//		ht.Add("y", 0.025f);
		ht.Add("islocal", true);
		iTween.MoveTo(gameObject,ht);
	}
	
	private	void	Tween_Fade(){
		label.alpha = 1.0f;
//		label.effectColor.SetAlpha(0.3f);
		
		Hashtable ht = new Hashtable();
		
		ht.Add("from", 1.0f);
		ht.Add("to", 0.0f);
		ht.Add("time", 1.0f);
		ht.Add("onupdate", "UpdateHandler_Fade");
		
		iTween.ValueTo(gameObject, ht);
	}
	
	private	void	UpdateHandler_Fade(float arg){
		label.alpha = arg;
//		label.effectColor.SetAlpha(arg * 0.3f);
	}
}
