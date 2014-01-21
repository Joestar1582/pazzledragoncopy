using UnityEngine;
using System.Collections;

public class OpenningState : StateBase {
	public GameObject mainView;
	public GameObject textObject;
	
	
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
		textObject.transform.localScale = Vector3.zero;
	}
	
	
	//! 実行.
	public override void Exec ()
	{
		// Debug
		if(Input.GetKeyDown(KeyCode.Space))
			state = STATE.eExit_Init;

		switch(state)
		{
			// タイトル　状態入り.
		case STATE.eEnter_Init:
			CRI_SoundManager_2D.Instance.PlayBGM(BGM_ID.OPENING);
			for(int i = 0;i < 7;i++)
				Game_CityLayer.Instance.CityLayerEnable(i,true);
			FadeIn(1.5f);
			state++;
			break;
		case STATE.eEnter_Wait:
			break;
			
			// タイトル　入力待ち.
		case STATE.eMain_Init:
			textObject.SendMessage("OpenText");
			state++;
			break;
		case STATE.eMain_Wait:
			mainView.transform.LookAt(Vector3.zero);
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
			Game_CityLayer.Instance.InitCityLayer();
			StateController.Instance.ChangeState(E_STATE.Tutorial);
			break;
		}
	}
	
	
	//! デストラクタ.
	protected override void OnDestruct ()
	{
		
	}
	
	
	override protected void OnCompleteFade(){
		state++;
	}
	
	void EndOpenning()
	{
		state = STATE.eExit_Init;
	}
	

}
