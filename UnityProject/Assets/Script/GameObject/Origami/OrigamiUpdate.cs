using UnityEngine;
using System.Collections;

public class OrigamiUpdate : MonoBehaviour {
	public enum STATE{
		START_MOVE,
		END_MOVE,
		WAVE_EFFECT,
		CREATE_BREAK_EFFECT,
		BREAK_EFFECT,
		CUT,
		FOLD_SELECT,
		FOLD,
		WAIT,
		REVERT,
		STOP,
	};
	private STATE State = STATE.START_MOVE;
	private STATE OldState = STATE.START_MOVE;
	
	public GameObject	OrigamiAppearEffectPrefab;
	public GameObject	WaveEffectPrefab;
	public GameObject	BreakEffectPrefab;
	
	[HideInInspector]
	public GameObject	WakuObject;
	public Vector3		WakuInitPos = new Vector3( 0,0,100.0f );
	
	private	GameObject	OrigamiAppearEffect;
	
	const float	StartMoveTime = 120.0f;
	const float	EndMoveTime = 20.0f;
	const float	WaveEffectTime = 30.0f;
	const float	BreakEffectTime = 30.0f;
	
	private float Timer = 0.0f;
	private	float t = 0.0f;
	
	[HideInInspector]
	public	bool	RevertFlg = false;
	[HideInInspector]
	public 	bool	LineDeleteFlg = false;
	private bool	CalculateMediumPos = false;
	private Vector3	MediumPos;
	private Vector3	OrigamiPos;
	private Vector3	WakuPos;
	
	public OrigamiUpdate.STATE GetState (){
		return State;
	}
	public void SetState ( OrigamiUpdate.STATE in_state ){
		State = in_state;
	}

	// Use this for initialization
	void Start () {
		//OrigamiAppearEffect = Instantiate( OrigamiAppearEffectPrefab  ) as GameObject;
//		OrigamiAppearEffect.transform.parent = Camera.main.transform;
//		OrigamiAppearEffect.transform.localPosition = Vector3.zero;
		//gameObject.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		switch ( State ){
		case STATE.START_MOVE:
			StartMove();
			break;
			
		case STATE.WAVE_EFFECT:
			WaveEffect();
			break;
			
		case STATE.END_MOVE:
			EndMove();
			break;
			
		case STATE.CREATE_BREAK_EFFECT:
			CreateBreakEffect();
			break;
			
		case STATE.BREAK_EFFECT:
			BreakEffect();
			break;
			
		default:
			break;
		}
	}
	
	// 開始時の動き.
	private void StartMove (){
		if( StaticMath.Compensation( ref Timer, StartMoveTime, 1.0f ) ){
			State = STATE.WAVE_EFFECT;
			Instantiate( WaveEffectPrefab, transform.position, Quaternion.identity );
			Destroy( OrigamiAppearEffect );
			gameObject.renderer.enabled = true;
			//Sound
			CRI_SoundManager_2D.Instance.PlaySE(SE_ID.ING_WaveEffect);
		}
		t = 1.0f - (StartMoveTime - Timer) / StartMoveTime;
		WakuObject.transform.localPosition = Vector3.Lerp( WakuInitPos, new Vector3( 0,0,4.5f ), t );
		transform.localPosition = Vector3.Lerp( Vector3.zero, new Vector3( 0,0,4.5f ), t );
		if( State != STATE.START_MOVE ) Timer = 0.0f;
	}
	
	// 拍手された後の動き.
	private void EndMove (){
		if( !CalculateMediumPos ){ 
			Vector3 Vec = WakuObject.transform.localPosition - transform.localPosition;
			MediumPos 	= transform.localPosition + Vec / 2.0f;
			OrigamiPos 	= transform.localPosition;
			WakuPos 	= WakuObject.transform.localPosition;
			CalculateMediumPos = true;
		}
		if( StaticMath.Compensation( ref Timer, EndMoveTime, 1.0f ) ){
			State = STATE.CREATE_BREAK_EFFECT;
		}
		t = 1.0f - (EndMoveTime - Timer) / EndMoveTime;
		WakuObject.transform.localPosition = Vector3.Lerp( WakuPos, MediumPos, t );
		transform.localPosition = Vector3.Lerp( OrigamiPos, MediumPos, t );
		if( State != STATE.END_MOVE ) Timer = 0.0f;
	}
	
	// 波紋エフェクト終了待機.
	private void WaveEffect (){
		if( StaticMath.Compensation( ref Timer, WaveEffectTime, 1.0f ) ){
			Timer = 0.0f;
			gameObject.layer = (int)LayerEnum.layer_OrigamiCut;
			State = STATE.CUT;
		}
	}
	
	// エフェクト生成.
	private void CreateBreakEffect (){
		State = STATE.BREAK_EFFECT;
		Instantiate( BreakEffectPrefab, transform.position, Quaternion.identity );
	}
	
	// エフェクト終了待機.
	private void BreakEffect (){
		if( StaticMath.Compensation( ref Timer, BreakEffectTime, 1.0f ) ){
			Timer = 0.0f;
			State = STATE.WAIT;
		}
	}
	
	public void Stop (){
		if( State == STATE.STOP ) return;
		OldState = State;
		State = STATE.STOP;
	}
	
	public void ReStart (){
		if( State != STATE.STOP ) return;
		State = OldState;
	}
}
