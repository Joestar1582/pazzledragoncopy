﻿/****************************************************************************
 *
 * CRI Middleware SDK
 *
 * Copyright (c) 2011-2012 CRI Middleware Co.,Ltd.
 *
 * Library  : CRI Atom
 * Module   : CRI Atom for Unity Editor
 * File     : CriAtomServer.cs
 *
 ****************************************************************************/
using UnityEngine;
using System.Collections;


public class CriAtomServer : MonoBehaviour {
	
	#region Internal Fields
	private static CriAtomServer _instance = null;
	#endregion
	
	public static CriAtomServer instance {
		get {
			CreateInstance();
			return _instance;
		}
	}
	
	public static void CreateInstance() {
		if (_instance == null) {
			CriWare.managerObject.AddComponent<CriAtomServer>();
		}
	}
	
	public static void DestroyInstance() {
		if (_instance != null) {
			UnityEngine.GameObject.Destroy(_instance);
		}
	}
	
	void Awake()
	{
		/* インスタンスは常に１つしか生成されないことを保証する */
		if (_instance == null) {
			_instance = this;
		} else {
			GameObject.Destroy(this);
		}
	}
	
	void OnDisable()
	{
		if (_instance == this) {
			_instance = null;
		}
	}
	
	void OnApplicationPause(bool pause)
	{
		CriAtomPlugin.Pause(pause);
	}
}
