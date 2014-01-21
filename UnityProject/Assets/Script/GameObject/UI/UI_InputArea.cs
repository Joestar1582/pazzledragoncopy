using UnityEngine;
using System.Collections;

public class UI_InputArea : MonoBehaviour {
	
	UILabel label;
	
	public string text{
		get{return label.text;}
	}
	
	// Use this for initialization
	void Start () {
		label = GetComponent<UILabel>();
		label.text = "";
	}
	
	public void AddString(string s){
		Debug.Log("texlen:"+label.text.Length);
		if(label.text.Length < 3)
		{
			label.text += s;
		}else{
			label.text = label.text.Substring(1) + s;
		}
	}
	
}
