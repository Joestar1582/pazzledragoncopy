using UnityEngine;
using System.Collections;

public class UI_ResultManager : SingletonMonoBehaviour<UI_ResultManager>{
	
	public	UI_Label_Fade		lbFdScore;
	public	UI_Sprite_Fade		spFdNewRecord;
	public	UI_InputChars		inputChars;
	public	UI_InputArea		inputArea;
	public	UISprite			spNextButton;
	public	UISprite			spOK;
	public	Effect_MoveSphere	sphere;
	
	public	bool SetScore(){
		lbFdScore.gameObject.GetComponent<UILabel>().text = "Score: " + StateController.Instance.score.ToString();
		
		for(int i=0; i<MasterData.Instance.rankingData.Length; i++)
		{
			if(StateController.Instance.score >= MasterData.Instance.rankingData[i].score)
				return true;
		}
		
		return false;
	}
}
