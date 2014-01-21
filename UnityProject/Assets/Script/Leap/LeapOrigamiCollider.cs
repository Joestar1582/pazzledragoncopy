using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeapOrigamiCollider : MonoBehaviour {
	
	public	GameObject		LineEffectPrefab;
	public	GameObject		PointLoopParticlePrefab;
	public	GameObject		PointOneShotParticlePrefab;
	private	GameObject[]	PointParticle = new GameObject[2]{ null, null };
	private	GameObject		HitObj = null;
	private Vector3			HitStartPos;
	private Vector3 		HitEndPos;
	private	bool			HitFlg = false;
	private OrigamiController OrigamiControllerScript;
	private	GameObject		LineEffectObj = null;
	private	LineEffect		LineEffectScript = null;
	private	bool			isEnd = false;
	
	private Vector3			MoveVec;
	private	Vector3			OldPos;
	private	Vector3			NowPos;
	
	private	Ray				ray1 = new Ray();
	private	Ray				ray2 = new Ray();
	private	RaycastHit 		HitInfo = new RaycastHit();
	private	Vector3			RayPosOffset = Vector3.forward * -5.0f;

	// Use this for initialization
	void Start () {
		ray1.direction = Vector3.forward;
		ray2.direction = -Vector3.forward;
		OrigamiControllerScript = GameObject.Find("OrigamiController").gameObject.GetComponent<OrigamiController>();
	}
	
	// Update is called once per frame
	void Update () {
		OldPos = NowPos;
		NowPos = transform.position;
		MoveVec = (OldPos - NowPos).normalized;
		
		if( HitObj != null ){
			if( HitObj.GetComponent<OrigamiUpdate>().LineDeleteFlg ){
				HitFlg = false;
				PointParticleDestroy();
			}
		}
		
		if( !OrigamiControllerScript.GetActiveFlg() || HitObj == null ){
			PointParticleDestroy();
			return;
		}
		if( HitObj.layer == (int)LayerEnum.layer_OrigamiCut && !HitFlg ){
			PointParticleDestroy();
		}
		
		
		if( LineEffectScript != null && !isEnd ){
			float z = LineEffectScript.targetPositionEnd.z;
			LineEffectScript.targetPositionEnd.Set( transform.localPosition.x, transform.localPosition.y, z );
		}
		
	}
	
	private	void PointParticleDestroy (){
		if( PointParticle[0] != null ){
			Destroy( PointParticle[0] );
			PointParticle[0] = null;
		}
		if( PointParticle[1] != null ){
			Destroy( PointParticle[1] );
			PointParticle[1] = null;
		}
		if( LineEffectObj != null ){
			iTween.Stop( LineEffectObj );
			Destroy( LineEffectObj );
		}
	}
	
	private void OnTriggerEnter (Collider other){
		if( enabled == false ) return;
		
		if( other.gameObject.layer == (int)LayerEnum.layer_OrigamiCut ){
			if( other.gameObject.GetComponent<OrigamiUpdate>().GetState() == OrigamiUpdate.STATE.STOP ||
				other.gameObject.GetComponent<OrigamiUpdate>().GetState() == OrigamiUpdate.STATE.FOLD ) return;
			UI_OKButton.Instance.Off();
			UI_RevertButton.Instance.Off();
			//UI_InGame.Instance.ButtonEnable(false);
			
			PointParticleDestroy();
			
			Vector3	Vec = MoveVec;
			Vec.z = 0.0f;
			Vector3	HitPos = other.ClosestPointOnBounds( transform.position );
			Vector3	HitPos2 = HitPos;
			HitPos += RayPosOffset;
			HitPos2.z = other.transform.position.z + 5.0f;
			Vector3	OldHitPos = HitPos;
			for(int i = 0;i < 10000;i++){
				ray1.origin = HitPos;
				ray2.origin = HitPos2;
				if( !other.Raycast( ray1, out HitInfo, 10.0f ) && !other.Raycast( ray2, out HitInfo, 10.0f ) ){
					HitPos = OldHitPos;
					break;
				}
				OldHitPos = HitPos;
				HitPos += Vec * 0.01f;
				HitPos2 += Vec * 0.01f;
			}
			HitPos -= RayPosOffset;

			HitObj = other.gameObject;

			HitStartPos = HitPos;
			PointParticle[0] = Instantiate( PointLoopParticlePrefab, HitStartPos, Quaternion.identity ) as GameObject;
			
			// Sound
			CRI_SoundManager_2D.Instance.PlaySE(SE_ID.ING_FlashEffect);
			
			LineEffectObj = Instantiate( LineEffectPrefab, Vector3.zero, Quaternion.identity ) as GameObject;
			LineEffectObj.transform.parent = HitObj.transform.parent;
			LineEffectScript = LineEffectObj.GetComponent<LineEffect>();

			Vector3 pos = HitPos;
			pos.y -= 50000.0f;
			pos.z = HitObj.transform.localPosition.z - 0.2f;
			LineEffectScript.targetPositionStart = pos;
			LineEffectScript.targetPositionEnd = pos;
			LineEffectScript.MoveToTargetPositionStart();
			
			HitFlg = true;
			isEnd = false;
			
			//Set AlowEffectPos.
			UI_AlowEffect.Instance.SetPoint1();
		}
	}
	
	private void OnTriggerExit (Collider other){
		if( enabled == false ) return;

		if( other.gameObject.layer == (int)LayerEnum.layer_OrigamiCut && HitFlg ){
			if( other.gameObject.GetComponent<OrigamiUpdate>().GetState() == OrigamiUpdate.STATE.STOP ) return;		
			
			Vector3	HitPos = other.ClosestPointOnBounds( transform.position );
			Vector3	HitPos2 = HitPos;
			Vector3	Vec = HitPos - HitStartPos;
			HitPos += RayPosOffset;
			HitPos2.z = other.transform.position.z + 5.0f;
			Vector3	OldHitPos = HitPos;
			for(int i = 0;i < 10000;i++){
				ray1.origin = HitPos;
				ray2.origin = HitPos2;
				if( other.Raycast( ray1, out HitInfo, 10.0f ) || other.Raycast( ray2, out HitInfo, 10.0f ) ){
					HitPos = OldHitPos;
					break;
				}
				OldHitPos = HitPos;
				HitPos -= Vec.normalized * 0.01f;
				HitPos2 -= Vec.normalized * 0.01f;
			}
			HitPos -= RayPosOffset;
			
			// 折れるかどうか判定.

			Vector3	MediumPos = HitStartPos + Vec / 2.0f;
			Vector3	MediumPos2 = MediumPos;
			Vector3	Normal = Vector3.Cross( Camera.main.transform.forward, Vec.normalized );
			

			Ray			endray = new Ray();
			float		RayLength = 10.0f;
			float		Offset = 0.3f;

			endray.direction = Camera.main.transform.forward;
			MediumPos -= endray.direction * RayLength;
			MediumPos2 += endray.direction * RayLength;
			
			bool CutFlg1 = false;
			bool CutFlg2 = false;

			endray.origin = MediumPos + Normal * Offset;
			if( other.Raycast( endray, out HitInfo, 100.0f ) ){
				CutFlg1 = true;
			}
			else{

				endray.direction = -Camera.main.transform.forward;
				endray.origin = MediumPos2 + Normal * Offset;
				if( other.Raycast( endray, out HitInfo, 100.0f ) ){
					CutFlg1 = true;
				}
			}

			endray.direction = Camera.main.transform.forward;
			endray.origin = MediumPos - Normal * Offset;
			if( other.Raycast( endray, out HitInfo, 100.0f ) ){
				CutFlg2 = true;
			}
			else{

				endray.direction = -Camera.main.transform.forward;
				endray.origin = MediumPos2 - Normal * Offset;
				if( other.Raycast( endray, out HitInfo, 100.0f ) ){
					CutFlg2 = true;
				}
			}
			
			if( CutFlg1 && CutFlg2 )
			{
				isEnd = true;
				HitEndPos = HitPos;
				LineEffectScript.targetPositionEnd.Set( HitEndPos.x, HitEndPos.y-50000.0f, LineEffectScript.targetPositionStart.z );
				Vector3	LocalStartPos = other.transform.InverseTransformPoint( HitStartPos );
				Vector3	LocalEndPos = other.transform.InverseTransformPoint( HitEndPos );
				Vector3	LocalVec = (LocalEndPos - LocalStartPos).normalized;
				
				// ContactParticleの座標と角度を設定.
				float Angle = Vector3.Dot( Vec.normalized, Camera.main.transform.right );
				if( LocalVec.z < 0.0f ){
					Angle = -Angle;
				}
				float Deg = Mathf.Acos(Angle)*180.0f/Mathf.PI;
				OrigamiSelect	OrigamiSelectScript = other.gameObject.GetComponent<OrigamiSelect>();
				OrigamiSelectScript.ContactParticleAngle = Deg;
				OrigamiSelectScript.ContactParticlePos = HitStartPos + Vec / 2.0f - Vector3.forward * 0.3f;
				
				PointParticle[1] = Instantiate( PointOneShotParticlePrefab, HitEndPos, Quaternion.identity ) as GameObject;
				
				//Sound
				CRI_SoundManager_2D.Instance.PlaySE(SE_ID.ING_FlashEffect);
				
				// カット.
				if( OrigamiMeshCutter.Cut( other.gameObject, HitStartPos, HitEndPos ) ){
					other.gameObject.layer = (int)LayerEnum.layer_OrigamiWait;
					OrigamiControllerScript.SetState( OrigamiUpdate.STATE.FOLD_SELECT );
					
					// 2013/11/26 kojima
					iTweenEvent.GetEvent(GameObject.Find("UI_Select"), "FadeIn").Play();
					
					// Set AlowEffectPos.
					UI_AlowEffect.Instance.SetPoint2();
				}
			}
			else{
				PointParticleDestroy();
				
				UI_OKButton.Instance.On();
				UI_RevertButton.Instance.On();
			}
			
			HitFlg = false;
		}
	}
}
