using UnityEngine;
using System.Collections;

//=============================================================
/*!
 *	@brief	シングルトンのMonoBehaviourを作りたいときに継承させるクラス
 *
 *	@date	2013/05/02
 */
//=============================================================
public class SingletonMonoBehaviour<T> : MonoBehaviour_Extends where T : MonoBehaviour_Extends
{
	private static T instance;
	public static T Instance {
		get {
			if (instance == null) {
				instance = (T)FindObjectOfType (typeof(T));
 
				if (instance == null) {
					Debug.LogError (typeof(T) + "is nothing");
				}
			}
 
			return instance;
		}
	}
	
	protected virtual void Awake()
	{
		CheckInstance();
	}
	
	protected bool CheckInstance()
	{
		if( this == Instance){ return true;}
		Destroy(this);
		return false;
	}
}