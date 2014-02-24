using UnityEngine;
using System.Collections;

public static class PuzzleCalculator {
	#region Calc Puzzle Position from ID
	public static Vector3 PiecePosition(PuzzleOperaterParam puzzleParam,int id)
	{
		Vector3 puzzlePos;
		puzzlePos.x = ((id % puzzleParam.maxColumns) - (puzzleParam.maxLines / 2) - 1) * puzzleParam.puzzleSpace;
		puzzlePos.y = 0;
		puzzlePos.z = ((id / puzzleParam.maxColumns) - (puzzleParam.maxLines / 2)) * puzzleParam.puzzleSpace;
		return puzzlePos;
	}
	#endregion
	
	#region Calc Column No from ID
	public static int PieceColumnNo(PuzzleOperaterParam puzzleParam,int id)
	{
		return (id % puzzleParam.maxColumns);
	}
	#endregion
	
	#region Calc Line No from ID
	public static int PieceLineNo(PuzzleOperaterParam puzzleParam,int id)
	{
		return (id / puzzleParam.maxColumns);
	}
	#endregion

	#region Calc Line Limit Border from standardCombo
	public static int LineLimitBorder(PuzzleOperaterParam puzzleParam)
	{
		return (puzzleParam.maxLines - (puzzleParam.standardCombo - 1));
	}
	#endregion

	#region Calc Column Limit Border from standardCombo
	public static int ColumnLimitBorder(PuzzleOperaterParam puzzleParam)
	{
		return (puzzleParam.maxColumns - (puzzleParam.standardCombo - 1));
	}
	#endregion

	#region Calc Puzzle Piece Space Offset
	public static float PieceSpaceOffset(PuzzleOperaterParam puzzleParam)
	{
		return (puzzleParam.puzzleSpace / 10.0f);
	}
	#endregion

	#region Calc Amount Range
	public static float AmountRange(PuzzleOperaterParam puzzleParam)
	{
		return (puzzleParam.puzzleSpace / 1.2f);
	}
	#endregion

	#region Calc Amount Range Diagonal
	public static float AmountRangeDiagonal(PuzzleOperaterParam puzzleParam)
	{
		return (puzzleParam.puzzleSpace / 2.0f);
	}
	#endregion

}
