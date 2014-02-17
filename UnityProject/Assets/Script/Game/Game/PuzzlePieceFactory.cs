using UnityEngine;
using System.Collections;

public class PuzzlePieceFactory  {
	#region Create Puzzle Piece
	public static void CreatePuzzlePieceObject(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam,
	                       GameObject puzzlePiecePrefab,Material[] puzzleColorList)
	{
		for(int puzzleNo = 0;puzzleNo < puzzleParam.maxPuzzles;puzzleNo++)
		{
			// Create puzzle piece.
			puzzleData.puzzleObjectList[puzzleNo] = Object.Instantiate(puzzlePiecePrefab,
			                                                PuzzleOperater.CalcPuzzlePosition(puzzleParam,puzzleNo),
			                                                Quaternion.identity) as GameObject;
			puzzleData.puzzleObjectList[puzzleNo].GetComponent<PuzzlePiece>().ID = puzzleNo;
			puzzleData.puzzleObjectList[puzzleNo].GetComponent<PuzzlePiece>().used = true;
			puzzleData.puzzleObjectList[puzzleNo].name = "Puzzle" + puzzleNo.ToString();

			// Set the color to random.
			int colorIdx = Random.Range(0,puzzleColorList.Length);
			puzzleData.puzzleObjectList[puzzleNo].GetComponent<PuzzlePiece>().SetColor(colorIdx,puzzleColorList[colorIdx]);
		}
	}
	#endregion


	#region Create NEW Puzzle to empty area
	public static void CreateAtEmpty(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam,
	                                 GameObject puzzlePiecePrefab,Material[] puzzleColorList)
	{
		// Rearranged in ascending order of ID puzzles
		PuzzleOperater.PuzzleQuickSort(ref puzzleData,0,puzzleParam.maxPuzzles - 1);
		// Create
		for(int puzzleNo = puzzleParam.maxPuzzles - 1; puzzleNo >= 0;puzzleNo--)
		{
			Vector3 puzzlePos = Vector3.zero;
			PuzzlePiece targetPuzzle = puzzleData.puzzleObjectList[puzzleNo].GetComponent<PuzzlePiece>();

			// Create a new puzzle piece ID that is not used
			if(targetPuzzle.used == false)
			{
				targetPuzzle.used = true;
				puzzleData.puzzleObjectList[puzzleNo].renderer.enabled = true;
				targetPuzzle.MoveAmountClear();

				// Puzzle emerges from the bottom
				puzzlePos = PuzzleOperater.CalcPuzzlePosition(puzzleParam,targetPuzzle.ID);
				puzzlePos.y -= puzzleParam.puzzleSpace;
				puzzleData.puzzleObjectList[puzzleNo].transform.position = puzzlePos;
				iTween.MoveTo(puzzleData.puzzleObjectList[puzzleNo],iTween.Hash("position",PuzzleOperater.CalcPuzzlePosition(puzzleParam,targetPuzzle.ID),"time",puzzleParam.moveTime));

				// Set the color to random.
				int colorIdx = Random.Range(0,puzzleColorList.Length);
				targetPuzzle.SetColor(colorIdx,puzzleColorList[colorIdx]);
			}
		}

	}
	#endregion
}
