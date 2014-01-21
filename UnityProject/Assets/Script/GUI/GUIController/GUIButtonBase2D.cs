using UnityEngine;
using System.Collections;

public class GUIButtonBase2D : SlideGUITexture {
	const int MaxPolygon = 4;
	public Vector2[] HitPolygon = new Vector2[MaxPolygon];
	// 若干判定がずれるのでオフセットで対応.
	public Vector2 HitPolygonOffset = new Vector2( 0.96f, 1.12f );
	
	public bool		isButtonScaleAnimation = false;
	public Vector2 	BaseScale;
	public Vector2 	MouseClickScalse;
	public Vector2 	MouseOverScalse;
	public float	AnimationTime = 5;
	float	AnimationTimer = 0;
	bool	isNowAnimation = false;
	bool	isNowClickAnimation = false;
	bool	isScale = false;
	bool	isBaseScale = true;
	bool	isClickEnd = false;
	bool	isRelease = false;
	
	int NowMouseOver = 0;
	int OldMouseOver = 0;
	int TrgMouseOver = 0;
	int RlsMouseOver = 0;
	int MouseUp = 0;
	int MouseDown = 0;
	int MouseClick = 0;
	
	public int GetNowMouseOver	() { return NowMouseOver; }
	public int GetOldMouseOver	() { return OldMouseOver; }
	public int GetTrgMouseOver	() { return TrgMouseOver; }
	public int GetRlsMouseOver	() { return RlsMouseOver; }
	public int GetMouseUp		() { return MouseUp; }
	public int GetMouseDown 	() { return MouseDown; }
	public int GetMouseClick 	() { return MouseClick; }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	// ボタンアニメーション
	void ButtonAnimation () {
		if( TrgMouseOver == 1 && !isNowAnimation ){
			isNowAnimation = true;
		}
		if( MouseClick == 1 && !isNowClickAnimation ){
			isNowClickAnimation = true;
			isClickEnd = false;
		}
		
		Vector2 FromScale = BaseScale;
		Vector2	Scale = MouseOverScalse;
		if( isNowClickAnimation ){
			Scale = MouseClickScalse;
			if( !isScale && !isRelease ){
				FromScale = MouseOverScalse;
			}
		}
		
		if( isNowAnimation || MouseClick == 1 || NowMouseOver == 1 || !isBaseScale ){
			float t = 1.0f - (AnimationTime - AnimationTimer) / AnimationTime;
			if( isScale || MouseClick == 1 || (NowMouseOver == 1 && !isNowClickAnimation) ){
				isBaseScale = false;
				if( StaticMath.Compensation( ref AnimationTimer, AnimationTime, 1.0f ) ){
					isScale = false;
				}
			}
			else{
				if( StaticMath.Compensation( ref AnimationTimer, 0, 1.0f ) ){
					if( !isNowClickAnimation ){
						isBaseScale = true;
						isNowAnimation = false;
						isScale = true;
						isRelease = false;
					}else{
						if( !isRelease )
							AnimationTimer = AnimationTime-1.0f;
						isClickEnd = true;
						isRelease = false;
					}
					isNowClickAnimation = false;
				}
			}
			SetSizeCenter( Vector2.Lerp( FromScale, Scale, t ) );
		}
	}
	
	// マウスとの当たり判定更新.
	public void UpdateMouseHit () {
	
		// 範囲内に含まれているかチェックし各フラグ更新.
		OldMouseOver = NowMouseOver;
		if( CheckMouseOver() ) {
			NowMouseOver = 1;
		}
		else{
			NowMouseOver = 0;
		}
		TrgMouseOver = ( NowMouseOver ^ OldMouseOver ) & NowMouseOver;
		RlsMouseOver = ( NowMouseOver ^ OldMouseOver ) & OldMouseOver;
		
		if( RlsMouseOver == 1 && MouseClick == 1 ){
			isRelease = true;
		}
		
		// クリック判定.
		if( NowMouseOver == 1 && Input.GetMouseButtonUp( 0 ) )
			MouseUp = 1;
		else
			MouseUp = 0;
		
		if( NowMouseOver == 1 && Input.GetMouseButtonDown( 0 ) )
			MouseDown = 1;
		else
			MouseDown = 0;
		
		if( NowMouseOver == 1 && Input.GetMouseButton( 0 ) )
			MouseClick = 1;
		else
			MouseClick = 0;
		
		if( MouseUp == 1 && isNowAnimation ){
			isClickEnd = true;
		}
		
		if( isButtonScaleAnimation )
			ButtonAnimation();
	}
	
	// マウスが当たっているか判定.
	bool CheckMouseOver () {
		// 座標を補正する.
		Vector2	Par;
		Vector2 Size;
		Vector2 Pos = GetScreenParPos();
		if( isButtonScaleAnimation ){
			Size = new Vector2( BaseScale.x*DefaultScreen.Par.x, BaseScale.y*DefaultScreen.Par.y );
			if( !isBaseScale ){
				Size = new Vector2( MouseOverScalse.x*DefaultScreen.Par.x, MouseOverScalse.y*DefaultScreen.Par.y );
			}
			Pos -= (Size - GetScreenParSize()) / 2.0f;
		}
		else{
			Size = GetScreenParSize();
		}
		Par.x = Size.x / (float)texture.width;
		Par.y = Size.y / (float)texture.height;
		Vector2[] Polygon = new Vector2[HitPolygon.Length];
		for( int i = 0; i < HitPolygon.Length; i++ ){
			Polygon[i].x = Pos.x + HitPolygon[i].x * Par.x * HitPolygonOffset.x;
			Polygon[i].y = Pos.y + Size.y - HitPolygon[i].y * HitPolygonOffset.y * Par.y;
		}
		
		// マウス座標取得.
		Vector2 MousePos = new Vector2( Input.mousePosition.x, Input.mousePosition.y );
		// 当たり判定.
		return StaticMath.CheckInPolygon2D( Polygon, HitPolygon.Length, MousePos );
		
	}
	
	public void	InitButton (){
		NowMouseOver = 0;
		OldMouseOver = 0;
		TrgMouseOver = 0;
		RlsMouseOver = 0;
		MouseUp = 0;
		MouseDown = 0;
		MouseClick = 0;
		AnimationTimer = 0;
		isNowAnimation = false;
		isNowClickAnimation = false;
		isScale = false;
		isBaseScale = true;
		isClickEnd = false;
		isRelease = false;
		SetSize( BaseScale );
	}
	
	public bool	GetIsBaseScale (){
		if( isClickEnd ){
			isClickEnd = false;
			return true;
		}
		return false;
	}
}
