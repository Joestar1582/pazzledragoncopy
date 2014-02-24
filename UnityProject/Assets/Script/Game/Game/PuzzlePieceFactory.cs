using UnityEngine;
using System.Collections;
using System.Linq; 

public static class PuzzlePieceFactory  {
	#region Create Puzzle Piece
	public static void CreatePuzzlePieceObject(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam,
	                       GameObject puzzlePiecePrefab,Material[] puzzleColorList)
	{
		for(int puzzleNo = 0;puzzleNo < puzzleParam.maxPuzzles;puzzleNo++)
		{
			// Create puzzle piece.
			puzzleData.pieceObjectList.Add(Object.Instantiate(puzzlePiecePrefab,
			                                                  PuzzleCalculator.PiecePosition(puzzleParam,puzzleNo),
			                                                  Quaternion.identity) as GameObject);
			puzzleData.pieceObjectList[puzzleNo].name = "Puzzle" + puzzleNo.ToString();

			// Set Parameters
			PuzzlePiece piece = puzzleData.pieceObjectList[puzzleNo].GetComponent<PuzzlePiece>();
			piece.ID = puzzleNo;
			piece.Resume();

			// Set the color to random.
			int colorIdx = Random.Range(0,puzzleColorList.Length);
			piece.SetColor(colorIdx,puzzleColorList[colorIdx]);
		}

	}
	#endregion


	#region Create NEW Puzzle to empty area
	public static void CreateAtEmpty(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam,Material[] puzzleColorList)
	{
		// Rearranged in ascending order of ID puzzles
		puzzleData.Sort();
		// Create
		puzzleData.pieceObjectList.ForEach((GameObject pieceObject) => 
		{
			PuzzlePiece piece = pieceObject.GetComponent<PuzzlePiece>();
			// Create a new puzzle piece ID that is not used
			if(piece.used == false)
			{
				// Resume Puzzle Piece
				piece.Resume();

				// Puzzle emerges from the bottom
				Vector3 initPos = PuzzleCalculator.PiecePosition(puzzleParam,piece.ID);
				initPos.y -= puzzleParam.puzzleSpace;
				piece.Move(initPos,PuzzleCalculator.PiecePosition(puzzleParam,piece.ID),puzzleParam.moveTime);

				// Set the color to random.
				int typeNo = Random.Range(0,puzzleColorList.Length);
				piece.SetColor(typeNo,puzzleColorList[typeNo]);
			}
		} );
	}
	#endregion
}
