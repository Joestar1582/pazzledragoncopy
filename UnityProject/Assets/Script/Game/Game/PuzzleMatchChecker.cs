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
			PuzzlePiece nextPuzzle;
			int numCombo;
			int limitBorder;

			// Check Column
			numCombo = 1;
			limitBorder = puzzleParam.maxColumns - (puzzleParam.standardCombo - 1);
			if(targetPuzzle.ID % puzzleParam.maxColumns < limitBorder)
			{
				// Search to the last column
				for(int i = 1;(i + targetPuzzle.ID) % puzzleParam.maxColumns < puzzleParam.maxColumns;i++,numCombo++)
				{
					nextPuzzle = puzzleData.FindPiece(i + targetPuzzle.ID);
					if(targetPuzzle.type != nextPuzzle.type)
						break;
				}
				if(numCombo >= puzzleParam.standardCombo)
				{
					checkMatch = true;
					targetPuzzle.Stop();
					for(int j = 1;j < numCombo;j++)
					{
						nextPuzzle = puzzleData.FindPiece(j + targetPuzzle.ID);
						nextPuzzle.Stop();
					}
				}
			}
			
			// Check Line
			numCombo = 1;
			limitBorder = puzzleParam.maxLines - (puzzleParam.standardCombo - 1);
			if(targetPuzzle.ID / puzzleParam.maxColumns < limitBorder)
			{
				// Search to the last line
				for(int i = puzzleParam.maxColumns;(i + targetPuzzle.ID) / puzzleParam.maxColumns <= puzzleParam.maxLines - 1;i += puzzleParam.maxColumns,numCombo++)
				{
					nextPuzzle = puzzleData.FindPiece(i + targetPuzzle.ID);
					if(targetPuzzle.type != nextPuzzle.type)
						break;
				}
				if(numCombo >= puzzleParam.standardCombo)
				{
					checkMatch = true;
					targetPuzzle.Stop();
					for(int j = 1;j < numCombo;j++)
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
