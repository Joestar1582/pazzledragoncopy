using UnityEngine;
using System.Collections;
using System.Collections.Generic;

delegate void PuzzleActionDelegate();

#region Data set of puzzle
[System.Serializable]
public class PuzzleOperaterParam
{
	public  	int 					maxRows;
	public  	int 					maxColumns;
	public		int						maxPuzzles;
	public  	float 					puzzleSpace;
	public		float					moveTime;
	public		float					deleteTime;
	public		int						standardCombo;
	public		int 					maxSelectTime;
};
#endregion

[System.Serializable]
public class PuzzleManager : SingletonMonoBehaviour<PuzzleManager>{

	public 		PuzzleOperaterParam		puzzleParam;
	public		GameObject				puzzlePiecePrefab;
	public		Material[]				puzzleColorList;

	private		PuzzleData				puzzleData;

	private		PuzzleActionDelegate	puzzleAction;

	#region Use this for initialization
	void Start () {
		// Create Puzzles
		puzzleData = new PuzzleData();
		PuzzlePieceFactory.CreatePuzzlePieceObject(ref puzzleData,puzzleParam,puzzlePiecePrefab,puzzleColorList);
		// Set Action
		puzzleAction = new PuzzleActionDelegate(PuzzleSelectAction);
	}
	#endregion

	#region Update is called once per frame
	void Update () {
		puzzleAction();
		DebugAction();
	}
	#endregion

	#region Action List
	public void PuzzleSelectAction()
	{
		PuzzleOperater.SortByRefEmpty(ref puzzleData,puzzleParam);
		if(PuzzleStateChecker.IsSelectedPiece(ref puzzleData,puzzleParam) == true)
			puzzleAction = new PuzzleActionDelegate(PuzzleMoveAction);
	}

	public void PuzzleMoveAction()
	{
		PuzzleOperater.Operate(ref puzzleData,puzzleParam);
		if(PuzzleStateChecker.IsSelectedPiece(ref puzzleData,puzzleParam) == false)
			puzzleAction = new PuzzleActionDelegate(PuzzleCheckAction);
	}

	public void PuzzleCheckAction()
	{
		if(PuzzleMatchChecker.HasMatch(ref puzzleData,puzzleParam))
			puzzleAction = new PuzzleActionDelegate(PuzzleDestroyAction);
		else 
			puzzleAction = new PuzzleActionDelegate(PuzzleCreateAction);
	}

	public void PuzzleDestroyAction()
	{
		if(PuzzlePieceGraveyard.IsCompletedDestroy(ref puzzleData))
			puzzleAction = new PuzzleActionDelegate(PuzzleCheckAction);
		PuzzleOperater.SortByRefID(ref puzzleData,puzzleParam);
	}

	public void PuzzleCreateAction()
	{
		PuzzleOperater.SortByRefEmpty(ref puzzleData,puzzleParam);
		PuzzlePieceFactory.CreateAtEmpty(ref puzzleData,puzzleParam,puzzleColorList);
		
		// Check again
		if(PuzzleMatchChecker.HasMatch(ref puzzleData,puzzleParam))
			puzzleAction = new PuzzleActionDelegate(PuzzleDestroyAction);
		else 
			puzzleAction = new PuzzleActionDelegate(PuzzleSelectAction);
	}

	public void DebugAction()
	{
		if(Input.GetKeyDown(KeyCode.L))
			puzzleData.AvailableAll();
	}
	#endregion

}
