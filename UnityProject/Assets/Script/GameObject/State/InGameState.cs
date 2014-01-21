using UnityEngine;
using System.Collections;

public class InGameState : StateBase {
	
	
	// 状態管理
	enum STATE{
		eEnter_Init = 0,
		eEnter_Wait,
		
		eMain_Init,
		eMain_Wait,
		
		eGameOver_Init,
		eGameOver_Wait,
		
		eGameClear_Init,
		eGameClear_Wait,
		
		eExit_Init,
		eExit_Wait,
		
		eChangeState_Bridge,
		
		eChangeState_ToGameOver,
		eChangeState_ToResult,
		eChangeState_ToTitle
	}
	
	private STATE	state		= STATE.eEnter_Init;	//!< 状態管理用
	private	STATE	nextState	= STATE.eEnter_Init;
	
	protected override void Start ()
	{
		RenderSettings.fogEndDistance = 80;
		base.Start ();
	}
	
	public override void Exec ()
	{
		
		switch(state)
		{			
			// インゲーム　状態入り
		case STATE.eEnter_Init:
			CRI_SoundManager_2D.Instance.PlayBGM(BGM_ID.INGAME);

			Debug.Log("Enter_InGame");
			Game_CityLayer.Instance.CityLayerEnable(0,true);
//			UI_TimeCounter.Instance.SetEnable(false);
//			UI_ScoreCounter.Instance.SetEnable(false);
			UI_TentionGauge.Instance.SetEnable(false);
			FadeIn(1.5f);			
			WakuGenerator.Instance.Init();
			
			state++;
			break;
		case STATE.eEnter_Wait:
			Debug.Log("enter_wait");
			break;
			
			// インゲーム　入力待ち
		case STATE.eMain_Init:
			Debug.Log("main_init");
			UI_TimeCounter.Instance.SetEnable(true);
			UI_ScoreCounter.Instance.SetEnable(true);
			UI_TentionGauge.Instance.SetEnable(true);

						WakuGenerator.Instance.Play();
			state++;
			break;
		case STATE.eMain_Wait:
//			UI_TimeCounter.Instance.Exec();
/*			if(UI_TimeCounter.Instance.total > 480.0f)
			{
				WakuGenerator.Instance.Stop();
			}*/
			if(UI_TentionGauge.Instance.isDead())
			{
				if(WakuGenerator.Instance.Stop())
					state = STATE.eGameClear_Init;
			}
/*			if(UI_TimeCounter.Instance.isLastWave)
			{
				if(WakuGenerator.Instance.Stop())
					state = STATE.eGameClear_Init;
			}*/
			break;
			
		case STATE.eGameOver_Init:
			//UI_TimeCounter.Instance.StopAllTween();
			nextState = STATE.eChangeState_ToGameOver;
			state++;
			break;
		case STATE.eGameOver_Wait:
			state = STATE.eExit_Init;
			break;
			
		case STATE.eGameClear_Init:
			//UI_TimeCounter.Instance.StopAllTween();
			UI_ScoreCounter.Instance.SetEnable(false);
			UI_TentionGauge.Instance.SetEnable(false);
			nextState = STATE.eChangeState_ToResult;
			StateController.Instance.score = UI_ScoreCounter.Instance.Score;
			state++;
			break;
		case STATE.eGameClear_Wait:
//			if(WakuGenerator.Instance.Stop())
//			{
				state = STATE.eExit_Init;
//			}
			break;
			
			// インゲーム　状態離脱
		case STATE.eExit_Init:			
			if(nextState == STATE.eChangeState_ToResult)
			{ 
				FadeOut(Color.white, 1.5f);
			}else{
				FadeOut(Color.black, 1.5f);
			}
			//GameObject.Find("UI_InGame").SetActive(false);
			state++;
			break;
		case STATE.eExit_Wait:
			break;
			
		case STATE.eChangeState_Bridge:
			state = nextState;
			CRI_SoundManager_2D.Instance.StopBGM();
			break;
			
			// 次の状態へ
		case STATE.eChangeState_ToGameOver:
			StateController.Instance.ChangeState(E_STATE.GameOver);
			break;
			// 次の状態へ
		case STATE.eChangeState_ToResult:
			StateController.Instance.ChangeState(E_STATE.Result);
			break;
		case STATE.eChangeState_ToTitle:	
			StateController.Instance.ChangeState(E_STATE.Title);
			break;
		
		}

	}
	
	protected override void OnDestruct ()
	{
		
	}
	
	
	//! 状態入り　初期化
	private void Enter_Init(){
		
	}
	//! 状態入り　待機
	private void Enter_Wait(){
		
	}
	
	
	//! メイン処理　初期化
	private void Main_Init(){
		
	}
	//! メイン処理　待機
	private void Main_Wait(){
		
	}
	
	override protected void OnCompleteFade(){
		state++;
	}
	
	public void ToTitle()
	{
		state = STATE.eExit_Init;
		nextState = STATE.eChangeState_ToResult;
	}
}
