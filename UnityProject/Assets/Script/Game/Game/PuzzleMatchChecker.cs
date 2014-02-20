using UnityEngine;
using System.Collections;

public static class PuzzleMatchChecker {
	#region Matching Puzzles
	public static bool Check(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam)
	{
		bool checkMatch = false;

		foreach(var pieceObject in puzzleData.pieceObjectList)
		{
			PuzzlePiece targetPuzzle = pieceObject.GetComponent<PuzzlePiece>();
			int numCombo = 0;
			PuzzlePiece nextPuzzle;
			
			// Check Column
			if(targetPuzzle.ID % puzzleParam.maxColumns < puzzleParam.maxColumns - 2)
			{
				for(int i = 1;(i + targetPuzzle.ID) % puzzleParam.maxColumns < puzzleParam.maxColumns;i++,numCombo++)
				{
					nextPuzzle = puzzleData.FindPiece(i + targetPuzzle.ID);
					if(targetPuzzle.type != nextPuzzle.type)
						break;
				}
				if(numCombo >= 2)
				{
					checkMatch = true;
					targetPuzzle.Stop();
					for(int j = 1;j <= numCombo;j++)
					{
						nextPuzzle = puzzleData.FindPiece(j + targetPuzzle.ID);
						nextPuzzle.Stop();
					}
				}
			}
			
			// Check Line
			numCombo = 0;
			if(targetPuzzle.ID / puzzleParam.maxColumns < puzzleParam.maxLines - 2)
			{
				for(int i = puzzleParam.maxColumns;(i + targetPuzzle.ID) / puzzleParam.maxColumns <= puzzleParam.maxLines - 1;i += puzzleParam.maxColumns,numCombo++)
				{
					nextPuzzle = puzzleData.FindPiece(i + targetPuzzle.ID);
					if(targetPuzzle.type != nextPuzzle.type)
						break;
				}
				if(numCombo >= 2)
				{
					checkMatch = true;
					targetPuzzle.Stop();
					for(int j = 1;j <= numCombo;j++)
					{
						nextPuzzle = puzzleData.FindPiece(j * puzzleParam.maxColumns + targetPuzzle.ID);
						nextPuzzle.Stop();
					}
				}
			}
		}
		return checkMatch;
	}
	#endregion
}
