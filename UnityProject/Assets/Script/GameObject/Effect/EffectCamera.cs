using UnityEngine;
using System.Collections;

public class EffectCamera : SingletonMonoBehaviour<EffectCamera> {
	
	public void CreateButtonEffect(){
		if(transform.FindChild("ButtonEffect") == null)
		{
			GameObject effect = InstantiateGameObjectAsChild( Resources.Load("Prefabs/Effects/ButtonEffect") , new Vector3(0,0,15.0f) , true);
			effect.GetComponent<CursorObject>().parentCamera = camera;
			effect.name = "ButtonEffect";
		}
	}
	
	public void DestroyButtonEffect(){
		if(transform.FindChild("ButtonEffect") != null)
		{
			Destroy(transform.FindChild("ButtonEffect").gameObject);
		}
	}
}
