using UnityEngine;
using System.Collections;

public class CRI_SoundManager_2D : SingletonMonoBehaviour<CRI_SoundManager_2D> {
	
	public	CriAtomSource	BGM;
	public	CriAtomSource	SE;
	
	public void PlayBGM(BGM_ID queID){
		BGM.Play((int)queID);
	}
	
	public void PlayBGM(string queName){
		BGM.Play(queName);
	}
	
	public void StopBGM(){
		BGM.Stop();
	}
	
	public void PauseBGM(){
		BGM.Pause(true);
	}
	
	public void ResumeBGM(){
		BGM.Pause(false);
	}
	
	public void PlaySE(SE_ID queID){
		SE.Play((int)queID);
	}
	
	public void PlaySE(string queName){
		SE.Play(queName);
	}
	
	public float GetBGMVolume(){
		return BGM.volume;
	}
	
	public void SetBGMVolume(float volume){
		BGM.volume = volume;
	}
	
	public float GetSEVolume(){
		return SE.volume;
	}
	
	public void SetSEVolume(float volume){
		SE.volume = volume;
	}
}
