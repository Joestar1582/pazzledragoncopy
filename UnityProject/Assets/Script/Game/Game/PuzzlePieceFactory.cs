using UnityEngine;
using System.Collections;
using System.Linq; 

public static class PuzzlePieceFactory  {
	#region Create Puzzle Piece
	public static void CreatePuzzlePieceObject(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam,
	                       GameObject puzzlePiecePrefab,Material[] puzzleColorList)
	{
		for(int i = 0;i < puzzleParam.maxPuzzles;i++)
		{
			// Create new piece.
			puzzleData.pieceObjectList.Add(Object.Instantiate(puzzlePiecePrefab,
			                                                  PuzzleCalculator.GetPiecePosition(puzzleParam,i),
			                                                  Quaternion.identity) as GameObject);
			puzzleData.pieceObjectList[i].name = "Puzzle" + i.ToString();

			// Set Parameters
			PuzzlePiece newPiece = puzzleData.pieceObjectList[i].GetComponent<PuzzlePiece>();
			newPiece.ID = i;
			newPiece.Resume();

			// Set the color to random.
			int colorIdx = Random.Range(0,puzzleColorList.Length);
			newPiece.SetColor(colorIdx,puzzleColorList[colorIdx]);
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
			PuzzlePiece targetPiece = pieceObject.GetComponent<PuzzlePiece>();
			// Create a new puzzle piece ID that is not used
			if(targetPiece.used == false)
			{
				// Resume Puzzle Piece
				targetPiece.Resume();

				// Set Puzzle position 
				Vector3 endPos 		= PuzzleCalculator.GetPiecePosition(puzzleParam,targetPiece.ID);
				Vector3 startPos	= endPos;
				endPos.z			= -50;
				targetPiece.Move(startPos,endPos,puzzleParam.moveTime);

				// Set the color to random.
				int typeNo = Random.Range(0,puzzleColorList.Length);
				targetPiece.SetColor(typeNo,puzzleColorList[typeNo]);
			}
		} );
	}
	#endregion
}
