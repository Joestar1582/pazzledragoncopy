using UnityEngine;
using System.Collections;
using System.Linq;

public static class PuzzleOperater {

	#region Calc Puzzle Position from ID
	public static Vector3 CalcPuzzlePosition(PuzzleOperaterParam puzzleParam,int id)
	{
		Vector3 puzzlePos;
		puzzlePos.x = ((id % puzzleParam.maxColumns) - (puzzleParam.maxLines / 2) - 1) * puzzleParam.puzzleSpace;
		puzzlePos.y = 0;
		puzzlePos.z = ((id / puzzleParam.maxColumns) - (puzzleParam.maxLines / 2)) * puzzleParam.puzzleSpace;
		return puzzlePos;
	}
	#endregion

	#region Sort automatically puzzle
	public static void Sort(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam)
	{
		// Rearranged in ascending order of ID puzzles
		puzzleData.Sort();
		// Puzzle number that is not in use is through
		for(int puzzleNo = puzzleParam.maxPuzzles - 1; puzzleNo >= 0;puzzleNo--)
		{
			Vector3 puzzlePos = Vector3.zero;
			PuzzlePiece targetPuzzle = puzzleData.pieceObjectList[puzzleNo].GetComponent<PuzzlePiece>();
			
			// Sort Puzzle ID 
			if(targetPuzzle.ID / puzzleParam.maxColumns < puzzleParam.maxLines - 1)
			{
				for(int id = targetPuzzle.ID + puzzleParam.maxColumns;id < puzzleParam.maxPuzzles;id += puzzleParam.maxColumns)
				{
					GameObject emptyTemp = puzzleData.pieceObjectList[puzzleData.FindPieceObjectIndex(id)];
					PuzzlePiece emptyPuzzle = emptyTemp.GetComponent<PuzzlePiece>();
					if(emptyPuzzle.used == false)
					{
						emptyPuzzle.ID = targetPuzzle.ID;
						targetPuzzle.ID = id;
						emptyPuzzle.Stop();
						emptyTemp.transform.position = CalcPuzzlePosition(puzzleParam,emptyPuzzle.ID);
					}
				}
			}

			targetPuzzle.MoveAmountClear();

			// Move puzzle piece
			Vector3 nowPos = puzzleData.pieceObjectList[puzzleNo].transform.position;
			Vector3 targetPos = CalcPuzzlePosition(puzzleParam,targetPuzzle.ID);
			if(Vector3.Distance(nowPos,targetPos) > puzzleParam.puzzleSpace / 10.0f)
				targetPuzzle.Move(targetPos,puzzleParam.moveTime);
		}
	}
	#endregion

	#region Operate Puzzles
	public static void Operate(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam)
	{
		Vector3 moveAmount;
		PuzzlePiece selectedPuzzle = puzzleData.pieceObjectList[puzzleData.selectedPuzzleNo].GetComponent<PuzzlePiece>();
		moveAmount = selectedPuzzle.moveAmount;
		
		float recognitionRange = puzzleParam.puzzleSpace / 1.2f;
		float recognitionRangeDiagonal = puzzleParam.puzzleSpace / 2.0f;

	// The amount of movement becomes a constant value,Move it
		// Move diagonal
		if(moveAmount.x >= recognitionRangeDiagonal && moveAmount.z >= recognitionRangeDiagonal)
			ChangeID(ref puzzleData,puzzleParam,selectedPuzzle.ID + 1 + puzzleParam.maxColumns);
		else if(moveAmount.x >= recognitionRangeDiagonal && moveAmount.z <= -recognitionRangeDiagonal)
			ChangeID(ref puzzleData,puzzleParam,selectedPuzzle.ID + 1 - puzzleParam.maxColumns);
		else if(moveAmount.x <= -recognitionRangeDiagonal && moveAmount.z >= recognitionRangeDiagonal)
			ChangeID(ref puzzleData,puzzleParam,selectedPuzzle.ID - 1 + puzzleParam.maxColumns);
		else if(moveAmount.x <= -recognitionRangeDiagonal && moveAmount.z <= -recognitionRangeDiagonal)
			ChangeID(ref puzzleData,puzzleParam,selectedPuzzle.ID - 1 - puzzleParam.maxColumns);
		// Move right.
		else if(moveAmount.x >= recognitionRange)
			ChangeID(ref puzzleData,puzzleParam,selectedPuzzle.ID + 1);
		// Move left.
		else if(moveAmount.x <= -recognitionRange)
			ChangeID(ref puzzleData,puzzleParam,selectedPuzzle.ID - 1);
		// Move up.
		else if(moveAmount.z >= recognitionRange)
			ChangeID(ref puzzleData,puzzleParam,selectedPuzzle.ID + puzzleParam.maxColumns);
		// Move down.
		else if(moveAmount.z <= -recognitionRange)
			ChangeID(ref puzzleData,puzzleParam,selectedPuzzle.ID - puzzleParam.maxColumns);
	}	
	#endregion

	#region Change Puzzle Piece ID
	public static void ChangeID(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam,int targetID)
	{
		PuzzlePiece selectedPuzzle = puzzleData.pieceObjectList[puzzleData.selectedPuzzleNo].GetComponent<PuzzlePiece>();
		PuzzlePiece targetPuzzle;
		int targetObjectIdx = puzzleData.FindPieceObjectIndex(targetID);
		int tempID = selectedPuzzle.ID;

		// debug
		PuzzleStateChecker.CheckPuzzleObjectIndexLeakage(puzzleData,puzzleParam,targetObjectIdx);

		// The amount of movement becomes a constant value,Change ID
		Vector3 amount = puzzleData.pieceObjectList[targetObjectIdx].transform.position - selectedPuzzle.transform.position;
		if( amount.x < puzzleParam.puzzleSpace && amount.x > -puzzleParam.puzzleSpace &&
		   amount.z < puzzleParam.puzzleSpace && amount.z > -puzzleParam.puzzleSpace )
		{
			targetPuzzle = puzzleData.pieceObjectList[targetObjectIdx].GetComponent<PuzzlePiece>();
			targetPuzzle.ID = tempID;
			selectedPuzzle.ID = targetID;

			selectedPuzzle.MoveAmountClear(CalcPuzzlePosition(puzzleParam,targetID));
			targetPuzzle.Move(CalcPuzzlePosition(puzzleParam,tempID),puzzleParam.moveTime);
		}
	}
	#endregion


}
