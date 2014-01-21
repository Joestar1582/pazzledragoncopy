using UnityEngine;
using System.Collections;

public class GenerateCube : MonoBehaviour_Extends {
	
	public	GameObject	OrigamiCube;
	
	// Use this for initialization
	void Start () {
		Generate ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Generate(){
		for(int i=1; i<9; i++)
		{
			GameObject go = new GameObject();
			go.name = "Origami_"+i;
			
			for(int j=0; j<i; j++)
			{
				Vector3 pos = new Vector3(0,j,0);
				GameObject cube = InstantiateGameObjectAsChild(OrigamiCube, go, pos);
				cube.name = "Cube_"+j;
			}
		}
	}
}
