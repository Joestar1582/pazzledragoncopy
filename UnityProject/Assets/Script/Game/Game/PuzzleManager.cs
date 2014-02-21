using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#region Data set of puzzle
[System.Serializable]
public class PuzzleOperaterParam
{
	public  	int 					maxLines;
	public  	int 					maxColumns;
	public		int						maxPuzzles;
	public  	float 					puzzleSpace;
	public		float					moveTime;
	public		int						standardCombo;
};
#endregion

[System.Serializable]
public class PuzzleManager : SingletonMonoBehaviour<PuzzleManager>{

	public 		PuzzleOperaterParam		puzzleParam;
	public		GameObject				puzzlePiecePrefab;
	public		Material[]				puzzleColorList;

	private		PuzzleData				puzzleData;

	#region Use this for initialization
	void Start () {
		// Create Puzzles
		puzzleData = new PuzzleData();
		PuzzlePieceFactory.CreatePuzzlePieceObject(ref puzzleData,puzzleParam,puzzlePiecePrefab,puzzleColorList);
	}
	#endregion

	#region Update is called once per frame
	void Update () {
		switch(puzzleData.state)
		{
		case PuzzleData.STATE.Select:
			PuzzleStateChecker.SelectedPuzzlePiece(ref puzzleData,puzzleParam,PuzzleData.STATE.Move);
			PuzzleOperater.Sort(ref puzzleData,puzzleParam);
			break;

		case PuzzleData.STATE.Move:
			PuzzleOperater.Operate(ref puzzleData,puzzleParam);
			PuzzleStateChecker.UnselectedPuzzlePiece(ref puzzleData,puzzleParam,PuzzleData.STATE.Check);
			break;

		case PuzzleData.STATE.Check:
			if(PuzzleMatchChecker.Check(ref puzzleData,puzzleParam) == false)
			{
				puzzleData.state = PuzzleData.STATE.Select;
			}
			else 
			{
				PuzzleOperater.Sort(ref puzzleData,puzzleParam);
				PuzzlePieceFactory.CreateAtEmpty(ref puzzleData,puzzleParam,puzzleColorList);
				PuzzleStateChecker.CheckPuzzleIDLeakage(puzzleData,puzzleParam);
			}
			break;
		};

	}
	#endregion

}
