using UnityEngine;
using System.Collections;

public class GUITextureBase : MonoBehaviour {
	
	public bool		DrawEnable = true;
	public Texture	texture;
	public Color	color = new Color( 1.0f, 1.0f, 1.0f, 1.0f );
	public Vector2	Position;
	public float	Width;
	public float	Height;
	public int		Depth = 5;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// 描画.
	void OnGUI () {
		if( !DrawEnable || texture == null ) return;
		
		Vector2	Pos = new Vector2( Position.x * DefaultScreen.Par.x, (DefaultScreen.Height-Position.y-Height)* DefaultScreen.Par.y );
		Vector2 Size = new Vector2( Width * DefaultScreen.Par.x, Height * DefaultScreen.Par.y );
		
		Vector2[] TexPolygon = new Vector2[] {
			new Vector2( Pos.x       , Pos.y ),
			new Vector2( Pos.x+Size.x, Pos.y ),
			new Vector2( Pos.x       , Pos.y+Size.y ),
			new Vector2( Pos.x+Size.x, Pos.y+Size.y ),
		};
		Vector2[] ScreenPolygon = new Vector2[] {
			new Vector2( 0.0f        , 0.0f ),
			new Vector2( Screen.width, 0.0f ),
			new Vector2( 0.0f        , Screen.height ),
			new Vector2( Screen.width, Screen.height ),
		};
		// 画面内判定.
		if( !StaticMath.CheckInPolygon2D( TexPolygon, 4, ScreenPolygon, 4 ) ){
			return;
		}
		
		GUI.depth = Depth;
		GUI.color = color;
		GUI.DrawTexture( new Rect( 
			Pos.x, Pos.y, 
			Size.x, Size.y ),
			texture );
		// 元に戻しておく.
		GUI.color = Color.white;
	}
	
	public virtual Vector2 GetPos (){
		return Position;
	}
	
	public virtual Vector2 GetScreenParPos (){
		return new Vector2( Position.x*DefaultScreen.Par.x, Position.y*DefaultScreen.Par.y );
	}
	
	public virtual Vector2 GetSize (){
		return new Vector2( Width, Height );
	}
	
	public virtual Vector2 GetScreenParSize (){
		return new Vector2( Width*DefaultScreen.Par.x, Height*DefaultScreen.Par.y );
	}
	
	public virtual Vector2 GetTexSize (){
		return new Vector2( texture.width, texture.height );
	}
	
	public virtual float GetTexRatio (){
		return (float)texture.width / (float)texture.height;
	}
	
	public virtual void SetPos	( float setX, float setY ){
		Position.x = setX;
		Position.y = setY;
	}
	
	public virtual void SetPos	( Vector2 Pos ){
		Position = Pos;
	}
	
	public virtual void SetSize	( Vector2 Size ){
		Width = Size.x;
		Height = Size.y;
	}
	
	public virtual void SetSize	( float setWidth, float setHeight ){
		Width = setWidth;
		Height = setHeight;
	}
	
	public virtual void SetSizeCenter ( Vector2 Size ){
		Vector2 Offset = new Vector2( Size.x - Width, Size.y - Height );
		Position -= Offset / 2.0f;
		Width = Size.x;
		Height = Size.y;
	}
	
	public void SetTexture ( Texture tex ){
		texture = tex;
	}
}
