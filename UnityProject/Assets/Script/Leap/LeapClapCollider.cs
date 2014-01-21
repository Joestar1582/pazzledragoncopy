using UnityEngine;
using System.Collections;

public class LeapClapCollider : MonoBehaviour {
	
	HandObjectController	HandControllerScript = null;
	OrigamiController		OrigamiControllerScript;

	// Use this for initialization
	void Start () {
		HandControllerScript = transform.parent.gameObject.GetComponent<HandObjectController>();
		OrigamiControllerScript = GameObject.Find( "OrigamiController" ).GetComponent<OrigamiController>();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	private void OnTriggerEnter (Collider other){
		if( HandControllerScript == null ){
			HandControllerScript = transform.parent.gameObject.GetComponent<HandObjectController>();
		}
		if( other.gameObject.layer == (int)LayerEnum.layer_LeapHand ){
			if( HandControllerScript.GetSpeed() > 0.3f &&
				OrigamiControllerScript.GetActiveFlg() ){
				//OrigamiControllerScript.Clap();
			}
		}
	}
}
