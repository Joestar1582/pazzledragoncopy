using UnityEngine;
using System.Collections;

//===========================================
/*!
 *	@brief	タイトルクラス
 *
 *	@date	2013/10/21
 *	@author	Daisuke Kojima
 */
//===========================================
public class TitleState : StateBase {
	
	// 状態管理.
	enum STATE{
		eEnter_Init = 0,
		eEnter_Wait,
		
		eMain_Init,
		eMain_Wait,
		
		eExit_Init,
		eExit_Wait,
		
		eChangeState
	}
	
	private STATE	state = STATE.eEnter_Init;	//!< 状態管理用.
	private E_STATE	nextState;
	
	
	//! 初期処理
	protected override void Start(){
		base.Start();
		gameObject.name = "State_Title";
		RenderSettings.fogEndDistance = 150;
	}
	
	
	//! 実行.
	public override void Exec ()
	{
		switch(state)
		{
			// タイトル　状態入り.
		case STATE.eEnter_Init:
			Debug.Log("Enter_Title");
			CRI_SoundManager_2D.Instance.PlayBGM(BGM_ID.TITLE);
			Game_CityLayer.Instance.InitCityLayer();
			FadeIn(1.5f);
			state++;
			break;
		case STATE.eEnter_Wait:
			break;
			
			// タイトル　入力待ち.
		case STATE.eMain_Init:
			state++;
			break;
		case STATE.eMain_Wait:
			break;
			
			// タイトル　状態離脱.
		case STATE.eExit_Init:
			FadeOut(1.5f);
			state++;
			break;
		case STATE.eExit_Wait:
			break;
			
			// 次の状態へ.
		case STATE.eChangeState:
			StateController.Instance.ChangeState(nextState);
			break;
		}
	}
	
	
	//! デストラクタ.
	protected override void OnDestruct ()
	{
		
	}
	
	private void OnStartPressed(){
		//nextState = E_STATE.Tutorial;
		nextState = E_STATE.Openning;
		state = STATE.eExit_Init;
	}
	
	private void OnRankingPressed(){
		nextState = E_STATE.Ranking;
		state = STATE.eExit_Init;
	}
	
	override protected void OnCompleteFade(){
		state++;
	}
}
