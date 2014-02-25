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

			if(targetPuzzle.used)
			{
				int targetColumNo 	= PuzzleCalculator.PieceColumnNo(puzzleParam,(targetPuzzle.ID));
				int targetLineNo 	= PuzzleCalculator.PieceLineNo(puzzleParam,targetPuzzle.ID);
				int targetIndex;

				// Check Column
				int numCombo 		= 1;
				int limitBorder 	= PuzzleCalculator.ColumnLimitBorder(puzzleParam);
				if(targetColumNo < limitBorder)
				{
					// Search to the last column
					for(int i = 1;
					    targetLineNo == PuzzleCalculator.PieceLineNo(puzzleParam,(i + targetPuzzle.ID));
					    i++,numCombo++)
					{
						targetIndex = i + targetPuzzle.ID;
						if(PuzzleStateChecker.hasIndexOutOfRange(puzzleData,targetIndex))
							break;

						nextPuzzle = puzzleData.FindPiece(i + targetPuzzle.ID);
						if(targetPuzzle.type != nextPuzzle.type)
							break;
					}
					if(numCombo >= puzzleParam.standardCombo)
					{
						// Matched
						checkMatch 			= true;
						targetPuzzle.used 	= false;
						for(int j = 1;j < numCombo;j++)
						{
							targetIndex	= j + targetPuzzle.ID;
							if(PuzzleStateChecker.hasIndexOutOfRange(puzzleData,targetIndex) == false)
							{
								nextPuzzle 		= puzzleData.FindPiece(targetIndex);
								nextPuzzle.used = false;
							}
						}
					}
				}
				
				// Check Line
				numCombo 			= 1;
				limitBorder 		= PuzzleCalculator.LineLimitBorder(puzzleParam);
				if(targetLineNo < limitBorder)
				{
					// Search to the last line
					for(int i = puzzleParam.maxColumns;
					    targetColumNo == PuzzleCalculator.PieceColumnNo(puzzleParam,(i + targetPuzzle.ID));
					    i += puzzleParam.maxColumns,numCombo++)
					{
						targetIndex = i + targetPuzzle.ID;
						if(PuzzleStateChecker.hasIndexOutOfRange(puzzleData,targetIndex))
							break;

						nextPuzzle = puzzleData.FindPiece(targetIndex);
						if(targetPuzzle.type != nextPuzzle.type)
							break;
					}

					if(numCombo >= puzzleParam.standardCombo)
					{
						// Matched
						checkMatch 			= true;
						targetPuzzle.used 	= false;
						for(int j = 1;j < numCombo;j++)
						{
							targetIndex = j * puzzleParam.maxColumns + targetPuzzle.ID;
							if(PuzzleStateChecker.hasIndexOutOfRange(puzzleData,targetIndex) == false)
							{
								nextPuzzle 		= puzzleData.FindPiece(targetIndex);
								nextPuzzle.used = false;
							}
						}
					}
				}
			}
		}
		return checkMatch;
	}
	#endregion
}
