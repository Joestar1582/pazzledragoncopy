using UnityEngine;
using System.Collections;

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
		PuzzleQuickSort(ref puzzleData,0, puzzleParam.maxPuzzles - 1);
		// Puzzle number that is not in use is through
		for(int puzzleNo = puzzleParam.maxPuzzles - 1; puzzleNo >= 0;puzzleNo--)
		{
			Vector3 puzzlePos = Vector3.zero;
			PuzzlePiece targetPuzzle = puzzleData.puzzleObjectList[puzzleNo].GetComponent<PuzzlePiece>();
			
			// Sort Puzzle ID 
			if(targetPuzzle.ID / puzzleParam.maxColumns < puzzleParam.maxLines - 1)
			{
				for(int id = targetPuzzle.ID + puzzleParam.maxColumns;id < puzzleParam.maxPuzzles;id += puzzleParam.maxColumns)
				{
					GameObject emptyTemp = puzzleData.puzzleObjectList[PuzzleManager.SearchPuzzleNo(puzzleData.puzzleObjectList,puzzleParam,id)];
					PuzzlePiece emptyPuzzle = emptyTemp.GetComponent<PuzzlePiece>();
					if(emptyPuzzle.used == false)
					{
						emptyPuzzle.ID = targetPuzzle.ID;
						targetPuzzle.ID = id;
						emptyPuzzle.MoveAmountClear();
						emptyTemp.transform.position = CalcPuzzlePosition(puzzleParam,emptyPuzzle.ID);
						emptyTemp.renderer.enabled = false;
					}
				}
			}
			// Set Position
			targetPuzzle.MoveAmountClear();
			Vector3 nowPos = puzzleData.puzzleObjectList[puzzleNo].transform.position;
			Vector3 targetPos = CalcPuzzlePosition(puzzleParam,targetPuzzle.ID);
			if(Vector3.Distance(nowPos,targetPos) > puzzleParam.puzzleSpace / 10.0f)
				iTween.MoveTo(puzzleData.puzzleObjectList[puzzleNo],iTween.Hash("position",targetPos,"time",puzzleParam.moveTime));
		}
	}
	#endregion

	#region Move Puzzles
	public static void Move(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam)
	{
		Vector3 moveAmount;
		PuzzlePiece selectedPuzzle = puzzleData.puzzleObjectList[puzzleData.selectedPuzzleNo].GetComponent<PuzzlePiece>();
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
	
	#region Sort Puzzles by QuickSort
	public static void PuzzleQuickSort(ref PuzzleData puzzleData,int start, int end)
	{
		int i 		= start;
		int j 		= end;		
		int pivot	= puzzleData.puzzleObjectList[(start + end) / 2].GetComponent<PuzzlePiece>().ID;
		while(true) 
		{
			while(puzzleData.puzzleObjectList[i].GetComponent<PuzzlePiece>().ID < pivot)
				i++;
			while(pivot < puzzleData.puzzleObjectList[j].GetComponent<PuzzlePiece>().ID)
				j--;
			if (puzzleData.puzzleObjectList[i].GetComponent<PuzzlePiece>().ID >= puzzleData.puzzleObjectList[j].GetComponent<PuzzlePiece>().ID)
				break;
			
			GameObject temp = puzzleData.puzzleObjectList[i];
			puzzleData.puzzleObjectList[i] = puzzleData.puzzleObjectList[j];
			puzzleData.puzzleObjectList[j] = temp;
			i++;
			j--;
		}
		
		if (start < i - 1)
			PuzzleQuickSort(ref puzzleData,start, i - 1);
		if (j + 1 <  end)
			PuzzleQuickSort(ref puzzleData,j + 1, end);
	}
	#endregion

	#region Change Puzzle Piece ID
	public static void ChangeID(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam,int targetID)
	{
		PuzzlePiece selectedPuzzle = puzzleData.puzzleObjectList[puzzleData.selectedPuzzleNo].GetComponent<PuzzlePiece>();
		int targetObjectNo = PuzzleManager.SearchPuzzleNo(puzzleData.puzzleObjectList,puzzleParam,targetID);	
		int tempID = selectedPuzzle.ID;

		// The amount of movement becomes a constant value,Change ID
		Vector3 amount = puzzleData.puzzleObjectList[targetObjectNo].transform.position - selectedPuzzle.transform.position;
		if( amount.x < puzzleParam.puzzleSpace && amount.x > -puzzleParam.puzzleSpace &&
		   amount.z < puzzleParam.puzzleSpace && amount.z > -puzzleParam.puzzleSpace )
		{
			selectedPuzzle.MoveAmountClear(CalcPuzzlePosition(puzzleParam,targetID));
			puzzleData.puzzleObjectList[targetObjectNo].GetComponent<PuzzlePiece>().ID = tempID;
			selectedPuzzle.ID = targetID;
			iTween.MoveTo(puzzleData.puzzleObjectList[targetObjectNo],iTween.Hash("position",CalcPuzzlePosition(puzzleParam,tempID),"time",puzzleParam.moveTime));
		}
	}
	#endregion


}
