using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

#region Data set of puzzle
[System.Serializable]
public class PuzzleOperaterParam
{
	public  	int 					maxRows;
	public  	int 					maxColumns;
	public		int						maxPuzzles;
	public		int						stdNumChaines;
	public  	float 					puzzleSpace;
	public		float					moveTime;
	public		float					destroyTime;
	public		float 					selectTime;
};
#endregion

[System.Serializable]
public class PuzzleManager : SingletonMonoBehaviour<PuzzleManager>{

	public 		PuzzleOperaterParam		puzzleParam;
	public		GameObject				puzzlePiecePrefab;
	public		Material[]				puzzleColorList;

	private		PuzzleData				puzzleData;
	private		Action					puzzleAction;

	#region Use this for initialization
	void Start () {
		// Create Puzzles
		puzzleData = new PuzzleData();
		PuzzlePieceFactory.CreatePuzzlePieceObject(ref puzzleData,puzzleParam,puzzlePiecePrefab,puzzleColorList);
		// Set Action
		puzzleAction = new Action(PuzzleSelectAction);
	}
	#endregion

	#region Update is called once per frame
	void Update () {
		puzzleAction();
	}
	#endregion

	#region Action List
	public void PuzzleSelectAction()
	{
		PuzzleOperater.SortByRefEmpty(ref puzzleData,puzzleParam);
		if(PuzzleStateChecker.IsSelectedPiece(ref puzzleData,puzzleParam))
		{
			puzzleAction = new Action(PuzzleMoveAction);
			TimeCounter.StartTimer(ref puzzleData.selectTimeCounter);
		}
	}

	public void PuzzleMoveAction()
	{
		PuzzleOperater.Operate(ref puzzleData,puzzleParam);
		if(	TimeCounter.IsTimeOver(ref puzzleData.selectTimeCounter,puzzleParam.selectTime) ||
			PuzzleStateChecker.IsSelectedPiece(ref puzzleData,puzzleParam) == false)
		{
			puzzleAction = new Action(PuzzleCheckAction);
			puzzleData.NotAvailableAll();
			PuzzleOperater.SortByRefID(ref puzzleData,puzzleParam);
		}
	}

	public void PuzzleCheckAction()
	{
		if(PuzzleMatchChecker.HasMatch(ref puzzleData,puzzleParam))
			puzzleAction = new Action(PuzzleDestroyAction);
		else 
			puzzleAction = new Action(PuzzleCreateAction);
	}

	public void PuzzleDestroyAction()
	{
		if(PuzzlePieceGraveyard.IsCompletedDestroy(ref puzzleData,puzzleParam))
			puzzleAction = new Action(PuzzleCheckAction);
		PuzzleOperater.SortByRefID(ref puzzleData,puzzleParam);
	}

	public void PuzzleCreateAction()
	{
		PuzzleOperater.SortByRefEmpty(ref puzzleData,puzzleParam);
		PuzzlePieceFactory.CreateAtEmpty(ref puzzleData,puzzleParam,puzzleColorList);
		
		// Check again
		if(PuzzleMatchChecker.HasMatch(ref puzzleData,puzzleParam))
			puzzleAction = new Action(PuzzleDestroyAction);
		else 
		{
			puzzleData.AvailableAll();
			puzzleData.ChaineClear();
			puzzleAction = new Action(PuzzleSelectAction);
		}
	}

	#endregion

}
