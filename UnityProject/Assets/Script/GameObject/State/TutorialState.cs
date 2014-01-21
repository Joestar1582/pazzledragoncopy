using UnityEngine;
using System.Collections;

public class TutorialState : StateBase {
		
	// 状態管理.
	enum STATE{
		eEnter_Init = 0,
		eEnter_Wait,
		
		eMain_Init,
		eMain_Wait,
				
		eExit_Init,
		eExit_Wait,
		
		eChangeState_ToInGame
	}
	
	private STATE	state		= STATE.eEnter_Init;	//!< 状態管理用.
	
	public	int	OriTime = 300;
	
	protected override void Start ()
	{
		base.Start ();
	}
	
	public override void Exec ()
	{
		// Debug
		if(Input.GetKeyDown(KeyCode.Space))
			state = STATE.eExit_Init;
		
		switch(state)
		{
			// インゲーム　状態入り.
		case STATE.eEnter_Init:
			CRI_SoundManager_2D.Instance.PlayBGM(BGM_ID.TUTORIAL);

			Debug.Log("Enter_Tutorial");
			Game_CityLayer.Instance.CityLayerEnable(0,true);
//			UI_TimeCounter.Instance.SetEnable(false);
//			UI_TentionGauge.Instance.SetEnable(false);
			FadeIn(1.5f);			
			state = STATE.eEnter_Wait;
			
			break;
		case STATE.eEnter_Wait:
			break;
			
			// インゲーム　入力待ち.
		case STATE.eMain_Init:
			UI_TimeCounter.Instance.SetEnable(true);
			UI_TentionGauge.Instance.SetEnable(true);
			state = STATE.eMain_Wait;
//			PlayOrigami();
			break;

		case STATE.eMain_Wait:
//			UI_TimeCounter.Instance.Exec();
			break;
			
		case STATE.eExit_Init:
			FadeOut(1.5f);
			Destroy(GameObject.Find("UI_Tutorial"));
			state = STATE.eExit_Wait;
			break;
			
		case STATE.eExit_Wait:
			break;
			
			// 次の状態へ.
		case STATE.eChangeState_ToInGame:
			StateController.Instance.ChangeState(E_STATE.InGame);
			break;
		}
	}
	
	protected override void OnDestruct ()
	{
		
	}
	
	
	//! 状態入り　初期化.
	private void Enter_Init(){
		
	}
	//! 状態入り　待機.
	private void Enter_Wait(){
		
	}
	
	
	//! メイン処理　初期化.
	private void Main_Init(){
		
	}
	//! メイン処理　待機.
	private void Main_Wait(){
		
	}
	
	override protected void OnCompleteFade(){
		state++;
	}
	
	public void EndText()
	{
		state = STATE.eExit_Init;
		Debug.Log("EndText");
	}
	
}
