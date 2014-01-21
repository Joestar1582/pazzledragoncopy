using UnityEngine;
using System.Collections;

public class MasterData : SingletonMonoBehaviour<MasterData> {
	
	public class RankingData{
		public	string	name ="HAL";
		public	int		score=1234;
	}
	
	[HideInInspector]
	public	RankingData[]	rankingData;
	
	void Start(){
		InitRanking();
	}
	
	void InitRanking(){
		rankingData = new RankingData[5];
		
		for(int i=0; i<5; i++)
		{
			rankingData[i] = new RankingData();
		}		
	}
}