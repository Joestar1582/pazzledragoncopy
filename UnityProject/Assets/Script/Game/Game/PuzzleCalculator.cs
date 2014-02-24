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

}
