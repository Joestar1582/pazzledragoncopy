using UnityEngine;
using System.Collections;

public class OrigamiSelectCollider : MonoBehaviour {
	
	[HideInInspector]
	public bool ActiveFlg = false;
	[HideInInspector]
	public bool Hit = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	private void OnTriggerStay (Collider other){
		if( other.gameObject.layer == (int)LayerEnum.layer_LeapHand && ActiveFlg ){
			Hit = true;
		}
	}
	
	private void OnTriggerExit(Collider other){
		if( other.gameObject.layer == (int)LayerEnum.layer_LeapHand && ActiveFlg ){
			Hit = false;
		}
	}
}
