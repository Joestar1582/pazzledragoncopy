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
	
	
	// Use this for initialization
	void Start () {
		maxpuzzles = maxLines * maxColumns;
		puzzles = new GameObject[maxpuzzles];
		for(int i = 0;i < maxpuzzles;i++)
		{
		// Create puzzle & Set position.
			Vector3 puzzlePos = Vector3.zero;
			puzzlePos.x = ((i % maxColumns) - (maxLines / 2) - 1) * puzzleSpace;
			puzzlePos.z = ((i / maxColumns) - (maxLines / 2)) * puzzleSpace;
			puzzles[i] = Instantiate(puzzlePrefab,puzzlePos,Quaternion.identity) as GameObject;
		// Set the color to random.
			puzzles[i].GetComponent<Puzzle>().SetColor(puzzleColor[Random.Range(0,puzzleColor.Length)]);
		// Set puzzle ID
			puzzles[i].GetComponent<Puzzle>().SetID(i);
		// Set puzzle Name
			puzzles[i].name = "Puzzle" + i.ToString();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.A))
			SortPuzzle();

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
			print ("Check out !");
			SortPuzzle();
			state = STATE.Select;
			break;
		};
	}
	
	// Puzzle check you have selected.
	void CheckSelecting(STATE nextState)
	{
		for(int puzzleNo = 0; puzzleNo < maxpuzzles;puzzleNo++)
		{
			if(puzzles[puzzleNo].GetComponent<Puzzle>().CheckSelecting())
			{
				state = nextState;
				selectedPazzleNo = puzzleNo;
				return;
			}
		}
	}

	// Puzzle check you have selected.
	void CheckNotSelecting(STATE nextState)
	{
		for(int puzzleNo = 0; puzzleNo < maxpuzzles;puzzleNo++)
		{
			if(puzzles[puzzleNo].GetComponent<Puzzle>().CheckSelecting())
				return;
		}
		state = nextState;
	}


	// Sort automatically puzzle
	void SortPuzzle()
	{
		for(int i = 0; i < maxpuzzles;i++)
		{
			Vector3 puzzlePos = Vector3.zero;
			int id = puzzles[i].GetComponent<Puzzle>().GetID();
			puzzlePos.x = ((id % maxColumns) - (maxLines / 2) - 1) * puzzleSpace;
			puzzlePos.z = ((id / maxColumns) - (maxLines / 2)) * puzzleSpace;

			puzzles[i].GetComponent<Puzzle>().MoveAmountClear();

//			puzzles[i].transform.position = puzzlePos;
			iTween.MoveTo(puzzles[i],iTween.Hash("position",puzzlePos,"time",0.1f));
		}
	}

	// Move Puzzles
	void Move()
	{
		Vector3 moveAmount;
		Puzzle selectedPazzle = puzzles[selectedPazzleNo].GetComponent<Puzzle>();
		moveAmount = selectedPazzle.GetMoveAmount();
		// Move right.
		if(moveAmount.x >= puzzleSpace)
			ChangeID(selectedPazzle.GetID() + 1);
		// Move left.
		else if(moveAmount.x <= -puzzleSpace)
			ChangeID(selectedPazzle.GetID() - 1);
		// Move up.
		else if(moveAmount.z >= puzzleSpace)
			ChangeID(selectedPazzle.GetID() + maxColumns);
		// Move down.
		else if(moveAmount.z <= -puzzleSpace)
			ChangeID(selectedPazzle.GetID() - maxColumns);

	}	


	// Change ID
	void ChangeID(int targetID)
	{
		Puzzle selectedPazzle = puzzles[selectedPazzleNo].GetComponent<Puzzle>();
		int tempID = selectedPazzle.GetID();
		selectedPazzle.MoveAmountClear();
		puzzles[SearchPuzzleNo(targetID)].GetComponent<Puzzle>().SetID(tempID);
		selectedPazzle.SetID(targetID);

		SortPuzzle();
	}

	// Search pazzle number from ID
	int SearchPuzzleNo(int id)
	{
		for(int puzzleNo = 0; puzzleNo < maxpuzzles;puzzleNo++)
		{
			if(puzzles[puzzleNo].GetComponent<Puzzle>().GetID() == id)
				return puzzleNo;
		}
		return 0;
	}
}
