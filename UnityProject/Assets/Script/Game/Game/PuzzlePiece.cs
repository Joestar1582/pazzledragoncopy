using UnityEngine;
using System.Collections;

public class PuzzlePiece : MonoBehaviour {
	public 		int 		ID			= 999;
	public 		bool 		selected	= false;
	public 		bool		used		= false;
	public 		int 		type		= 999;

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

	#region Set Puzzle Piece Color & Type
	public void SetColor(int typeNo,Material mat)
	{
		type = typeNo;
		this.renderer.material = mat;
	}
	#endregion

	#region Clear MOVE amount
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
	#endregion

	#region Action List
	public void Move(Vector3 targetPos,float time)
	{
		iTween.MoveTo(gameObject,iTween.Hash("position",targetPos,"time",time));
	}

	public void Move(Vector3 initPos,Vector3 targetPos,float time)
	{
		gameObject.transform.position = initPos;
		iTween.MoveTo(gameObject,iTween.Hash("position",targetPos,"time",time));
	}

	public void Resume()
	{
		used = true;
		gameObject.renderer.enabled = true;
		MoveAmountClear();
	}

	public void Stop()
	{
		used = false;
		gameObject.renderer.enabled = false;
		MoveAmountClear();
	}
	#endregion

}
