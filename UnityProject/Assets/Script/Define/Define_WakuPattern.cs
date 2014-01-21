using UnityEngine;
using System.Collections;

public enum WAKU{
	LEVEL_1_1,
	LEVEL_1_2,
	LEVEL_1_3,
	LEVEL_1_4,
	LEVEL_1_5,
	
	LEVEL_2_1,
	LEVEL_2_2,
	LEVEL_2_3,
	LEVEL_2_4,
	LEVEL_2_5,
	
	LEVEL_3_1,
	LEVEL_3_2,
	LEVEL_3_3,
	LEVEL_3_4,
	LEVEL_3_5,
	LEVEL_3_6,
	LEVEL_3_7,
	LEVEL_3_8,
	LEVEL_3_9,
	LEVEL_3_10,
	
	LEVEL_4_1,
	LEVEL_4_2,
	LEVEL_4_3,
	LEVEL_4_4,
	LEVEL_4_5,
	LEVEL_4_6,
	LEVEL_4_7,
	LEVEL_4_8,
	LEVEL_4_9,
	LEVEL_4_10,
	LEVEL_4_11,
	LEVEL_4_12,
	LEVEL_4_13,
	LEVEL_4_14,
	LEVEL_4_15,
	
	LEVEL_5_1,
	LEVEL_5_2,
	LEVEL_5_3,
	LEVEL_5_4,
	LEVEL_5_5,
	LEVEL_5_6,
	LEVEL_5_7,
	LEVEL_5_8,
	LEVEL_5_9,
	LEVEL_5_10,
	LEVEL_5_11,
	LEVEL_5_12,
	LEVEL_5_13,
	LEVEL_5_14,
	LEVEL_5_15,
	LEVEL_5_16,
	LEVEL_5_17,
	LEVEL_5_18,
	LEVEL_5_19,
	LEVEL_5_20,
	
	LEVEL_6_1,
	LEVEL_6_2,
	LEVEL_6_3,
	LEVEL_6_4,
	LEVEL_6_5,
	LEVEL_6_6,
	LEVEL_6_7,
	LEVEL_6_8,
	LEVEL_6_9,
	LEVEL_6_10,
	LEVEL_6_11,
	LEVEL_6_12,
	LEVEL_6_13,
	LEVEL_6_14,
	LEVEL_6_15,
	
	LEVEL_7_1,
	LEVEL_7_2,
	LEVEL_7_3,
	LEVEL_7_4,
	LEVEL_7_5,
	LEVEL_7_6,
	LEVEL_7_7,
	LEVEL_7_8,
	LEVEL_7_9,
	LEVEL_7_10,
	NUM
};

public class WakuPattern {
	public float	Incidence;
	public WAKU		WakuLevel;
	
	public void Set ( float arIncidence, WAKU arWakuLevel ){
		Incidence = arIncidence;
		WakuLevel = arWakuLevel;
	}
}

public class WakuPatternTable {
	public int				Degree;
	public WakuPattern[]	Pattern;
	
	public void CreatePattern ( uint Num ){
		Pattern = new WakuPattern[Num];
	}
	
	public void Set ( uint Index, float arIncidence, WAKU arWakuLevel ){
		if( Index >= Pattern.Length ){
			Debug.Log( "Out of Index" );
			return;
		}
		Pattern[Index] = new WakuPattern();
		Pattern[Index].Set( arIncidence, arWakuLevel );
	}
}

public static class Define_WakuPattern {
	// テーブルのサイズ.
	public const int MaxRange = 49;
	public static WakuPatternTable[] Table;
	
	public const int MIN_DEGREE = 0;
	public const int MAX_DEGREE = 49;
	
	static Define_WakuPattern (){
		Table = new WakuPatternTable[MaxRange];
		int Index = 0;
		Table[Index] = new WakuPatternTable();
		// 段階.
		Table[Index].Degree = MIN_DEGREE + Index;
		// パターンの数.
		Table[Index].CreatePattern( 1 );
		// 番号　出現率　レベル　折れる時間　インターバル.
		Table[Index].Set( 0, 100, WAKU.LEVEL_1_1 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 1 );
		Table[Index].Set( 0, 100, WAKU.LEVEL_1_2 );
		
/*		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 1 );
		Table[Index].Set( 0, 100, WAKU.LEVEL_1_3 );
*/		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 1 );
		Table[Index].Set( 0, 100, WAKU.LEVEL_1_4 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 2 );
		Table[Index].Set( 0, 75, WAKU.LEVEL_1_5 );
		Table[Index].Set( 1, 25, WAKU.LEVEL_2_1 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 2 );
		Table[Index].Set( 0, 50, WAKU.LEVEL_2_3 );
		Table[Index].Set( 1, 50, WAKU.LEVEL_2_2 );
		
/*		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 3 );
		Table[Index].Set( 0, 20, WAKU.LEVEL_2_1 );
		Table[Index].Set( 1, 30, WAKU.LEVEL_2_4 );
		Table[Index].Set( 2, 50, WAKU.LEVEL_2_5 ); 
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 2 );
		Table[Index].Set( 0, 60, WAKU.LEVEL_2_4 );
		Table[Index].Set( 1, 40, WAKU.LEVEL_3_1 ); */
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 3 );
		Table[Index].Set( 0, 50, WAKU.LEVEL_3_2 );
		Table[Index].Set( 1, 30, WAKU.LEVEL_3_3 );
		Table[Index].Set( 2, 20, WAKU.LEVEL_3_4 );
		
/*		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 3 );
		Table[Index].Set( 0, 40, WAKU.LEVEL_3_5 );
		Table[Index].Set( 1, 30, WAKU.LEVEL_3_6 );
		Table[Index].Set( 2, 30, WAKU.LEVEL_3_7 );
*/		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 3 );
		Table[Index].Set( 0, 40, WAKU.LEVEL_3_8 );
		Table[Index].Set( 1, 40, WAKU.LEVEL_3_9 );
		Table[Index].Set( 2, 20, WAKU.LEVEL_4_4 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 3 );
		Table[Index].Set( 0, 50, WAKU.LEVEL_4_1 );
		Table[Index].Set( 1, 20, WAKU.LEVEL_4_2 );
		Table[Index].Set( 2, 30, WAKU.LEVEL_4_3 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 3 );
		Table[Index].Set( 0, 20, WAKU.LEVEL_4_4 );
		Table[Index].Set( 1, 30, WAKU.LEVEL_4_5 );
		Table[Index].Set( 2, 50, WAKU.LEVEL_4_6 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 2 );
		Table[Index].Set( 0, 60, WAKU.LEVEL_4_7 );
		Table[Index].Set( 1, 40, WAKU.LEVEL_4_8 );
/*		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 3 );
		Table[Index].Set( 0, 30, WAKU.LEVEL_4_9 );
		Table[Index].Set( 1, 40, WAKU.LEVEL_4_10 );
		Table[Index].Set( 2, 30, WAKU.LEVEL_4_11 );
*/		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 2 );
		Table[Index].Set( 0, 50, WAKU.LEVEL_4_12 );
		Table[Index].Set( 1, 50, WAKU.LEVEL_4_13 );
		
/*		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 2 );
		Table[Index].Set( 0, 50, WAKU.LEVEL_4_14 );
		Table[Index].Set( 1, 50, WAKU.LEVEL_4_15 );
*/		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 3 );
		Table[Index].Set( 0, 30, WAKU.LEVEL_5_1 );
		Table[Index].Set( 1, 30, WAKU.LEVEL_5_2 );
		Table[Index].Set( 2, 40, WAKU.LEVEL_5_3 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 3 );
		Table[Index].Set( 0, 40, WAKU.LEVEL_5_4 );
		Table[Index].Set( 1, 30, WAKU.LEVEL_5_5 );
		Table[Index].Set( 2, 30, WAKU.LEVEL_5_6 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 2 );
		Table[Index].Set( 0, 50, WAKU.LEVEL_5_7 );
		Table[Index].Set( 1, 50, WAKU.LEVEL_5_8 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 1 );
		Table[Index].Set( 0, 100, WAKU.LEVEL_5_9 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 1 );
		Table[Index].Set( 0, 100, WAKU.LEVEL_5_10 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 3 );
		Table[Index].Set( 0, 40, WAKU.LEVEL_5_11 );
		Table[Index].Set( 1, 40, WAKU.LEVEL_5_12 );
		Table[Index].Set( 2, 20, WAKU.LEVEL_5_13 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 2 );
		Table[Index].Set( 0, 100, WAKU.LEVEL_5_14 );
		Table[Index].Set( 1, 100, WAKU.LEVEL_5_15 );		

		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 1 );
		Table[Index].Set( 0, 100, WAKU.LEVEL_5_15 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 3 );
		Table[Index].Set( 0, 30, WAKU.LEVEL_5_16 );
		Table[Index].Set( 1, 30, WAKU.LEVEL_5_17 );
		Table[Index].Set( 2, 40, WAKU.LEVEL_5_18 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 2 );
		Table[Index].Set( 0, 50, WAKU.LEVEL_5_19 );
		Table[Index].Set( 1, 50, WAKU.LEVEL_5_20 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 2 );
		Table[Index].Set( 0, 50, WAKU.LEVEL_6_1 );
		Table[Index].Set( 1, 50, WAKU.LEVEL_6_2 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 2 );
		Table[Index].Set( 0, 60, WAKU.LEVEL_6_3 );
		Table[Index].Set( 1, 40, WAKU.LEVEL_6_4 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 1 );
		Table[Index].Set( 0, 100, WAKU.LEVEL_6_5 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 2 );
		Table[Index].Set( 0, 50, WAKU.LEVEL_6_6 );
		Table[Index].Set( 1, 50, WAKU.LEVEL_6_7 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 1 );
		Table[Index].Set( 0, 100, WAKU.LEVEL_6_8 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 1 );
		Table[Index].Set( 0, 100, WAKU.LEVEL_6_9 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 1 );
		Table[Index].Set( 0, 100, WAKU.LEVEL_6_10 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 1 );
		Table[Index].Set( 0, 100, WAKU.LEVEL_6_11 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 1 );
		Table[Index].Set( 0, 100, WAKU.LEVEL_6_12 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 1 );
		Table[Index].Set( 0, 100, WAKU.LEVEL_6_13 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 1 );
		Table[Index].Set( 0, 100, WAKU.LEVEL_6_14 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 1 );
		Table[Index].Set( 0, 100, WAKU.LEVEL_6_15 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 1 );
		Table[Index].Set( 0, 100, WAKU.LEVEL_7_1 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 1 );
		Table[Index].Set( 0, 100, WAKU.LEVEL_7_2 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 1 );
		Table[Index].Set( 0, 100, WAKU.LEVEL_7_3);
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 1 );
		Table[Index].Set( 0, 100, WAKU.LEVEL_7_4 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 1 );
		Table[Index].Set( 0, 100, WAKU.LEVEL_7_5 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 1 );
		Table[Index].Set( 0, 100, WAKU.LEVEL_7_6 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 1 );
		Table[Index].Set( 0, 100, WAKU.LEVEL_7_7 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 1 );
		Table[Index].Set( 0, 100, WAKU.LEVEL_7_8 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 1 );
		Table[Index].Set( 0, 100, WAKU.LEVEL_7_9 );
		
		Index++;
		Table[Index] = new WakuPatternTable();
		Table[Index].Degree = MIN_DEGREE + Index;
		Table[Index].CreatePattern( 1 );
		Table[Index].Set( 0, 100, WAKU.LEVEL_7_10 );
		
		Debug.Log( Index );
	}
	
}
