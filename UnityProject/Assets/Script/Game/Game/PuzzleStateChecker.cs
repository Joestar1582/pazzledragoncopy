﻿using UnityEngine;
using System.Collections;
using System.Linq;

public static class PuzzleStateChecker  {

	#region Puzzle check player have selected.
	public static void SelectedPuzzlePiece(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam,PuzzleData.STATE nextState)
	{
		foreach(var peiceObject in puzzleData.pieceObjectList.Select((value, index) => new {value,index}))
		{
			if(peiceObject.value.GetComponent<PuzzlePiece>().selected)
			{
				puzzleData.state = nextState;
				puzzleData.selectedPuzzleNo = peiceObject.index;
				return;
			}
		}
	}
	#endregion

	#region Puzzle check player have NOT selected.
	public static void UnselectedPuzzlePiece(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam,PuzzleData.STATE nextState)
	{
		foreach(var peiceObject in puzzleData.pieceObjectList.Select((value, index) => new {value,index}))
		{
			if(peiceObject.value.GetComponent<PuzzlePiece>().selected)
				return;
		}
		puzzleData.state = nextState;
	}
	#endregion
	

	#region Check if there is any leakage of the puzzle ID
	public static void CheckPuzzleIDLeakage(PuzzleData puzzleData,PuzzleOperaterParam puzzleParam)
	{
		foreach(var pieceObject in puzzleData.pieceObjectList)
		{
			PuzzlePiece targetPuzzle = pieceObject.GetComponent<PuzzlePiece>();
			if(targetPuzzle.ID < 0 || targetPuzzle.ID >=  puzzleData.pieceObjectList.Count)
				Debug.Log ("Puzzle" + pieceObject.name + " is a leak of the ID");
		}
	}
	#endregion

	#region Check if there is any leakage of the puzzle object index
	public static bool hasIndexLeakage(PuzzleData puzzleData,PuzzleOperaterParam puzzleParam,int targetIdx)
	{
		bool check = false;
		if(targetIdx < 0 || targetIdx >=  puzzleData.pieceObjectList.Count)
		{
			Debug.Log ("Leak of the puzzle object index " + targetIdx);
			check = true;
		}
		return check;
	}
	#endregion

	#region Count up selecting piece's time
	public static bool isFinishedSelectTime(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam)
	{
		bool checkTimeUp = false;
		return checkTimeUp;
	}
	#endregion


}
