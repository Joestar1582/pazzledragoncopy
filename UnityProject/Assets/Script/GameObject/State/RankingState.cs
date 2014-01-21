using UnityEngine;
using System.Collections;

public class RankingState : StateBase {
	
	// 状態管理
	enum STATE{
		eEnter_Init = 0,
		eEnter_Wait,
		
		eMain_Init,
		eMain_Wait,
		
		eExit_Init,
		eExit_Wait,
		
		eChangeState
	}
	
	private STATE		state = STATE.eEnter_Init;	//!< 状態管理用
	public	UI_Ranking	uiRanking;
	
	void Start(){
		gameObject.name = "State_Ranking";
	}
	
	public override void Exec ()
	{
		switch(state)
		{
			// タイトル　状態入り
		case STATE.eEnter_Init:
			//Debug.Log("Enter_Ranking");
			//fade.Tween_FadeIn(gameObject,"OnFadeComplete",0.5f);
			FadeIn(1.5f);
			uiRanking.Init();
			uiRanking.btMenu.active = true;
			state++;
			break;
		case STATE.eEnter_Wait:
			break;
			
			// タイトル　入力待ち
		case STATE.eMain_Init:
			state++;
			break;
		case STATE.eMain_Wait:
			break;
			
			// タイトル　状態離脱
		case STATE.eExit_Init:
			FadeOut(1.5f);
			uiRanking.btMenu.active = false;
			state++;
			break;
		case STATE.eExit_Wait:
			break;
			
			// 次の状態へ
		case STATE.eChangeState:
			StateController.Instance.ChangeState(E_STATE.Title);
			break;
		}
	}
	
	void OnMenuButtonPressed()
	{
		state = STATE.eExit_Init;
	}
	
	protected override void OnCompleteFade ()
	{
		state++;
	}
	
	protected override void OnDestruct ()
	{
		
	}
}
