using UnityEngine;
using System.Collections;

public class OpenningText : MonoBehaviour {
	public string[] text;
	public GameObject state;
		int currentNo = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(currentNo < text.Length)
		{
			TextMesh tm = (TextMesh)gameObject.GetComponent("TextMesh");
			tm.text = text[currentNo];
		}

	}
	void OpenText()
	{
		iTweenEvent.GetEvent(gameObject,"OpenTextTween").Play();
	}
	void CloseText()
	{
		iTweenEvent.GetEvent(gameObject,"CloseTextTween").Play();		
	}
	void NextText()
	{
		currentNo++;
		if(currentNo >= text.Length)
		{
			state.SendMessage("EndOpenning");
			return;
		}
		else
			OpenText();
	}
	void IdleText()
	{
		iTweenEvent.GetEvent(gameObject,"IdleTextTween").Play();
	}
	
}
