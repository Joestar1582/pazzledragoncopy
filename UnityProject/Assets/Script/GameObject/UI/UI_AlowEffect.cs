using UnityEngine;
using System.Collections;

public class UI_AlowEffect : SingletonMonoBehaviour<UI_AlowEffect> {

//	public	Camera	uiCamera;
	private	Vector3		hitPoint1;
	private	Vector3		hitPoint2;
	private	UISprite	sprite;
	
	void Start(){
		sprite = GetComponent<UISprite>();
	}
	
	public	void	SetPoint1(){
		hitPoint1 = UI_OrigamiDicisionGauge.Instance.transform.parent.localPosition;
	}
	
	public	void	SetPoint2(){
		hitPoint2 = UI_OrigamiDicisionGauge.Instance.transform.parent.localPosition;
		
		transform.localPosition	= hitPoint1 + (hitPoint2 - hitPoint1)/2;
	}
	
	public	void	On(){
		sprite.spriteName = "alow_shorter0000";
		
	//	if(sprite.alpha == 0)
			GetComponent<UI_Sprite_Fade>().FadeIn(0.25f);
		
		Vector3 lineVec	= hitPoint2 - hitPoint1;
		Vector3	palmPos = UI_OrigamiDicisionGauge.Instance.transform.parent.localPosition;
		Vector3	vecToPalm	= palmPos - hitPoint1;
		
		transform.rotation = getLookDirection( transform.rotation, Vector3.up, lineVec);
		
		if(Vector3.Cross(lineVec, vecToPalm).z > 0)
			transform.localEulerAngles -= Vector3.forward * 90.0f;
		else
			transform.localEulerAngles += Vector3.forward * 90.0f;

	}
	
	public	void	Off(){
	//	if(sprite.alpha > 0)
			GetComponent<UI_Sprite_Fade>().FadeOut(0.25f);
	}
	
	public Quaternion getLookDirection( Quaternion q, Vector3 axis, Vector3 direction )
	{
	    Vector3 v   = q * axis;
	    Vector3 nm  = Vector3.Cross( v, direction );
	    float   ang = Vector3.Angle( v, direction );
	    return Quaternion.AngleAxis( ang , nm ) * q;
	}
}
