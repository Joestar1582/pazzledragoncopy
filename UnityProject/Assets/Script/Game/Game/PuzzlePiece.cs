using UnityEngine;
using System.Collections;

public class PuzzlePiece : MonoBehaviour {
	public 		int 		ID			= 999;
	public 		bool 		selected	= false;
	public 		bool		used		= false;
	public 		int 		colorNo		= 999;

	private 	Vector3 	screenPoint;
    private 	Vector3 	offset;
	private 	Vector3 	oldPos;
	public 		Vector3 	moveAmount;	

	
	# region Mouse Action
    void OnMouseDown() {
       	this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        this.offset = transform.position - 
		Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		oldPos = transform.position;
//		selected = true;
    }

    void OnMouseUp() {
		selected = false;
    }

	void OnMouseDrag() {
    	Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
       	transform.position = currentPosition;
		selected = true;
		moveAmount = currentPosition - oldPos;

		#region Debug
		if(Input.GetKeyDown(KeyCode.Space))
		{
			used = used ^ true;
			print ("Puzzle Used " + used.ToString());
		}
		if(Input.GetKeyDown(KeyCode.S))
		{
			print ("Selected Puzzle ID " + ID.ToString());
			print ("moveAmount " + moveAmount.ToString());
		}
		#endregion
	}
	#endregion


	public void SetColor(int colorIdx,Material mat)
	{
		colorNo = colorIdx;
		this.renderer.material = mat;
	}

	public void MoveAmountClear()
	{
		oldPos = transform.position;
		moveAmount = Vector3.zero;
	}

	public void MoveAmountClear(Vector3 referencePos)
	{
		oldPos = referencePos;
		moveAmount = Vector3.zero;
	}


}
