using UnityEngine;
using System.Collections;
using System.Linq;

public static class PuzzleStateChecker  {

	#region Puzzle check player have selected.
	public static bool IsSelectedPiece(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam)
	{
		bool existsSelectedPiece = false;
		foreach(var peiceObject in puzzleData.pieceObjectList.Select((piece, index) => new {piece,index}))
		{
			if(peiceObject.piece.GetComponent<PuzzlePiece>().selected)
			{
				puzzleData.selectedPieceNo = peiceObject.index;
				existsSelectedPiece = true;
				break;
			}
		}
		return existsSelectedPiece;
	}
	#endregion
	
	#region Check if Index is any out of range
	public static bool HasIndexOutOfRange(PuzzleData puzzleData,int targetIdx)
	{
		bool isError = false;
		if(targetIdx < 0 || targetIdx >=  puzzleData.pieceObjectList.Count)
		{
			Debug.LogWarning ("Index " + targetIdx + " Out of range");
			isError = true;
		}
		return isError;
	}
	#endregion

	#region Count up selecting piece's time
	public static bool IsFinishedSelectTime(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam)
	{
		bool isFinished = false;
		return isFinished;
	}
	#endregion


}
