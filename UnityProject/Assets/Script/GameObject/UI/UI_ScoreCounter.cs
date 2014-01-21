using UnityEngine;
using System.Collections;

public class UI_ScoreCounter : SingletonMonoBehaviour<UI_ScoreCounter> {
	
	private			int			score			= 0;
	private			int			combo			= 0;
	private			float		scoreCounter	= 0;
	private			UILabel		label;
	
	private			int[]		SC_CITYLEVEL_POINTS = 	{
															1000,
															2500,
															5000,
															7500,
															10000,
															15000,
															20000
														};
	
	private	const	float		SC_COUNTSPEED 	= 600.0f;
	
	public	int		Score{
		get{return score;}
	}
	
	public	int		Combo{
		get{return combo;}
	}
	
	public	void	SetEnable(bool b){
		label.enabled = b;
	}
	
	public	void	SetResult(float	per){
		Debug.Log("score:"+per);
		
		float	per_floored = StaticMath.ToRoundDown(per, 1);
		
		if(per < Define_Rate.Good)
			combo = 0;
		
		int	scoreAdds = Mathf.FloorToInt( (per * 10) * (1 + (float)combo * 0.5f) );
		
		CityLevelUp(scoreAdds);
		
		score += scoreAdds;
		
		if(score > 99999)
			score = 99999;
		
		if(combo > 0)
			UI_Combo.Instance.SetCombo(combo);
		
		if(per >= Define_Rate.Good)
			combo++;
		
		UI_ScoreAdds.Instance.SetScore(scoreAdds);
	}
	
	private	void	Start(){
		label = gameObject.GetComponent<UILabel>();
	}
	
	private	void	Update(){
		if(score > scoreCounter)
		{
			scoreCounter += Time.deltaTime * SC_COUNTSPEED;
			label.text = Mathf.FloorToInt(scoreCounter).ToString("00000");
		}else if(score < scoreCounter)
		{
			scoreCounter = score;
			label.text = score.ToString("00000");
		}
	}
	
	private	void	CityLevelUp(int adds){
		if(score < SC_CITYLEVEL_POINTS[0] && (score+adds) >= SC_CITYLEVEL_POINTS[0])
		{
			NaviCamera.Instance.LookTownPauseTween();
			Game_CityLayer.Instance.CityLayerEnable(1,true);
			return;
		}else if(score < SC_CITYLEVEL_POINTS[1] && (score+adds) >= SC_CITYLEVEL_POINTS[1]){
			NaviCamera.Instance.LookTownPauseTween();
			Game_CityLayer.Instance.CityLayerEnable(2,true);
			return;
		}else if(score < SC_CITYLEVEL_POINTS[2] && (score+adds) >= SC_CITYLEVEL_POINTS[2]){
			NaviCamera.Instance.LookTownPauseTween();
			Game_CityLayer.Instance.CityLayerEnable(3,true);
			return;
		}else if(score < SC_CITYLEVEL_POINTS[3] && (score+adds) >= SC_CITYLEVEL_POINTS[3]){
			NaviCamera.Instance.LookTownPauseTween();
			Game_CityLayer.Instance.CityLayerEnable(4,true);
			return;
		}else if(score < SC_CITYLEVEL_POINTS[4] && (score+adds) >= SC_CITYLEVEL_POINTS[4]){
			NaviCamera.Instance.LookTownPauseTween();
			Game_CityLayer.Instance.CityLayerEnable(5,true);
			return;
		}else if(score < SC_CITYLEVEL_POINTS[5] && (score+adds) >= SC_CITYLEVEL_POINTS[5]){
			NaviCamera.Instance.LookTownPauseTween();
			Game_CityLayer.Instance.CityLayerEnable(6,true);
			return;
		}else if(score < SC_CITYLEVEL_POINTS[6] && (score+adds) >= SC_CITYLEVEL_POINTS[6]){
			NaviCamera.Instance.LookTownPauseTween();
			Game_CityLayer.Instance.CityLayerEnable(7,true);
			return;
		}
	}
}
