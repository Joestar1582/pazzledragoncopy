using UnityEngine;
using System.Collections;

public class OrigamiSelect : MonoBehaviour {
	
	private	OrigamiSelectCollider[]	SelectColliderScript = new OrigamiSelectCollider[2];
	private	OrigamiController	OrigamiControllerScript;
	private	OrigamiUpdate		OrigamiUpdateScript;
	
	public GameObject	ContactParticlePrefab;
	[HideInInspector]
	public	float		ContactParticleAngle;
	[HideInInspector]
	public	Vector3		ContactParticlePos;
	
	private	Mesh	NewMesh;
	private Vector3	HitVec;
	private	Vector3	HitNormal;
	private	Vector3	HitPoint1;
	private	Vector3	HitPoint2;
	private	bool	ActiveFlg;
	private	bool[]	SelectFlg = new bool[]{ false,false,false };
	private	float[]	SelectTimer = new float[]{ 0,0,0 };
	private MeshFilter[]	SelectMesh = new MeshFilter[2];
	private bool	TutorialFlg = false;
	
	private const float	IntervalTime = 0.5f * 60.0f;
	private float		Timer = 0.0f;
	private	bool		SelectStartFlg = false;
	
	public void TutorialModeEnable (){
		TutorialFlg = true;
	}

	// Use this for initialization
	void Start () {

		UI_OrigamiDicisionGauge.Instance.fillTime = 2.0f;
		OrigamiUpdateScript = gameObject.GetComponent<OrigamiUpdate>();
		OrigamiControllerScript = GameObject.Find("OrigamiController").gameObject.GetComponent<OrigamiController>();
		SelectColliderScript[0] = transform.FindChild( "SelectMesh1" ).GetComponent<OrigamiSelectCollider>();
		SelectColliderScript[1] = transform.FindChild( "SelectMesh2" ).GetComponent<OrigamiSelectCollider>();
		SelectMesh[0] = SelectColliderScript[0].gameObject.GetComponent<MeshFilter>();
		SelectMesh[1] = SelectColliderScript[1].gameObject.GetComponent<MeshFilter>();
		SelectMesh[0].renderer.enabled = false;
		SelectMesh[1].renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if( !ActiveFlg ) return;
		if( OrigamiUpdateScript.GetState() == OrigamiUpdate.STATE.STOP ){
			UI_OrigamiDicisionGauge.Instance.Pause();
			return;
		}
		
		if( !SelectStartFlg ){
			if( StaticMath.Compensation( ref Timer, IntervalTime, 1.0f ) ){
				UI_OrigamiDicisionGauge.Instance.spriteEnable( true );
				UI_OrigamiDicisionGauge.Instance.PlayAtZero();
				SelectColliderScript[0].Hit = false;
				SelectColliderScript[1].Hit = false;
				SelectStartFlg = true;
			}
			return;
		}
		UI_OrigamiDicisionGauge.Instance.Play();
		
		
		if( SelectColliderScript[0].Hit ){
			if( !SelectFlg[0] ){
				SelectFlg[0] = true;
				SelectFlg[1] = SelectFlg[2] = false;
				UI_OrigamiDicisionGauge.Instance.SetNowAmount( SelectTimer[0] );
				UI_OrigamiDicisionGauge.Instance.Play();
				UI_OrigamiDicisionGauge.Instance.SetSpriteType(UI_OrigamiDicisionGauge.SPRITETYPE.Select);
				
				// AlowEffect On
				UI_AlowEffect.Instance.On();
			}
		}
		else if( SelectColliderScript[1].Hit ){
			if( !SelectFlg[1] ){
				SelectFlg[1] = true;
				SelectFlg[0] = SelectFlg[2] = false;
				UI_OrigamiDicisionGauge.Instance.SetNowAmount( SelectTimer[1] );
				UI_OrigamiDicisionGauge.Instance.Play();
				UI_OrigamiDicisionGauge.Instance.SetSpriteType(UI_OrigamiDicisionGauge.SPRITETYPE.Select);
				
				// AlowEffect On
				UI_AlowEffect.Instance.On();
			}
		}
		else{
			if( TutorialFlg ){
				UI_OrigamiDicisionGauge.Instance.SetNowAmount( 0 );
				UI_OrigamiDicisionGauge.Instance.Stop();
				SelectFlg[0] = SelectFlg[1] = SelectFlg[2] = false;
				UI_AlowEffect.Instance.Off();
			}
			else if( !SelectFlg[2] ){
				SelectFlg[2] = true;
				SelectFlg[0] = SelectFlg[1] = false;
				UI_OrigamiDicisionGauge.Instance.SetNowAmount( SelectTimer[2] );
				UI_OrigamiDicisionGauge.Instance.Play();
				UI_OrigamiDicisionGauge.Instance.SetSpriteType(UI_OrigamiDicisionGauge.SPRITETYPE.Cancel);
				
				// AlowEffect Off
				UI_AlowEffect.Instance.Off();
			}
		}
		UpdateTimer();
		UpdateMeshRenderer();
		
		
		if( UI_OrigamiDicisionGauge.Instance.isFilled() ){
			UI_OrigamiDicisionGauge.Instance.spriteEnable( false );
			SelectMesh[0].renderer.enabled = false;
			SelectMesh[1].renderer.enabled = false;
			if 		( SelectFlg[0] )	Fold( true );
			else if ( SelectFlg[1] )	Fold( false );
			else {
				OrigamiControllerScript.SetState( OrigamiUpdate.STATE.CUT );
				gameObject.layer = (int)LayerEnum.layer_OrigamiCut;
				
				CRI_SoundManager_2D.Instance.PlaySE(SE_ID.ING_ContactEffect);
			}
			ActiveFlg = false;
			
			// 2013/11/26 kojima
			iTweenEvent.GetEvent(GameObject.Find("UI_Select"), "FadeOut").Play();
			// AlowEffect Off
			UI_AlowEffect.Instance.Off();
			
			UI_OKButton.Instance.On();
			UI_RevertButton.Instance.On();
		}
	}
	
	private void UpdateMeshRenderer (){
		if( SelectFlg[0] ){
			SelectMesh[0].renderer.enabled = true;
			SelectMesh[1].renderer.enabled = false;
		}
		else if( SelectFlg[1] ){
			SelectMesh[0].renderer.enabled = false;
			SelectMesh[1].renderer.enabled = true;
		}
		else{
			SelectMesh[0].renderer.enabled = false;
			SelectMesh[1].renderer.enabled = false;
		}
	}
	
	private void UpdateTimer (){
		float DecreaseTime = (Time.deltaTime / UI_OrigamiDicisionGauge.Instance.fillTime) * 3.0f;
		if( SelectFlg[0] ){
			SelectTimer[0] = UI_OrigamiDicisionGauge.Instance.GetNowAmount();
			if( SelectTimer[1] > 0.0f ) SelectTimer[1]-=DecreaseTime;
			if( SelectTimer[2] > 0.0f ) SelectTimer[2]-=DecreaseTime;
		}
		else if( SelectFlg[1] ){
			SelectTimer[1] = UI_OrigamiDicisionGauge.Instance.GetNowAmount();
			if( SelectTimer[0] > 0.0f ) SelectTimer[0]-=DecreaseTime;
			if( SelectTimer[2] > 0.0f ) SelectTimer[2]-=DecreaseTime;
		}
		else if( SelectFlg[2] ){
			SelectTimer[2] = UI_OrigamiDicisionGauge.Instance.GetNowAmount();
			if( SelectTimer[0] > 0.0f ) SelectTimer[0]-=DecreaseTime;
			if( SelectTimer[1] > 0.0f ) SelectTimer[2]-=DecreaseTime;
		}
	}
	
	private	void Fold ( bool OriFlg ){
		
		OrigamiCollider origamiCollider = gameObject.GetComponent<OrigamiCollider>();
		gameObject.GetComponent<MeshFilter>().mesh = NewMesh;
		gameObject.GetComponent<MeshCollider>().sharedMesh = NewMesh;
		
		Vector3[]	Vertex = NewMesh.vertices;
		int[]		Index = NewMesh.triangles;

		// 判定を折る.
		Vector3		CrossPoint = Vector3.zero;
		Vector2[]	Polygon = new Vector2[3];
		Vector2		Point = new Vector2();
		if( origamiCollider == null )	return;
		origamiCollider.BackUpPoint();
		for( int i = 0; i < origamiCollider.RayPoint.Length; i++ ){
			if( !origamiCollider.RayPoint[i].Enable )	continue;
			if( OrigamiCutter.GetSide( HitVec, origamiCollider.RayPoint[i].Position - HitPoint1 ) == OriFlg ){
				float Len = StaticMath.RayDistancePoint( HitPoint1, HitPoint2, origamiCollider.RayPoint[i].Position, ref CrossPoint );
				if( OriFlg ){
					origamiCollider.RayPoint[i].Position -= HitNormal * Len * 2.0f;
				}
				else{
					origamiCollider.RayPoint[i].Position += HitNormal * Len * 2.0f;
				}
				
				Point.Set( origamiCollider.RayPoint[i].Position.x, origamiCollider.RayPoint[i].Position.z );
				for( int j = 0; j < Index.Length; j+=3 ){
					Polygon[0].Set( Vertex[Index[j+0]].x, Vertex[Index[j+0]].z );
					Polygon[1].Set( Vertex[Index[j+1]].x, Vertex[Index[j+1]].z );
					Polygon[2].Set( Vertex[Index[j+2]].x, Vertex[Index[j+2]].z );
					if( StaticMath.CheckInPolygon2D( Polygon, 3, Point ) ){
						origamiCollider.RayPoint[i].Enable = false;
						break;
					}
				}
			}
		}
		
		// 折る前と折った後の頂点座標とインデックスを求める.
		int OriCnt = 0;
		for( int i = 0; i < Vertex.Length; i++ ){
			if( OrigamiCutter.GetSide( HitVec, Vertex[i] - HitPoint1 ) == OriFlg ){
				OriCnt++;
			}
		}
		Vector3[] 	StartPos = new Vector3[OriCnt];
		Vector3[] 	EndPos = new Vector3[OriCnt];
		int[]		OrigamiIndex = new int[OriCnt];
		OriCnt = 0;
		for( int i = 0; i < Vertex.Length; i++ ){
			if( OrigamiCutter.GetSide( HitVec, Vertex[i] - HitPoint1 ) == OriFlg ){
				float Len = StaticMath.RayDistancePoint( HitPoint1, HitPoint2, Vertex[i], ref CrossPoint );
				StartPos[OriCnt] = Vertex[i];
				if( OriFlg ){
					EndPos[OriCnt] = Vertex[i] - HitNormal * Len * 2.0f;
				}
				else{
					EndPos[OriCnt] = Vertex[i] + HitNormal * Len * 2.0f;
				}
				OrigamiIndex[OriCnt] = i;
				OriCnt ++;
			}
		}
		OrigamiControllerScript.SetState( OrigamiUpdate.STATE.FOLD );
		origamiCollider.SetStartPos( StartPos );
		origamiCollider.SetEndPos( EndPos );
		origamiCollider.SetOrigamiIndex( OrigamiIndex );
		
		Instantiate( ContactParticlePrefab, ContactParticlePos, Quaternion.AngleAxis( ContactParticleAngle, Camera.main.transform.forward ) );
	}
	
	public void SetCutInfo ( Vector3 inHitVec, Vector3 inHitNormal, Vector3 inHitPoint1, Vector3 inHitPoint2 ){
		HitVec = inHitVec;
		HitNormal = inHitNormal;
		HitPoint1 = inHitPoint1;
		HitPoint2 = inHitPoint2;
	}
	
	public void SetSelectMesh ( Mesh inNewMesh, Mesh Mesh1, Mesh Mesh2 ){
		NewMesh = inNewMesh;
		SelectMesh[0].mesh = Mesh1;
		SelectMesh[0].renderer.enabled = false;
		SelectMesh[1].mesh = Mesh2;
		SelectMesh[1].renderer.enabled = false;
		SelectColliderScript[0].gameObject.GetComponent<MeshCollider>().sharedMesh = Mesh1;
		SelectColliderScript[1].gameObject.GetComponent<MeshCollider>().sharedMesh = Mesh2;
		SelectColliderScript[0].ActiveFlg = true;
		SelectColliderScript[1].ActiveFlg = true;
		SelectColliderScript[0].Hit = SelectColliderScript[1].Hit = false;
			
		SelectFlg[0] = SelectFlg[1] = SelectFlg[2] = false;
		SelectTimer[0] = SelectTimer[1] = SelectTimer[2] = 0.0f;
		ActiveFlg = true;
		SelectStartFlg = false;
		Timer = 0.0f;
	}
}
