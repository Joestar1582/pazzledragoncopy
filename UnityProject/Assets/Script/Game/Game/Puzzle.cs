using UnityEngine;
using System.Collections;

public class Puzzle : MonoBehaviour {
	private bool 		bSelected = false; 
	private Vector3 	screenPoint;
    private Vector3 	offset;

	private Vector3 	oldPos;
	private Vector3 	moveAmount;
	
	private	int 		ID = 999;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
 
	}
	
    void OnMouseDown() {
       	this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		oldPos = transform.position;
//		bSelected = true;
		print ("Selected Puzzle ID " + ID.ToString());
    }

    void OnMouseUp() {
		bSelected = false;
    }

	void OnMouseDrag() {
    	Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
       	transform.position = currentPosition;
		bSelected = true;
		moveAmount = currentPosition - oldPos;
		bSelected = true;
	}
	
	public void SetColor(Material mat)
	{
		this.renderer.material = mat;
	}
	
	public bool CheckSelecting()
	{
		return bSelected;
	}
	
	public void SetID(int id)
	{
		ID = id;
	}

	public int GetID()
	{
		return ID;
	}
	
	public Vector3 GetMoveAmount()
	{
		return moveAmount;
	}

	public void MoveAmountClear()
	{
		oldPos = transform.position;
		moveAmount = Vector3.zero;
	}
}
