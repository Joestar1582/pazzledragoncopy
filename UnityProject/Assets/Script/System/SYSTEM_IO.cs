using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


//===================================================
/*!
　　 	@brief		インプット、アウトプット基底クラス
　　 	
　　 	@date		2013/07/10
　　 	@author		Daichi Horio
　　*/
//===================================================
public class SYSTEM_IO {

	protected const		string		mApplicationName = "origami";

	protected 			string		mFolderName		=	"";					//!< フォルダパス
	protected 			string 		mSaveFileName 	= 	"";					//!< セーブファイルパス

	protected			string		mExtensionName	=	".SaveData";			//!< 拡張子名

	public SYSTEM_IO()
	{
		RuntimePlatform plat = Application.platform;
		if(plat == RuntimePlatform.WindowsPlayer)
		{
			mFolderName		=	Application.dataPath + "/Userdata";	//!< フォルダパス	
		}
		else if(plat == RuntimePlatform.WindowsEditor)
		{
			mFolderName		=	Application.dataPath + "/../origami_Data/Userdata";	//!< フォルダパス
		}
		else if(plat == RuntimePlatform.OSXPlayer)
		{
			mFolderName		=	Application.dataPath + "/Userdata";
		}
		else if(plat == RuntimePlatform.OSXEditor)
		{
			mFolderName		= 	Application.dataPath + "/../origami.app/Contents/Userdata";
		}

		mSaveFileName 	= 	mFolderName + "/savefile";						//!< セーブファイルパス
	}

	//===================================================
	/*!
	 	@brief	フォルダの新規作成

		@param	string ファイルパス
	 	@date	2013/07/10
	 	@author	Diachi Horio
	*/
	//===================================================
	protected bool CreateFolder(string path)
	{
		if (!System.IO.Directory.Exists (path))
		{
			System.IO.Directory.CreateDirectory (path);
			return true;
		}
		return false;
	}

	//===================================================
	/*!
	 	@brief	フォルダの削除

		@param	string ファイルパス
	 	@date	2013/07/10
	 	@author	Diachi Horio
	*/
	//===================================================
	protected bool DeleteFolder(string path)
	{
		if (System.IO.Directory.Exists (path))
		{
			System.IO.Directory.Delete (path);
			return true;
		}
		return false;
	}

	//===================================================
	/*!
	 	@brief	ファイルの新規作成

		@param	string ファイルパス
	 	@date	2013/07/10
	 	@author	Diachi Horio
	*/
	//===================================================
	protected bool SearchFile(string path)
	{
		if (System.IO.File.Exists (path))
		{
			//System.IO.File.Create(path);
			return true;
		}
		return false;
	}

	//===================================================
	/*!
	 	@brief	ファイルの削除

		@param	string ファイルパス
	 	@date	2013/07/10
	 	@author	Diachi Horio
	*/
	//===================================================
	protected bool DeleteFile(string path)
	{
		if (System.IO.File.Exists (path))
		{
			System.IO.File.Delete(path);
			return true;
		}
		return false;
	}

	//===================================================
	/*!
	 	@brief	セーブデータをバイナリで書き出し
	 			受け取ったデータを受けとったパスに書き出し
	 			パスにファイルが無ければ作る

		@param	object  obj		データクラスオブジェクト
		@param	string  path	ファイルのパス
	 	@date	2013/06/06
	 	@author	Diachi Horio
	*/
	//===================================================
	protected static void SaveToBinaryFile(object obj, string path)
	{
		FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
		BinaryFormatter bf = new BinaryFormatter();
		//シリアル化して書き込む
		bf.Serialize(fs, obj);
		fs.Close();
	}

	//===================================================
	/*!
	 	@brief	セーブデータをバイナリで読み込み
				受け取ったパスから読み込み

		@param	string  path	ファイルのパス
		@retrn	object	データオブジェクト
	 	@date	2013/06/06
	 	@author	Diachi Horio
	*/
	//===================================================
	protected static object LoadFromBinaryFile(string path)
	{
		FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
		BinaryFormatter f = new BinaryFormatter();
		//読み込んで逆シリアル化する
		object obj = f.Deserialize(fs);
		fs.Close();

		return obj;
	}
}
