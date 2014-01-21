using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandObjectController : MonoBehaviour {
	
	private Vector3			Pos;
	private List<Vector3>	PosArray = new List<Vector3>();
	private	bool			OutOfScreen = false;
	private float			Speed = 0.0f;
	private	Vector3			Offset = new Vector3( 0, 50000, 0 );
	
	public void	SetOutOfScreen ( bool Flg ){ OutOfScreen = Flg; }
	public float GetSpeed () { return Speed; }
	
	// Use this for initialization
	void Start () {
		Pos = Camera.main.transform.localPosition + Camera.main.transform.forward * 10.0f;
	}
	
	// Update is called once per frame
	void	Update	(){
		if( OutOfScreen && PosArray.Count == 0 ){
			this.enabled = false;
			//gameObject.SetActive( false );
		}
		if( PosArray.Count == 0 ){
			Speed = 0.0f;
			return;
		}
		
		const float	OffsetLength = 0.5f;
		float	Length = Vector3.Distance( Pos, PosArray[0] );
		Speed = 0.0f;
		for( int i = 1; i < PosArray.Count; i++ ){
			Speed += Vector3.Distance( PosArray[i-1], PosArray[i] );
		}
		// 距離が長すぎた場合補間しない.
		/*if( Length > MaxLength ){
			Pos = PosArray[0];
		}*/
		if( Length > OffsetLength ){
			Pos += (PosArray[0] - Pos).normalized * OffsetLength;
		}
		else{
			int	Cnt = 0;
			for( int i = 0; i < PosArray.Count; i++ ){
				if( Vector3.Distance( Pos, PosArray[i] ) <= OffsetLength ){
					Pos = PosArray[i];
					Cnt ++;
				}
				else{
					break;
				}
			}
			PosArray.RemoveRange( 0, Cnt );
		}
		
		gameObject.transform.position = Pos + Offset;
	}
	
	public void	SetPos	( Vector3 inPos ){
		
		Vector2[] ScreenPolygon = new Vector2[] {
			new Vector2( 0.0f        , 0.0f ),
			new Vector2( Screen.width, 0.0f ),
			new Vector2( 0.0f        , Screen.height ),
			new Vector2( Screen.width, Screen.height ),
		};
		Vector3	ScreenPos = Camera.main.WorldToScreenPoint( inPos+Camera.main.transform.position );
		if( StaticMath.CheckInPolygon2D( ScreenPolygon, 4, new Vector2( ScreenPos.x, ScreenPos.y ) ) ){
			if( OutOfScreen ){
				Pos = inPos;
				gameObject.transform.position = inPos + Offset;
				PosArray.Clear();
			}
			else{
				PosArray.Add( inPos );
			}
			this.enabled = true;
			//gameObject.SetActive( true );
		}
	}
	
	public Vector2 GetScreenPos (){
	  return Camera.main.WorldToScreenPoint( gameObject.transform.position );
	}
}
