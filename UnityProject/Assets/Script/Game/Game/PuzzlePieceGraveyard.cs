using UnityEngine;
using System.Collections;
using System.Linq;

public static class PuzzlePieceGraveyard{

	#region Create NEW Puzzle to empty area
	public static bool IsCompletedDestroy(ref PuzzleData puzzleData)
	{
		bool finishedDelete = false;

		// Delete preferentially from the puzzle that has been traced
		if(puzzleData.selectedPieceNameList.Count != 0)
		{
			// Select item that has been registered
			PuzzlePiece registeredPiece = puzzleData.FindPiece(puzzleData.selectedPieceNameList[0]);
			// Remove the first item
			puzzleData.selectedPieceNameList.RemoveAt(0);

			puzzleData.pieceObjectList.ForEach((GameObject pieceObject) => 
			{
				PuzzlePiece targetPiece = pieceObject.GetComponent<PuzzlePiece>();
				if(registeredPiece.type == targetPiece.type)
				{
					if(targetPiece.used == false)
						targetPiece.Stop();
				}
			});
		}
		else
		{
			puzzleData.pieceObjectList.ForEach((GameObject pieceObject) => 
			{
				PuzzlePiece targetPiece = pieceObject.GetComponent<PuzzlePiece>();
				if(targetPiece.used == false)
					targetPiece.Stop();
			});
			finishedDelete = true;
		}

		return finishedDelete;
	}
	#endregion



}
