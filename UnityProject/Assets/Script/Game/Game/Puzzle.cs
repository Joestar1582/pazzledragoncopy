using UnityEngine;
using System.Collections;

public class Puzzle : MonoBehaviour {
	private bool bSelected = false; 
	  private Vector3 screenPoint;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
 
	}
	
    void OnMouseDown() {
       this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
//		bSelected = true;
    }

    void OnMouseUp() {
		bSelected = false;
    }

	void OnMouseDrag() {
    	Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
        transform.position = currentPosition;
//		bSelected = true;
	}
	
	public void SetColor(Material mat)
	{
		this.renderer.material = mat;
	}
	
	public bool CheckSelecting()
	{
		return bSelected;
	}
	
}
