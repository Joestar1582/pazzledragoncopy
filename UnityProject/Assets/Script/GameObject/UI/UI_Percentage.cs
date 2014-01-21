using UnityEngine;
using System.Collections;

public class UI_Percentage : MonoBehaviour_Extends {
	
	UILabel label;
	float	target = 0.0f;
	float	now = 0.0f;
	
	void Start () {
		label = GetComponent<UILabel>();
		label.alpha = 0;
	}	

	void Update () {
		if(target > now)
		{
			now += Time.deltaTime * 120.0f;
			label.text = StaticMath.ToRoundDown(now, 2).ToString() + "%";
			label.color = new Color(0.9f, now*0.01f, 1 - now*0.01f, 0.9f);
		}else if(target < now)
		{
			now = target;
			label.text = StaticMath.ToRoundDown(now, 2).ToString() + "%";
		}else if(target == now)
		{
			label.alpha -= Time.deltaTime;
		}
	}
	
	public void Activate(float per){
		now = 0;
		target = per;
		label.alpha = 1;
		label.text = StaticMath.ToRoundDown(now, 2).ToString() + "%";
	}
}
