using UnityEngine;
using System.Collections;

public class UI_Ranking : MonoBehaviour {

	public	UILabel[]	lbNames;
	public	UILabel[]	lbScores;
	public	UI_Buttons	btMenu;
	
	public	void	Init(){
		for(int i=0; i<5; i++)
		{
			lbNames[i].text  = MasterData.Instance.rankingData[i].name;
			lbScores[i].text = MasterData.Instance.rankingData[i].score.ToString("00000");
			
		}
	}
	
}
