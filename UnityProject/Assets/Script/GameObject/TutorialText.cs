using UnityEngine;
using System.Collections;

public class TutorialText : MonoBehaviour {
	public Texture[] movie;
	public string[] text;

	public GameObject tutorialState;
	public GameObject tutorialMovie;

	private OrigamiController OrigamiControllerScript = null;

	int currentNo = 0;
	
	// Use this for initialization
	void Start () {
		OrigamiControllerScript = GameObject.Find( "OrigamiController" ).GetComponent<OrigamiController>();
		gameObject.transform.localScale = Vector3.zero;
		tutorialMovie.transform.localScale = Vector3.zero;
		OpenText();
		
		UI_TentionGauge.Instance.SetEnable(true);

	}
	
	// Update is called once per frame
	void Update () {		
		// Debug
		if(Input.GetKeyDown(KeyCode.A))
			CloseText();

		switch(currentNo)
		{
		// 折るための始点と終点の線を引く説明.
		case 5:
			if(OrigamiControllerScript.GetState() == OrigamiUpdate.STATE.FOLD_SELECT)
			{
				CloseText();
				OrigamiControllerScript.SetUpdateFlg(false);
			}
			else
			{
				OrigamiControllerScript.DisableClap();
			}
			
			break;

		// 折る方向の選択の説明.
		case 6:
			if(OrigamiControllerScript.GetState() == OrigamiUpdate.STATE.FOLD)
			{
				CloseText();
				OrigamiControllerScript.SetUpdateFlg(false);
				OrigamiControllerScript.OrigamiLineDelete();
				UI_OKButton.Instance.Off();
			}
			else
			{
				OrigamiControllerScript.DisableClap();
			}
			break;

		// 戻る機能の説明.
		case 7:
			if(OrigamiControllerScript.GetState() == OrigamiUpdate.STATE.REVERT)
			{
				UI_OKButton.Instance.On();
				CloseText();
			}
			else
			{
				OrigamiControllerScript.DisableClap();
				if(OrigamiControllerScript.GetState() == OrigamiUpdate.STATE.FOLD)	
					OrigamiControllerScript.SetUpdateFlg(true);
				else{
					OrigamiControllerScript.SetUpdateFlg(false);	
					UI_RevertButton.Instance.On();
				}
			}
			break;
			
		// 拍手の説明.
		case 8:
			if(OrigamiControllerScript.GetState() == OrigamiUpdate.STATE.END_MOVE)
			{
				CloseText();
			}
			else
			{
				OrigamiControllerScript.SetUpdateFlg(true);
				OrigamiControllerScript.OnlyClap();
			}
			break;
		}
					
		
		if(currentNo < text.Length)
		{
			TextMesh tm = (TextMesh)gameObject.GetComponent("TextMesh");
			tm.text = text[currentNo];
		}
	}
	
	void Restart()
	{
		currentNo = 1;
	}
	
	void OpenText()
	{
		iTween.Stop(gameObject);
		iTweenEvent.GetEvent(gameObject,"OpenTextTween").Play();
	}
	
	void IdleText()
	{
		switch(currentNo)
		{
		// ゲームの説明は必要ですか？.
		case 0:
			break;
			
		// 操作方法の説明.
		case 3:
			CountMovieTime(false);
			break;
			
		// ワクの説明.
		case 4:
			CountMovieTime(false);
			break;

		// 折るための始点と終点の線を引く説明.
		case 5:
			CountMovieTime(true);
			UI_TimeCounter.Instance.enabled = false;
			break;

		// 折る方向の選択の説明.
		case 6:
			CountMovieTime(true);
			break;

		// 戻る機能の説明.
		case 7:
			CountMovieTime(true);
			break;
			
		// 拍手の説明.
		case 8:
			CountMovieTime(true);
			break;

		// 制限時間の説明.
		case 9:
			UI_TimeCounter.Instance.enabled = true;
			CountMovieTime(false);
			break;
			
		case 10:
			iTweenEvent.GetEvent(gameObject,"IdleTextTween").Play();		
			UI_TimeCounter.Instance.enabled = false;
			break;
			
		// 評価の説明.
		case 11:
			
			CountMovieTime(false);
			break;
			
		// ゲージの説明.
		case 12:
			CountMovieTime(false);
			break;
			
		default:
			iTweenEvent.GetEvent(gameObject,"IdleTextTween").Play();		
			break;
		}
	}
	
	void CloseText()
	{
		MovieTexture tex = (MovieTexture)tutorialMovie.renderer.material.mainTexture;
		if(tex.isPlaying)	
			tex.Stop();
		iTween.Stop(gameObject);
		iTweenEvent.GetEvent(gameObject,"CloseTextTween").Play();		
	}
	
	void EndTutorial()
	{
		tutorialState.GetComponent<TutorialState>().EndText();
	}
	
	void PlayOrigami()
	{
		OrigamiControllerScript.CreateOrigami(WAKU.LEVEL_1_3,90);
		OrigamiControllerScript.OrigamiSelectTutorialModeEnable();
	}
	
	void NextText()
	{
		currentNo++;
		if(currentNo >= text.Length)
		{
			EndTutorial();
			return;
		}
		else
			OpenText();
				
		switch(currentNo)
		{
		// 操作方法の説明.
		case 3:
			SetMovie(0,false);
			iTween.Stop(tutorialMovie);
			iTweenEvent.GetEvent(tutorialMovie,"OpenMovieTween").Play();		
			iTweenEvent.GetEvent(tutorialMovie,"MovieRefleshMove").Play();		
			break;
			
		// ワクの説明.
		case 4:
			SetMovie(1,true);
			break;

		// 折るための始点と終点の線を引く説明.
		case 5:
			SetMovie(2,true);
			break;

		// 折る方向の選択の説明.
		case 6:
			OrigamiControllerScript.SetUpdateFlg(true);
			SetMovie(3,true);
			break;

		// 戻る機能の説明.
		case 7:
			OrigamiControllerScript.SetUpdateFlg(true);
			SetMovie(4,true);
			break;
			
		// 拍手の説明.
		case 8:
			SetMovie(5,true);
			break;

		// 制限時間の説明.
		case 9:
			SetMovie(6,true);
			iTween.Stop(tutorialMovie);
			iTweenEvent.GetEvent(tutorialMovie,"OpenMovieTween").Play();
			iTweenEvent.GetEvent(tutorialMovie,"MovieRefleshMove").Play();		
			break;
			
		// 評価の説明.
		case 11:
			SetMovie(7,true);
			iTween.Stop(tutorialMovie);
			iTweenEvent.GetEvent(tutorialMovie,"OpenMovieTween").Play();	
			iTweenEvent.GetEvent(tutorialMovie,"MovieRefleshMove").Play();		
			break;
			
		// ゲージの説明.
		case 12:
			SetMovie(8,true);
			iTween.Stop(tutorialMovie);
			iTweenEvent.GetEvent(tutorialMovie,"OpenMovieTween").Play();	
			iTweenEvent.GetEvent(tutorialMovie,"MovieRefleshMove").Play();		
			break;
			
		default:
			iTween.Stop(tutorialMovie);
			iTweenEvent.GetEvent(tutorialMovie,"CloseMovieTween").Play();		
			break;
		}
		
	}
	
	void SetMovie(int movieNo,bool useChangeTween)
	{
		MovieTexture tex = (MovieTexture)tutorialMovie.renderer.material.mainTexture;
		if(tex.isPlaying)	
			tex.Stop();
		tutorialMovie.renderer.material.SetTexture("_MainTex",movie[movieNo]);
		tex = (MovieTexture)tutorialMovie.renderer.material.mainTexture;
		tex.loop = true;
		tex.Play();
		
		if(useChangeTween)
			iTweenEvent.GetEvent(tutorialMovie,"ChangeMovieTween").Play();
	}
	
	void ReduceMovie()
	{
		iTween.Stop(tutorialMovie);
		iTweenEvent.GetEvent(tutorialMovie,"MovieReduceMove").Play();
		iTweenEvent.GetEvent(tutorialMovie,"MovieReduceScale").Play();
//		iTweenEvent.GetEvent(gameObject,"IdleTextTween").Play();
		
		if(currentNo == 5)
			PlayOrigami();
	}
	
	void CountMovieTime(bool loop)
	{
		MovieTexture tex = (MovieTexture)tutorialMovie.renderer.material.mainTexture;
		if(loop)
			ReduceMovie();
//			iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1, "time",tex.duration , "onupdate", "CountMovieTimeUpdate", "oncomplete", "ReduceMovie"));
		else
			iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1, "time",tex.duration , "onupdate", "CountMovieTimeUpdate", "oncomplete", "CloseText"));
	}
	
	void CountMovieTimeUpdate(float value)
	{
		
	}
	
	public void OnYesButtonPressed(){
		CloseText();
	}
	
	public void OnNoButtonPressed(){
		EndTutorial();
	}
}
