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
			// Delete preferentially from the puzzle that has been traced
			if(puzzleData.selectedPieceNameList.Count != 0)
			{
				// Select item that has been registered
				PuzzlePiece registeredPiece = puzzleData.FindPiece(puzzleData.selectedPieceNameList[0]);
				// Remove the first item
				puzzleData.selectedPieceNameList.RemoveAt(0);

				puzzleData.pieceObjectList.ForEach((GameObject pieceObject) => 
				{
					PuzzlePiece nowPiece = pieceObject.GetComponent<PuzzlePiece>();
					if(registeredPiece.type == nowPiece.type)
					{
						if(nowPiece.used == false)
							nowPiece.Stop();
					}
				});

				TimeCounter.StartTimer(ref puzzleData.destroyTimeCounter);
			}
			else
			{
				int nowChaineID = puzzleData.numChaine;
				puzzleData.pieceObjectList.ForEach((GameObject pieceObject) => 
				{
					PuzzlePiece nowPiece = pieceObject.GetComponent<PuzzlePiece>();
					if(nowPiece.used == false && nowPiece.chaineID == nowChaineID)
						nowPiece.Stop();
				});

				TimeCounter.StartTimer(ref puzzleData.destroyTimeCounter);
			}

			puzzleData.numChaine--;
			if(puzzleData.numChaine <= 0)
				isCompleted = true;
		}
		return isCompleted;
	}
	#endregion



}
