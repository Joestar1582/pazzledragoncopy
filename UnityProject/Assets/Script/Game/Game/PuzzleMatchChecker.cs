using UnityEngine;
using System.Collections;

public static class PuzzleMatchChecker {
	#region Matching Puzzles
	public static bool hasCombo(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam)
	{
		bool checkMatch = false;

		foreach(var pieceObject in puzzleData.pieceObjectList)
		{
			PuzzlePiece targetPuzzle = pieceObject.GetComponent<PuzzlePiece>();
			PuzzlePiece nextPuzzle;
			int numCombo;
			int limitBorder;
			if(targetPuzzle.used)
			{
				// Check Column
				numCombo = 1;
				limitBorder = puzzleParam.maxColumns - (puzzleParam.standardCombo - 1);
				if(targetPuzzle.ID % puzzleParam.maxColumns < limitBorder)
				{
					// Search to the last column
					for(int i = 1;
					    PuzzleCalculator.PieceLineNo(puzzleParam,(targetPuzzle.ID)) == PuzzleCalculator.PieceLineNo(puzzleParam,(i + targetPuzzle.ID));
					    i++,numCombo++)
					{
						nextPuzzle = puzzleData.FindPiece(i + targetPuzzle.ID);
						if(targetPuzzle.type != nextPuzzle.type)
							break;
					}
					if(numCombo >= puzzleParam.standardCombo)
					{
						// Matched
						checkMatch = true;
						targetPuzzle.used = false;
						for(int j = 1;j < numCombo;j++)
						{
							nextPuzzle = puzzleData.FindPiece(j + targetPuzzle.ID);
							nextPuzzle.used = false;
						}
					}
				}
				
				// Check Line
				numCombo = 1;
				limitBorder = puzzleParam.maxLines - (puzzleParam.standardCombo - 1);
				if(PuzzleCalculator.PieceLineNo(puzzleParam,targetPuzzle.ID) < limitBorder)
				{
					// Search to the last line
					for(int i = puzzleParam.maxColumns;
					    PuzzleCalculator.PieceColumnNo(puzzleParam,(targetPuzzle.ID)) == PuzzleCalculator.PieceColumnNo(puzzleParam,(i + targetPuzzle.ID));
					    i += puzzleParam.maxColumns,numCombo++)
					{
						nextPuzzle = puzzleData.FindPiece(i + targetPuzzle.ID);
						if(targetPuzzle.type != nextPuzzle.type)
							break;
					}

					if(numCombo >= puzzleParam.standardCombo)
					{
						// Matched
						checkMatch = true;
						targetPuzzle.used = false;
						for(int j = 1;j < numCombo;j++)
						{
							nextPuzzle = puzzleData.FindPiece(j * puzzleParam.maxColumns + targetPuzzle.ID);
							nextPuzzle.used = false;
						}
					}
				}
			}
		}
		return checkMatch;
	}
	#endregion
}
