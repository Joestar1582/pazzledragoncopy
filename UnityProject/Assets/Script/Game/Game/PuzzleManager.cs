using UnityEngine;
using System.Collections;

#region Data set of puzzle
[System.Serializable]
public class PuzzleOperaterParam
{
	public  	int 					maxLines;
	public  	int 					maxColumns;
	public		int						maxPuzzles;
	public  	float 					puzzleSpace;
	public		float					moveTime;
};
#endregion

#region Data set of puzzle
[System.Serializable]
public class PuzzleData
{
	public enum STATE
	{
		Select,
		Move,
		Check
	};

	public 		GameObject[] 			puzzleObjectList;
	public 		STATE 					state;
	public		int						selectedPuzzleNo;

	public PuzzleData(int puzzleObjectListSize)
	{
		puzzleObjectList 	= new GameObject[puzzleObjectListSize];
		state				= STATE.Select;
		selectedPuzzleNo	= 0;
	}
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
		puzzleData = new PuzzleData(puzzleParam.maxPuzzles);
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
			PuzzleOperater.Move(ref puzzleData,puzzleParam);
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
				PuzzlePieceFactory.CreateAtEmpty(ref puzzleData,puzzleParam,puzzlePiecePrefab,puzzleColorList);
				PuzzleStateChecker.CheckPuzzleIDLeakage(ref puzzleData,puzzleParam);
			}
			break;
		};

	}
	#endregion
	
	#region Search pazzle number from ID
	public static int SearchPuzzleNo(GameObject[] puzzleObjectList,PuzzleOperaterParam puzzleParam,int id)
	{
		for(int puzzleNo = 0; puzzleNo < puzzleParam.maxPuzzles;puzzleNo++)
		{
			if(puzzleObjectList[puzzleNo].GetComponent<PuzzlePiece>().ID == id)
				return puzzleNo;
		}
		return 0;
	}        
	#endregion

}
