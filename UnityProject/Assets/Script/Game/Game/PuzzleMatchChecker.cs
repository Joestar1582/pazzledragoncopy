using UnityEngine;
using System;
using System.Collections;

public static class PuzzleMatchChecker {
	#region Matching Puzzles
	public static bool HasMatch(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam)
	{
		bool hasMatch = false;

		foreach(var pieceObject in puzzleData.pieceObjectList)
		{
			PuzzlePiece nowPiece = pieceObject.GetComponent<PuzzlePiece>();

			if(nowPiece.used)
			{
				int nowColumNo 			= PuzzleCalculator.GetPieceColumnNo(puzzleParam,(nowPiece.ID));
				int nowRowNo 			= PuzzleCalculator.GetPieceRowNo(puzzleParam,nowPiece.ID);
				int rowLimitBorder		= PuzzleCalculator.GetRowLimitBorder(puzzleParam);
				int columnLimitBorder	= PuzzleCalculator.GetColumnLimitBorder(puzzleParam);
				int numChaine 			= 1;

				// Check Column
				if(nowColumNo < columnLimitBorder)
				{
					// Search to the last column
					for(int i = 1;
					    nowRowNo == PuzzleCalculator.GetPieceRowNo(puzzleParam,(i + nowPiece.ID));
					    i++,numChaine++)
					{
						int targetIndex = i + nowPiece.ID;
						if(HasNoChaine(puzzleData,nowPiece.ID,targetIndex))
							break;
					}
					if(numChaine >= puzzleParam.stdNumChaines)
					{
						Matched(nowPiece.ID,numChaine,ref puzzleData,(nextIndex) => nextIndex + nowPiece.ID);
						hasMatch = true;
					}
				}
				
				// Check Row
				numChaine = 1;
				if(nowRowNo < rowLimitBorder)
				{
					// Search to the last line
					int targetIndex;
					for(int i = puzzleParam.maxColumns;
					    nowColumNo == PuzzleCalculator.GetPieceColumnNo(puzzleParam,(i + nowPiece.ID));
					    i += puzzleParam.maxColumns,numChaine++)
					{
						targetIndex = i + nowPiece.ID;
						if(HasNoChaine(puzzleData,nowPiece.ID,targetIndex))
							break;
					}

					if(numChaine >= puzzleParam.stdNumChaines)
					{
						Matched(nowPiece.ID,numChaine,ref  puzzleData,(nextIndex) => nextIndex * puzzleParam.maxColumns + nowPiece.ID);
						hasMatch = true;
					}
				}
			}
		}
		return hasMatch;
	}

	#endregion

	#region Cheak Matching
	private static bool HasNoChaine(PuzzleData puzzleData,int nowIndex,int nextIndex)
	{
		if(PuzzleStateChecker.HasIndexOutOfRange(puzzleData,nextIndex))
			return true;
		
		PuzzlePiece nowPiece 	= puzzleData.FindPiece(nowIndex);
		PuzzlePiece nextPiece 	= puzzleData.FindPiece(nextIndex);
		if(nowPiece.type != nextPiece.type)
			return true;

		return false;
	}
	#endregion

	#region Matched
	private static void Matched(int nowIndex,int numChaine,ref PuzzleData puzzleData,Func<int,int> NextIndex)
	{
		PuzzlePiece nowPiece 	= puzzleData.FindPiece(nowIndex);
		nowPiece.used 			= false;

		puzzleData.numChaine++;

		if(nowPiece.chaineID < 0)
			nowPiece.chaineID	= puzzleData.numChaine;

		for(int i = 1;i < numChaine;i++)
		{
			int nextIndex = NextIndex(i);
			if(PuzzleStateChecker.HasIndexOutOfRange(puzzleData,nextIndex) == false)
			{
				PuzzlePiece nextPiece 	= puzzleData.FindPiece(nextIndex);
				nextPiece.used 			= false;
				
				// Connect the chain if you are chained already
				if(nextPiece.chaineID < 0)
					nextPiece.chaineID	= puzzleData.numChaine;
				else
					puzzleData.SetChaine(nextPiece.chaineID,nowPiece.chaineID);
			}
		}
	}
	#endregion
}
