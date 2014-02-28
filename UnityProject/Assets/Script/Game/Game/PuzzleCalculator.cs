using UnityEngine;
using System.Collections;

public static class PuzzleCalculator {
	#region Calc Puzzle Position from ID
	public static Vector3 GetPiecePosition(PuzzleOperaterParam puzzleParam,int targetId)
	{
		Vector3 puzzlePos;
		puzzlePos.x = ((targetId % puzzleParam.maxColumns) - (puzzleParam.maxRows / 2) - 1) * puzzleParam.puzzleSpace;
		puzzlePos.y = 0;
		puzzlePos.z = ((targetId / puzzleParam.maxColumns) - (puzzleParam.maxRows / 2)) * puzzleParam.puzzleSpace;
		return puzzlePos;
	}
	#endregion
	
	#region Calc Column No from ID
	public static int GetPieceColumnNo(PuzzleOperaterParam puzzleParam,int targetId)
	{
		return (targetId % puzzleParam.maxColumns);
	}
	#endregion
	
	#region Calc Row No from ID
	public static int GetPieceRowNo(PuzzleOperaterParam puzzleParam,int targetId)
	{
		return (targetId / puzzleParam.maxColumns);
	}
	#endregion

	#region Calc Row Limit Border from stdNumChaines
	public static int GetRowLimitBorder(PuzzleOperaterParam puzzleParam)
	{
		return (puzzleParam.maxRows - (puzzleParam.stdNumChaines - 1));
	}
	#endregion

	#region Calc Column Limit Border from stdNumChaines
	public static int GetColumnLimitBorder(PuzzleOperaterParam puzzleParam)
	{
		return (puzzleParam.maxColumns - (puzzleParam.stdNumChaines - 1));
	}
	#endregion

	#region Calc Puzzle Piece Space Offset
	public static float GetPieceSpaceOffset(PuzzleOperaterParam puzzleParam)
	{
		return (puzzleParam.puzzleSpace / 10.0f);
	}
	#endregion

	#region Calc Amount Range of CrissCross
	public static float GetAmountRangeCrissCross(PuzzleOperaterParam puzzleParam)
	{
		return (puzzleParam.puzzleSpace / 1.2f);
	}
	#endregion

	#region Calc Amount Range of Diagonal
	public static float GetAmountRangeDiagonal(PuzzleOperaterParam puzzleParam)
	{
		return (puzzleParam.puzzleSpace / 2.0f);
	}
	#endregion

}
