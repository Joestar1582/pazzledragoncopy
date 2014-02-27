using UnityEngine;
using System.Collections;

public static class PuzzleMatchChecker {
	#region Matching Puzzles
	public static bool HasMatch(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam)
	{
		bool hasMatch = false;

		foreach(var pieceObject in puzzleData.pieceObjectList)
		{
			PuzzlePiece nowPiece = pieceObject.GetComponent<PuzzlePiece>();
			PuzzlePiece nextPiece;

			if(nowPiece.used)
			{
				int targetColumNo 	= PuzzleCalculator.GetPieceColumnNo(puzzleParam,(nowPiece.ID));
				int targetRowNo 	= PuzzleCalculator.GetPieceRowNo(puzzleParam,nowPiece.ID);
				int targetIndex;

				// Check Column
				int numCombo 		= 1;
				int limitBorder 	= PuzzleCalculator.GetColumnLimitBorder(puzzleParam);
				if(targetColumNo < limitBorder)
				{
					// Search to the last column
					for(int i = 1;
					    targetRowNo == PuzzleCalculator.GetPieceRowNo(puzzleParam,(i + nowPiece.ID));
					    i++,numCombo++)
					{
						targetIndex = i + nowPiece.ID;
						if(PuzzleStateChecker.HasIndexOutOfRange(puzzleData,targetIndex))
							break;

						nextPiece = puzzleData.FindPiece(i + nowPiece.ID);
						if(nowPiece.type != nextPiece.type)
							break;
					}
					if(numCombo >= puzzleParam.standardCombo)
					{
						// Matched
						hasMatch 			= true;
						nowPiece.used 	= false;
						for(int j = 1;j < numCombo;j++)
						{
							targetIndex	= j + nowPiece.ID;
							if(PuzzleStateChecker.HasIndexOutOfRange(puzzleData,targetIndex) == false)
							{
								nextPiece 		= puzzleData.FindPiece(targetIndex);
								nextPiece.used = false;
							}
						}
					}
				}
				
				// Check Row
				numCombo 			= 1;
				limitBorder 		= PuzzleCalculator.GetRowLimitBorder(puzzleParam);
				if(targetRowNo < limitBorder)
				{
					// Search to the last line
					for(int i = puzzleParam.maxColumns;
					    targetColumNo == PuzzleCalculator.GetPieceColumnNo(puzzleParam,(i + nowPiece.ID));
					    i += puzzleParam.maxColumns,numCombo++)
					{
						targetIndex = i + nowPiece.ID;
						if(PuzzleStateChecker.HasIndexOutOfRange(puzzleData,targetIndex))
							break;

						nextPiece = puzzleData.FindPiece(targetIndex);
						if(nowPiece.type != nextPiece.type)
							break;
					}

					if(numCombo >= puzzleParam.standardCombo)
					{
						// Matched
						hasMatch 			= true;
						nowPiece.used 	= false;
						for(int j = 1;j < numCombo;j++)
						{
							targetIndex = j * puzzleParam.maxColumns + nowPiece.ID;
							if(PuzzleStateChecker.HasIndexOutOfRange(puzzleData,targetIndex) == false)
							{
								nextPiece 		= puzzleData.FindPiece(targetIndex);
								nextPiece.used = false;
							}
						}
					}
				}
			}
		}
		return hasMatch;
	}
	#endregion
}
