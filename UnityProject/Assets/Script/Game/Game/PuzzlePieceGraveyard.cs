using UnityEngine;
using System.Collections;
using System.Linq;

public static class PuzzlePieceGraveyard{

	#region Create NEW Puzzle to empty area
	public static bool IsCompletedDestroy(ref PuzzleData puzzleData,PuzzleOperaterParam puzzleParam)
	{
		bool isCompleted = false;

		if(TimeCounter.IsTimeOver(ref puzzleData.destroyTimeCounter,puzzleParam.destroyTime))
		{
			int nowChaineID = puzzleData.numChaine;
			puzzleData.pieceObjectList.ForEach((GameObject pieceObject) => 
			{
				PuzzlePiece nowPiece = pieceObject.GetComponent<PuzzlePiece>();
				if(nowPiece.chaineID == nowChaineID)
					nowPiece.Stop();
			});

			TimeCounter.StartTimer(ref puzzleData.destroyTimeCounter);
			puzzleData.numChaine--;
			if(puzzleData.numChaine < 0)
				isCompleted = true;
		}
		return isCompleted;
	}
	#endregion



}
