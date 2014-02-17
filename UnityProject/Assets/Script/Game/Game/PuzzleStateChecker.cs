using UnityEngine;
using System.Collections;

public class PuzzleStateChecker  {

	#region Puzzle check you have selected.
	public static void SelectedPuzzlePiece(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam,PuzzleData.STATE nextState)
	{
		for(int puzzleNo = 0; puzzleNo < puzzleParam.maxPuzzles;puzzleNo++)
		{
			if(puzzleData.puzzleObjectList[puzzleNo].GetComponent<PuzzlePiece>().selected)
			{
				puzzleData.state = nextState;
				puzzleData.selectedPuzzleNo = puzzleNo;
				return;
			}
		}
	}
	#endregion

	#region Puzzle check you have NOT selected.
	public static void UnselectedPuzzlePiece(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam,PuzzleData.STATE nextState)
	{
		for(int puzzleNo = 0; puzzleNo < puzzleParam.maxPuzzles;puzzleNo++)
		{
			if(puzzleData.puzzleObjectList[puzzleNo].GetComponent<PuzzlePiece>().selected)
				return;
		}
		puzzleData.state = nextState;
	}
	#endregion
	

	#region Check if there is any leakage of the puzzle ID
	public static void CheckPuzzleIDLeakage(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam)
	{
		PuzzlePiece targetPuzzle;
		for(int puzzleNo = 0; puzzleNo < puzzleParam.maxPuzzles;puzzleNo++)
		{
			targetPuzzle = puzzleData.puzzleObjectList[puzzleNo].GetComponent<PuzzlePiece>();
			if(targetPuzzle.ID < 0 || targetPuzzle.ID >=  puzzleParam.maxPuzzles)
			{
				Debug.Log ("Puzzle" + puzzleData.puzzleObjectList[puzzleNo].name + " is a leak of the ID");
			}
		}
	}
	#endregion

}
