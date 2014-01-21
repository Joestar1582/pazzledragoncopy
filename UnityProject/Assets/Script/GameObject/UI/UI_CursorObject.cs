using UnityEngine;
using System.Collections;

public class UI_CursorObject : MonoBehaviour {
	public	float			defaultZ = -1;
	private	GameObject[]	palmObj;
	
	public Vector3 GetScreenPos(){
		return transform.localPosition;
	}
	
	// Use this for initialization
	void Start () {
		palmObj = new GameObject[2];
		
		LeapUnityHandController hands = GameObject.Find("Leap Hands").GetComponent<LeapUnityHandController>();
		
		palmObj[0] = hands.m_palms[0];
		palmObj[1] = hands.m_palms[0];	
	}
	
	// Update is called once per frame
	void Update () {
		if(DebugManager.Instance.mouseMode)
		{
			transform.localPosition = Input.mousePosition - new Vector3(Screen.width/2, Screen.height/2, -defaultZ);
		}else{
			for(int i=0; i<2; i++)
			{
				if(palmObj[i].GetComponent<BoxCollider>().enabled)
				{
					Vector2 pos = palmObj[i].GetComponent<HandObjectController>().GetScreenPos();
					transform.localPosition = new Vector3(pos.x, pos.y, 0) - new Vector3(Screen.width/2, Screen.height/2, -defaultZ);
				}
			}
		//	transform.localPosition = GameObject.Find() - new Vector3(Screen.width/2, Screen.height/2, 0);
		}	
	}
}
