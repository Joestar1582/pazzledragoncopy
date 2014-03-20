using UnityEngine;
using System.Collections;

public class PuzzlePiece : MonoBehaviour {
	public 		int 		ID			= 999;
	public 		bool 		selected	= false;
	public 		bool		used		= false;
	public 		bool		canSelect	= true;
	public 		int 		type		= 999;
	public 		Vector3 	moveAmount;	
	public		int			chaineID	= -1;

	private 	Vector3 	screenPoint;
    private 	Vector3 	offset;
	private 	Vector3 	oldPos;

	
	# region Mouse Action
    void OnMouseDown() {
		if(canSelect)
		{
	       	this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
	        this.offset = transform.position - 
			Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
			oldPos = transform.position;
			selected = true;
		}
    }
	
    void OnMouseUp() {
		selected = false;
    }

	void OnMouseDrag() {
		if(selected)
		{
	    	Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
	        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
	       	transform.position = currentPosition;
			selected = true;
			moveAmount = currentPosition - oldPos;
		}
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

	public void Move(Vector3 startPos,Vector3 endPos,float time)
	{
		gameObject.transform.position = startPos;
		iTween.MoveTo(gameObject,iTween.Hash("position",endPos,"time",time));
	}

	public void Resume()
	{
		used = true;
		gameObject.renderer.enabled = true;
//		iTweenEvent.GetEvent(gameObject,"ColorToClear").Play();
//		iTweenEvent.GetEvent(gameObject,"ScaleToBig").Play();
		MoveAmountClear();
	}

	public void Stop()
	{
		used = false;
		chaineID = -1;
		gameObject.renderer.enabled = false;
//		iTweenEvent.GetEvent(gameObject,"ColorToZero").Play();
//		iTweenEvent.GetEvent(gameObject,"ScaleToSmall").Play();
		MoveAmountClear();
	}

	#endregion

}
