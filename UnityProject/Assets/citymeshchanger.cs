using UnityEngine;
using System.Collections;

public class citymeshchanger : MonoBehaviour {
	
	public	Mesh[]	meshTemplates;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha9))
			ChangeMesh();
	}
	
	void ChangeMesh(){
		MeshFilter[] mf = GetComponentsInChildren<MeshFilter>();
		
		foreach(MeshFilter it in mf)
		{
			if(it.gameObject.name.Contains("Building"))
			{
				it.mesh = meshTemplates[Random.Range(0,meshTemplates.Length)];
				Vector3 scale = it.transform.localScale;
				scale.x = scale.z = Random.Range(0.003f, 0.005f);
				scale.y = Random.Range(0.1f, 0.6f);
				
				it.transform.localScale = scale;
			}
		}
	}
}
