using UnityEngine;
using System.Collections;
using System.Linq;

public static class PuzzlePieceGraveyard{

	#region Create NEW Puzzle to empty area
	public static void Delete(ref PuzzleData puzzleData)
	{
		puzzleData.pieceObjectList.ForEach((GameObject pieceObject) => 
		{
			PuzzlePiece targetPiece = pieceObject.GetComponent<PuzzlePiece>();
			if(targetPiece.used == false)
				targetPiece.Stop();
		});
	}
	#endregion

}
