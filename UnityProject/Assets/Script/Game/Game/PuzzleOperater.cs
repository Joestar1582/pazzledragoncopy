using UnityEngine;
using System.Collections;
using System.Linq;

public static class PuzzleOperater {
	
	#region Sort automatically puzzle,reffering to empty
	public static void SortByRefEmpty(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam)
	{
		// Rearranged in ascending order of ID puzzles
		puzzleData.Sort();

		for(int puzzleNo = puzzleData.pieceObjectList.Count - 1; puzzleNo >= 0;puzzleNo--)
		{
			Vector3 puzzlePos = Vector3.zero;
			PuzzlePiece nowPiece = puzzleData.pieceObjectList[puzzleNo].GetComponent<PuzzlePiece>();
			
			// It moves one on the target is empty
			int targetRowNo = PuzzleCalculator.GetPieceRowNo(puzzleParam,nowPiece.ID);
			if(targetRowNo < puzzleParam.maxRows - 1)
			{
				// Check until it can move
				for(int id = nowPiece.ID + puzzleParam.maxColumns;id < puzzleParam.maxPuzzles;id += puzzleParam.maxColumns)
				{
					GameObject emptyTemp = puzzleData.pieceObjectList[puzzleData.FindPieceObjectIndex(id)];
					PuzzlePiece emptyPuzzle = emptyTemp.GetComponent<PuzzlePiece>();
					if(emptyPuzzle.used == false)
					{
						emptyPuzzle.ID 	= nowPiece.ID;
						nowPiece.ID = id;
						emptyPuzzle.Stop();
						emptyTemp.transform.position = PuzzleCalculator.GetPiecePosition(puzzleParam,emptyPuzzle.ID);
					}
				}
			}

			nowPiece.MoveAmountClear();

			// Move puzzle piece
			Vector3 nowPos 		= puzzleData.pieceObjectList[puzzleNo].transform.position;
			Vector3 targetPos 	= PuzzleCalculator.GetPiecePosition(puzzleParam,nowPiece.ID);
			if(Vector3.Distance(nowPos,targetPos) > PuzzleCalculator.GetPieceSpaceOffset(puzzleParam))
				nowPiece.Move(targetPos,puzzleParam.moveTime);
		}
	}
	#endregion

	#region Sort automatically puzzle ,reffering to ID
	public static void SortByRefID(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam)
	{
		// Rearranged in ascending order of ID puzzles
		puzzleData.Sort();

		puzzleData.pieceObjectList.ForEach((GameObject pieceObject) => 
		{
			PuzzlePiece targetPiece = pieceObject.GetComponent<PuzzlePiece>();
			Vector3 targetPos = PuzzleCalculator.GetPiecePosition(puzzleParam,targetPiece.ID);
			targetPiece.Move(targetPos,puzzleParam.moveTime);

		});
	}
	#endregion


	#region Operate Puzzles
	public static void Operate(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam)
	{
		PuzzlePiece selectedPiece 	= puzzleData.pieceObjectList[puzzleData.selectedPieceNo].GetComponent<PuzzlePiece>();
		Vector3 moveAmount 			= selectedPiece.moveAmount;
		float amountRange 			= PuzzleCalculator.GetAmountRangeCrissCross(puzzleParam);
		float amountRangeDiagonal 	= PuzzleCalculator.GetAmountRangeDiagonal(puzzleParam);

	// The amount of movement becomes a constant value,Move it

		// Move diagonal
		if(moveAmount.x >= amountRangeDiagonal && moveAmount.z >= amountRangeDiagonal)
			ChangeID(ref puzzleData,puzzleParam,selectedPiece.ID + 1 + puzzleParam.maxColumns);
		else if(moveAmount.x >= amountRangeDiagonal && moveAmount.z <= -amountRangeDiagonal)
			ChangeID(ref puzzleData,puzzleParam,selectedPiece.ID + 1 - puzzleParam.maxColumns);
		else if(moveAmount.x <= -amountRangeDiagonal && moveAmount.z >= amountRangeDiagonal)
			ChangeID(ref puzzleData,puzzleParam,selectedPiece.ID - 1 + puzzleParam.maxColumns);
		else if(moveAmount.x <= -amountRangeDiagonal && moveAmount.z <= -amountRangeDiagonal)
			ChangeID(ref puzzleData,puzzleParam,selectedPiece.ID - 1 - puzzleParam.maxColumns);
		// Move right.
		else if(moveAmount.x >= amountRange)
			ChangeID(ref puzzleData,puzzleParam,selectedPiece.ID + 1);
		// Move left.
		else if(moveAmount.x <= -amountRange)
			ChangeID(ref puzzleData,puzzleParam,selectedPiece.ID - 1);
		// Move up.
		else if(moveAmount.z >= amountRange)
			ChangeID(ref puzzleData,puzzleParam,selectedPiece.ID + puzzleParam.maxColumns);
		// Move down.
		else if(moveAmount.z <= -amountRange)
			ChangeID(ref puzzleData,puzzleParam,selectedPiece.ID - puzzleParam.maxColumns);
	}	
	#endregion

	#region Change Puzzle Piece ID
	public static void ChangeID(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam,int targetID)
	{
		PuzzlePiece selectedPuzzle = puzzleData.pieceObjectList[puzzleData.selectedPieceNo].GetComponent<PuzzlePiece>();
		PuzzlePiece nowPiece;
		int targetObjectIdx 	= puzzleData.FindPieceObjectIndex(targetID);
		int tempID 				= selectedPuzzle.ID;

		// debug
		if(PuzzleStateChecker.HasIndexOutOfRange(puzzleData,targetObjectIdx))
			return;

		// The amount of movement becomes a constant value,Change ID
		Vector3 amount = puzzleData.pieceObjectList[targetObjectIdx].transform.position - selectedPuzzle.transform.position;
		if( amount.x < puzzleParam.puzzleSpace && amount.x > -puzzleParam.puzzleSpace &&
		   	amount.z < puzzleParam.puzzleSpace && amount.z > -puzzleParam.puzzleSpace )
		{
			nowPiece 		= puzzleData.pieceObjectList[targetObjectIdx].GetComponent<PuzzlePiece>();
			nowPiece.ID	 	= tempID;
			selectedPuzzle.ID 	= targetID;

			selectedPuzzle.MoveAmountClear(PuzzleCalculator.GetPiecePosition(puzzleParam,targetID));
			nowPiece.Move(PuzzleCalculator.GetPiecePosition(puzzleParam,tempID),puzzleParam.moveTime);
		}

		// Register puzzles traced
		puzzleData.selectedPieceNameList.Add(puzzleData.pieceObjectList[targetObjectIdx].name);
		
	}
	#endregion
}
