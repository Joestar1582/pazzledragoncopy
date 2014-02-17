using UnityEngine;
using System.Collections;

public static class PuzzleMatchChecker {
	#region Matching Puzzles
	public static bool Check(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam)
	{
		bool checkMatch = false;
		for(int puzzleNo = 0; puzzleNo < puzzleParam.maxPuzzles;puzzleNo++)
		{
			PuzzlePiece targetPuzzle = puzzleData.puzzleObjectList[puzzleNo].GetComponent<PuzzlePiece>();
			int combo = 0;
			GameObject nextPuzzleObj;
			PuzzlePiece nextPuzzle;

			// Check Column
			if(targetPuzzle.ID % puzzleParam.maxColumns < puzzleParam.maxColumns - 2)
			{
				for(int i = 1;(i + targetPuzzle.ID) % puzzleParam.maxColumns <= puzzleParam.maxColumns - 1;i++,combo++)
				{
					nextPuzzleObj = puzzleData.puzzleObjectList[PuzzleManager.SearchPuzzleNo(puzzleData.puzzleObjectList,puzzleParam,i + targetPuzzle.ID)];
					nextPuzzle = nextPuzzleObj.GetComponent<PuzzlePiece>();
					if(targetPuzzle.colorNo != nextPuzzle.colorNo)
						break;
				}
				if(combo >= 2)
				{
					checkMatch = true;
					targetPuzzle.used = false;
					targetPuzzle.MoveAmountClear();
					puzzleData.puzzleObjectList[puzzleNo].renderer.enabled = false;
					for(int j = 1;j <= combo;j++)
					{
						nextPuzzleObj = puzzleData.puzzleObjectList[PuzzleManager.SearchPuzzleNo(puzzleData.puzzleObjectList,puzzleParam,targetPuzzle.ID + j)];
						nextPuzzle = nextPuzzleObj.GetComponent<PuzzlePiece>();
						nextPuzzle.used = false;
						nextPuzzle.MoveAmountClear();
						nextPuzzleObj.renderer.enabled = false;
					}
				}
			}

			// Check Line
			combo = 0;
			if(targetPuzzle.ID / puzzleParam.maxColumns < puzzleParam.maxLines - 2)
			{
				for(int i = puzzleParam.maxColumns;(i + targetPuzzle.ID) / puzzleParam.maxColumns <= puzzleParam.maxLines - 1;i += puzzleParam.maxColumns,combo++)
				{
					nextPuzzleObj = puzzleData.puzzleObjectList[PuzzleManager.SearchPuzzleNo(puzzleData.puzzleObjectList,puzzleParam,i + targetPuzzle.ID)];
					nextPuzzle = nextPuzzleObj.GetComponent<PuzzlePiece>();
					if(targetPuzzle.colorNo != nextPuzzle.colorNo)
						break;
				}
				if(combo >= 2)
				{
//					print ("Puzzle" + targetPuzzle.ID.ToString() + " is " + (combo+1).ToString() + "combo in Line");
					checkMatch = true;
					targetPuzzle.used = false;
					targetPuzzle.MoveAmountClear();
					puzzleData.puzzleObjectList[puzzleNo].renderer.enabled = false;
					for(int j = 1;j <= combo;j++)
					{
						nextPuzzleObj = puzzleData.puzzleObjectList[PuzzleManager.SearchPuzzleNo(puzzleData.puzzleObjectList,puzzleParam,targetPuzzle.ID + j * puzzleParam.maxColumns)];
						nextPuzzle = nextPuzzleObj.GetComponent<PuzzlePiece>();
						nextPuzzle.used = false;
						nextPuzzle.MoveAmountClear();
						nextPuzzleObj.renderer.enabled = false;
					}
				}
			}

		}
		return checkMatch;
	}
	#endregion
}
