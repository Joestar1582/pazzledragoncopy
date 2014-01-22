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
		}
	}
	
	// Update is called once per frame
	void Update () {
		switch(state)
		{
		case STATE.Select:
			CheckSelecting(true,STATE.Move);
			break;

		case STATE.Move:
			CheckSelecting(false,STATE.Check);
			break;

		case STATE.Check:
			state = STATE.Select;
			break;
		};
	}
	
	// Puzzle check you have selected.
	void CheckSelecting(bool compared,STATE nextState)
	{
		for(int puzzleNo = 0; puzzleNo < maxpuzzles;puzzleNo++)
		{
			if(puzzles[puzzleNo].GetComponent<Puzzle>().CheckSelecting() == compared)
			{
				state = nextState;
				selectedPazzleNo = puzzleNo;
				break;
			}
		}	
	}
	
	// Sort automatically puzzle
	void SortPuzzle()
	{
		
	}
}
