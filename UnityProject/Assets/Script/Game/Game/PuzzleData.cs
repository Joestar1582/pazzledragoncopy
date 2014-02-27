using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#region Data set of puzzle
public class PuzzleData
{
	public 		List<GameObject>		pieceObjectList;
	public 		List<string>			selectedPieceNameList;
	public		int						selectedPieceNo;
	public		float					selectTime;

	#region Construct
	public PuzzleData()
	{
		pieceObjectList			= new List<GameObject>();
		selectedPieceNameList	= new List<string>();
		selectedPieceNo			= 0;
		selectTime				= 0;
	}
	#endregion
	
	#region Find target`s ID from pieceObjectList
	public int FindPieceObjectIndex(int targetID)
	{
		return pieceObjectList.FindIndex(item => item.GetComponent<PuzzlePiece>().ID == targetID);
	}
	public int FindPieceObjectIndex(string name)
	{
		return pieceObjectList.FindIndex(item => item.name == name);
	}
	#endregion


	#region Find target`s gameobject from pieceObjectList
	public GameObject FindPieceObject(int targetID)
	{
		return pieceObjectList.Find(item => item.GetComponent<PuzzlePiece>().ID == targetID);
	}
	#endregion
	
	#region Find target`s PuzzlePiece from pieceObjectList
	public PuzzlePiece FindPiece(int targetID)
	{
		return pieceObjectList.Find(item => item.GetComponent<PuzzlePiece>().ID == targetID).GetComponent<PuzzlePiece>();
	}
	public PuzzlePiece FindPiece(string name)
	{
		return pieceObjectList.Find(item => item.name == name).GetComponent<PuzzlePiece>();
	}
	#endregion

	
	#region Sort pieceObjectList where ID
	public void Sort()
	{
		pieceObjectList.Sort((a, b) => a.GetComponent<PuzzlePiece>().ID - b.GetComponent<PuzzlePiece>().ID);
	}
	#endregion

	#region Select Action
	public void UnselectAll()
	{
		pieceObjectList.ForEach((GameObject pieceObject) => pieceObject.GetComponent<PuzzlePiece>().selected = false);
	}

	public void AvailableAll()
	{
		pieceObjectList.ForEach((GameObject pieceObject) => pieceObject.GetComponent<PuzzlePiece>().Resume());
	}
	#endregion
	
};
#endregion

