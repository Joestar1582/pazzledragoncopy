using UnityEngine;
using System.Collections;

//===================================================
/*!
 *	@brief		Transform用拡張メソッド群
 *
 *	@date		2013/05/02
 *	@author		Daisuke Kojima
 */
//===================================================
public static class TransformExtension
{
	// ********** Transform.position ***********

	/*! position.xを直接セット可能にする */
	public static void SetPosX(this Transform transform, float x)
	{
		Vector3 newPosition = 
			new Vector3(x, transform.position.y, transform.position.z);
		
		transform.position = newPosition;
	}
	
	/*! position.yを直接セット可能にする */
	public static void SetPosY(this Transform transform, float y)
	{
		Vector3 newPosition = 
			new Vector3(transform.position.x, y, transform.position.z);
		
		transform.position = newPosition;
	}
	
	/*! position.zを直接セット可能にする */
	public static void SetPosZ(this Transform transform, float z)
	{
		Vector3 newPosition = 
			new Vector3(transform.position.x, transform.position.y, z);
		
		transform.position = newPosition;
	}
	
	/*! position.xを直接セット可能にする */
	public static void SetLocalPosX(this Transform transform, float x)
	{
		Vector3 newPosition = 
			new Vector3(x, transform.localPosition.y, transform.localPosition.z);
		
		transform.localPosition = newPosition;
	}
	
	/*! position.yを直接セット可能にする */
	public static void SetLocalPosY(this Transform transform, float y)
	{
		Vector3 newPosition = 
			new Vector3(transform.localPosition.x, y, transform.localPosition.z);
		
		transform.localPosition = newPosition;
	}
	
	/*! position.zを直接セット可能にする */
	public static void SetLocalPosZ(this Transform transform, float z)
	{
		Vector3 newPosition = 
			new Vector3(transform.localPosition.x, transform.localPosition.y, z);
		
		transform.localPosition = newPosition;
	}
	
	
	// ********** Transform.localScale ***********
	
	/*! localScale.xを直接セット可能にする */
	public static void SetScaleX(this Transform transform, float x)
	{
		Vector3 newScale = 
			new Vector3(x, transform.localScale.y, transform.localScale.z);

		transform.localScale = newScale;
	}
	
	/*! localScale.yを直接セット可能にする */
	public static void SetScaleY(this Transform transform, float y)
	{
		Vector3 newScale = 
			new Vector3(transform.localScale.x, y, transform.localScale.z);

		transform.localScale = newScale;
	}
	
	/*! localScale.zを直接セット可能にする */
	public static void SetScaleZ(this Transform transform, float z)
	{
		Vector3 newScale = 
			new Vector3(transform.localScale.x, transform.localScale.y, z);

		transform.localScale = newScale;
	}
	
	/*! localScale.x～zを同じ値にセット */
	public static void SetScaleAll(this Transform transform, float scale)
	{
		Vector3 newScale = 
			new Vector3(scale, scale, scale);

		transform.localScale = newScale;
	}
	
	
	// ********** Transform.rotation ***********

	/*! rotation.x～zを一括セット */
	public static void SetRotation(this Transform transform, float x, float y, float z)
	{
		Quaternion newRotation =
			new Quaternion(x,y,z,1);

		transform.rotation = newRotation;
	}
}