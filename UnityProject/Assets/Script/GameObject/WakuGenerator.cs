using UnityEngine;
using System.Collections;

public class WakuGenerator : SingletonMonoBehaviour<WakuGenerator> {
	
	enum STATE {
		STOP,		// 11/29 kojima added
		CREATE,
		WAIT,
		INTERVAL
	};
	
//	STATE State = STATE.CREATE;
	STATE State = STATE.STOP;	// 11/29 kojima edited
	
	OrigamiController OrigamiControllerScript = null;
	
	[HideInInspector]
	public int	NowDegree = 3;
	[HideInInspector]
	public int	CutTime;
	
	public	int	IntervalTime = 5;
	public	int	OriTime = 30;
	private readonly int[] DegreeAddTable = new int[4]{ 1,1,1,2 };
	//private int Timer = 0;
	private	float	Timer = 0;	// 11/29 kojima edited
	
	public void Init (){

		NowDegree = 1;
		OrigamiControllerScript = GameObject.Find( "OrigamiController" ).GetComponent<OrigamiController>();
		State = STATE.STOP;
	}
	
	public void SetResult ( int Result ){
		if( Result >= DegreeAddTable.Length )	return;
		

		NowDegree += DegreeAddTable[Result];
	}
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if( State == STATE.CREATE ){
			int	Index = NowDegree - Define_WakuPattern.MIN_DEGREE;
			if( Index < 0 )	Index = 0;

			else if( Index >= Define_WakuPattern.MaxRange )	Index = Define_WakuPattern.MaxRange-1;
			int NumPattern = Define_WakuPattern.Table[Index].Pattern.Length;
			float RandPar = Random.Range( 0.0f, 100.0f );
			float Par = 0;
			int	SelectIndex = 0;
			for( int i = 0; i < NumPattern; i++ ){
				Par += Define_WakuPattern.Table[Index].Pattern[i].Incidence;
				if( Par > RandPar ){
					SelectIndex = i;
					break;
				}
			}
			// 折り紙生成.
			OrigamiControllerScript.CreateOrigami( 
				Define_WakuPattern.Table[Index].Pattern[SelectIndex].WakuLevel, OriTime );
			
			
			State = STATE.WAIT;
		}
		else if( State == STATE.WAIT ){
			if( OrigamiControllerScript.GetActiveFlg() ) return;
			Timer = 0;
			State = STATE.INTERVAL;
		}
		else if(State == STATE.INTERVAL){
//			if( StaticMath.Compensation( ref Timer, IntervalTime, 1 ) ){
			if( Timer >= 2.0f && Timer < 3.0f )
				UI_WaveCount.Instance.Play();
			
			if((Timer+=Time.deltaTime) >= IntervalTime){					// 11/29 kojima edited
				State = STATE.CREATE;
			}
		}
	}
	
	// 枠生成の停止	11/29 kojima added
	public bool Stop(){
		if(State == STATE.WAIT)
		{
			return false;
		}else{
			State = STATE.STOP;
			return true;
		}
	}
	
	// 枠生成の開始	11/29 kojima added
	public bool Play(){
		if(State == STATE.STOP || State == STATE.INTERVAL)
		{
			State = STATE.WAIT;
			return true;
		}else{
			return false;
		}
	}	
}
