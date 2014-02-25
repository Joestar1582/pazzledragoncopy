using UnityEngine;
using System.Collections;
using System.Linq;

public static class PuzzleStateChecker  {

	#region Puzzle check player have selected.
	public static void SelectedPuzzlePiece(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam,PuzzleData.STATE nextState)
	{
		foreach(var peiceObject in puzzleData.pieceObjectList.Select((value, index) => new {value,index}))
		{
			if(peiceObject.value.GetComponent<PuzzlePiece>().selected)
			{
				puzzleData.state = nextState;
				puzzleData.selectedPuzzleNo = peiceObject.index;
				return;
			}
		}
	}
	#endregion

	#region Puzzle check player have NOT selected.
	public static void UnselectedPuzzlePiece(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam,PuzzleData.STATE nextState)
	{
		foreach(var peiceObject in puzzleData.pieceObjectList.Select((value, index) => new {value,index}))
		{
			if(peiceObject.value.GetComponent<PuzzlePiece>().selected)
				return;
		}
		puzzleData.state = nextState;
	}
	#endregion
	

	#region Check if PuzzleID is any out of range
	public static void CheckPuzzleIDOutOfRange(PuzzleData puzzleData)
	{
		foreach(var pieceObject in puzzleData.pieceObjectList)
		{
			PuzzlePiece targetPuzzle = pieceObject.GetComponent<PuzzlePiece>();
			if(targetPuzzle.ID < 0 || targetPuzzle.ID >=  puzzleData.pieceObjectList.Count)
				Debug.LogWarning (pieceObject.name + "'s ID is out of range");
		}
	}
	#endregion

	#region Check if Index is any out of range
	public static bool hasIndexOutOfRange(PuzzleData puzzleData,int targetIdx)
	{
		bool check = false;
		if(targetIdx < 0 || targetIdx >=  puzzleData.pieceObjectList.Count)
		{
			Debug.LogWarning ("Index " + targetIdx + " Out of range");
			check = true;
		}
		return check;
	}
	#endregion

	#region Count up selecting piece's time
	public static bool isFinishedSelectTime(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam)
	{
		bool checkTimeUp = false;
		return checkTimeUp;
	}
	#endregion


}
