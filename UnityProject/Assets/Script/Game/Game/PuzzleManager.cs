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
	public		int 					maxSelectTime;
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
			if(PuzzleMatchChecker.hasCombo(ref puzzleData,puzzleParam))
				puzzleData.state = PuzzleData.STATE.Delete;
			else 
				puzzleData.state = PuzzleData.STATE.Create;
			break;

		case PuzzleData.STATE.Delete:
			PuzzlePieceGraveyard.Delete(ref puzzleData);
			puzzleData.state = PuzzleData.STATE.Check;
			break;

		case PuzzleData.STATE.Create:
			PuzzleOperater.Sort(ref puzzleData,puzzleParam);
			PuzzlePieceFactory.CreateAtEmpty(ref puzzleData,puzzleParam,puzzleColorList);

			// Check again
			if(PuzzleMatchChecker.hasCombo(ref puzzleData,puzzleParam))
				puzzleData.state = PuzzleData.STATE.Delete;
			else 
				puzzleData.state = PuzzleData.STATE.Select;
			break;
		};

		#region Debug
		if(Input.GetKeyDown(KeyCode.Return))
			print ("State." + puzzleData.state);
		if(Input.GetKeyDown(KeyCode.L))
			puzzleData.AvailableAll();
		#endregion
	}
	#endregion

}
