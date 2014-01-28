using UnityEngine;
using System.Collections;

public class PuzzleManager : SingletonMonoBehaviour<PuzzleManager>{
	
	enum STATE
	{
		Select,
		Move,
		Check
	};

	private GameObject[] 	puzzles;
	private STATE 			state = STATE.Select;
	private	int				maxpuzzles;
	private	int				selectedPazzleNo = 0;
	
	public  int 			maxLines 	= 5;
	public  int 			maxColumns 	= 7;
	public  float 			puzzleSpace = 1.2f;
	
	public	GameObject		puzzlePrefab;
	public	Material[]		puzzleColor;
	
	#region Use this for initialization
	void Start () {
		maxpuzzles = maxLines * maxColumns;
		puzzles = new GameObject[maxpuzzles];
		for(int puzzleNo = 0;puzzleNo < maxpuzzles;puzzleNo++)
		{
			// Create puzzle.
			puzzles[puzzleNo] = Instantiate(puzzlePrefab,CalcPuzzlePosition(puzzleNo),Quaternion.identity) as GameObject;
			puzzles[puzzleNo].GetComponent<Puzzle>().ID = puzzleNo;
			puzzles[puzzleNo].name = "Puzzle" + puzzleNo.ToString();
			puzzles[puzzleNo].GetComponent<Puzzle>().used = true;

			// Set the color to random.
			int colorIdx = Random.Range(0,puzzleColor.Length);
			puzzles[puzzleNo].GetComponent<Puzzle>().SetColor(colorIdx,puzzleColor[colorIdx]);

		}
	}
	#endregion

	#region Update is called once per frame
	void Update () {
		// Debug
		if(Input.GetKeyDown(KeyCode.A))
			SortPuzzle();
		if(Input.GetKeyDown(KeyCode.Z))
			CreatePuzzle();

		switch(state)
		{
		case STATE.Select:
			CheckSelecting(STATE.Move);
			break;

		case STATE.Move:
			Move();
			CheckNotSelecting(STATE.Check);
			break;

		case STATE.Check:
			SortPuzzle();
			state = STATE.Select;
			break;
		};
	}
	#endregion

	#region Puzzle check you have selected.
	void CheckSelecting(STATE nextState)
	{
		for(int puzzleNo = 0; puzzleNo < maxpuzzles;puzzleNo++)
		{
			if(puzzles[puzzleNo].GetComponent<Puzzle>().selected)
			{
				state = nextState;
				selectedPazzleNo = puzzleNo;
				return;
			}
		}
	}
	#endregion

	#region Puzzle check you have NOT selected.
	void CheckNotSelecting(STATE nextState)
	{
		for(int puzzleNo = 0; puzzleNo < maxpuzzles;puzzleNo++)
		{
			if(puzzles[puzzleNo].GetComponent<Puzzle>().selected)
				return;
		}
		state = nextState;
	}
	#endregion
	
	#region Sort automatically puzzle
	void SortPuzzle()
	{
		// Rearranged in ascending order of ID puzzles
		PuzzleQuickSort(0,maxpuzzles - 1);
		// Puzzle number that is not in use is through
		for(int puzzleNo = maxpuzzles - 1; puzzleNo >= 0;puzzleNo--)
		{
			Vector3 puzzlePos = Vector3.zero;
			Puzzle targetPuzzle = puzzles[puzzleNo].GetComponent<Puzzle>();

			// Sort Puzzle ID 
			if(targetPuzzle.ID / maxColumns < maxLines - 1)
			{

				for(int id = targetPuzzle.ID + maxColumns;id < maxpuzzles;id += maxColumns)
				{
					GameObject emptyTemp = puzzles[SearchPuzzleNo(id)];
					Puzzle emptyPuzzle = emptyTemp.GetComponent<Puzzle>();
					if(emptyPuzzle.used == false)
					{
						emptyPuzzle.ID = targetPuzzle.ID;
						targetPuzzle.ID = id;
						emptyPuzzle.MoveAmountClear();
						emptyTemp.transform.position = CalcPuzzlePosition(emptyPuzzle.ID);
						emptyTemp.renderer.enabled = false;
					}
				}
			}
			// Set Position
			targetPuzzle.MoveAmountClear();
			iTween.MoveTo(puzzles[puzzleNo],iTween.Hash("position",CalcPuzzlePosition(targetPuzzle.ID),"time",0.1f));
		}
	}
	#endregion

	#region Move Puzzles
	void Move()
	{
		Vector3 moveAmount;
		Puzzle selectedPazzle = puzzles[selectedPazzleNo].GetComponent<Puzzle>();
		moveAmount = selectedPazzle.moveAmount;

		float recognitionRange = puzzleSpace / 1.2f;
		float recognitionRangeDiagonal = puzzleSpace / 2.0f;

		// Move diagonal
		if(moveAmount.x >= recognitionRangeDiagonal && moveAmount.z >= recognitionRangeDiagonal)
			ChangeID(selectedPazzle.ID + 1 + maxColumns);
		else if(moveAmount.x >= recognitionRangeDiagonal && moveAmount.z <= -recognitionRangeDiagonal)
			ChangeID(selectedPazzle.ID + 1 - maxColumns);
		else if(moveAmount.x <= -recognitionRangeDiagonal && moveAmount.z >= recognitionRangeDiagonal)
			ChangeID(selectedPazzle.ID - 1 + maxColumns);
		else if(moveAmount.x <= -recognitionRangeDiagonal && moveAmount.z <= -recognitionRangeDiagonal)
			ChangeID(selectedPazzle.ID - 1 - maxColumns);
		// Move right.
		else if(moveAmount.x >= recognitionRange)
			ChangeID(selectedPazzle.ID + 1);
		// Move left.
		else if(moveAmount.x <= -recognitionRange)
			ChangeID(selectedPazzle.ID - 1);
		// Move up.
		else if(moveAmount.z >= recognitionRange)
			ChangeID(selectedPazzle.ID + maxColumns);
		// Move down.
		else if(moveAmount.z <= -recognitionRange)
			ChangeID(selectedPazzle.ID - maxColumns);
	}	
	#endregion

	#region Change ID
	void ChangeID(int targetID)
	{
		Puzzle selectedPazzle = puzzles[selectedPazzleNo].GetComponent<Puzzle>();
		int tempID = selectedPazzle.ID;
		Vector3 amount = 	puzzles[SearchPuzzleNo(targetID)].transform.position - 
							selectedPazzle.transform.position;
		if( amount.x < puzzleSpace && amount.x > -puzzleSpace &&
		    amount.z < puzzleSpace && amount.z > -puzzleSpace )
		{
			int targetIdx = SearchPuzzleNo(targetID);
			selectedPazzle.MoveAmountClear(CalcPuzzlePosition(targetID));
			puzzles[targetIdx].GetComponent<Puzzle>().ID = tempID;
			selectedPazzle.ID = targetID;
			iTween.MoveTo(puzzles[targetIdx],iTween.Hash("position",CalcPuzzlePosition(tempID),"time",0.1f));

		}
	}
	#endregion

	#region Search pazzle number from ID
	int SearchPuzzleNo(int id)
	{
		for(int puzzleNo = 0; puzzleNo < maxpuzzles;puzzleNo++)
		{
			if(puzzles[puzzleNo].GetComponent<Puzzle>().ID == id)
				return puzzleNo;
		}
		return 0;
	}        
	#endregion

	#region Matching Puzzles
	void MatchingPuzzle()
	{
		for(int puzzleNo = maxpuzzles - 1; puzzleNo > 0;puzzleNo--)
		{

		}

	}
	#endregion

	#region Sort Puzzles by QuickSort
	void PuzzleQuickSort(int start, int end)
	{
		int i 		= start;
		int j 		= end;		
		int pivot	= puzzles[(start + end) / 2].GetComponent<Puzzle>().ID;
		while(true) 
		{
			while(puzzles[i].GetComponent<Puzzle>().ID < pivot)
				i++;
			while(pivot < puzzles[j].GetComponent<Puzzle>().ID)
				j--;
			if (puzzles[i].GetComponent<Puzzle>().ID >= puzzles[j].GetComponent<Puzzle>().ID)
				break;
			
			GameObject temp = puzzles[i];
			puzzles[i] = puzzles[j];
			puzzles[j] = temp;
			i++;
			j--;
		}

		if (start < i - 1)
			PuzzleQuickSort(start, i - 1);
		if (j + 1 <  end)
			PuzzleQuickSort(j + 1, end);
	}
	#endregion

	#region Calc Puzzle Position from ID
	Vector3 CalcPuzzlePosition(int id)
	{
		Vector3 puzzlePos;
		puzzlePos.x = ((id % maxColumns) - (maxLines / 2) - 1) * puzzleSpace;
		puzzlePos.y = 0;
		puzzlePos.z = ((id / maxColumns) - (maxLines / 2)) * puzzleSpace;
		return puzzlePos;
	}
	#endregion

	#region Create NEW Puzzle to empty area
	void CreatePuzzle()
	{
		// Rearranged in ascending order of ID puzzles
		PuzzleQuickSort(0,maxpuzzles - 1);
		// Create
		for(int puzzleNo = maxpuzzles - 1; puzzleNo >= 0;puzzleNo--)
		{
			Vector3 puzzlePos = Vector3.zero;
			Puzzle targetPuzzle = puzzles[puzzleNo].GetComponent<Puzzle>();

			if(targetPuzzle.used == false)
			{
				targetPuzzle.used = true;
				puzzles[puzzleNo].renderer.enabled = true;
				targetPuzzle.MoveAmountClear();

				// Puzzle emerges from the bottom
				puzzlePos = CalcPuzzlePosition(targetPuzzle.ID);
				puzzlePos.y -= puzzleSpace;
				puzzles[puzzleNo].transform.position = puzzlePos;
				iTween.MoveTo(puzzles[puzzleNo],iTween.Hash("position",CalcPuzzlePosition(targetPuzzle.ID),"time",0.1f));

				// Set the color to random.
				int colorIdx = Random.Range(0,puzzleColor.Length);
				targetPuzzle.SetColor(colorIdx,puzzleColor[colorIdx]);
			}
		}

	}
	#endregion

}
