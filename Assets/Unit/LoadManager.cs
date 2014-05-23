using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using SportsTimeMachinePlayer.Common;
using ICSharpCode.SharpZipLib.Zip;

namespace SportsTimeMachinePlayer.Unit
{

	/// <summary>
	/// スポーツタイムマシンのロードマネージャ.
	/// WEBからのデータのダウンロードとダウンロードしたデータのロードの操作を行う.
	/// </summary>
	public class LoadManager : MonoBehaviour 
	{
		/// <summary>
		/// ファイル名.DIされる.
		/// </summary>
		public string filename;

		// ダウンロード完了イベント
		public event EventHandler DownLoadCompleted = delegate(object s, EventArgs e){};

		// ダウンロード進捗イベント
		public event Action<int> DownLoadProgressing = delegate(int progress){};

		// ロード完了イベント
		public event Action<UnitsManager> LoadCompleted = delegate(UnitsManager units){};
		
		// ロード進捗イベント
		public event Action<int> LoadProgressing = delegate(int progress){};

		public UnitsManager Units{get; private set;}

		void Awake (){
			Units = new UnitsManager();
			Units.LoadProgressing += OnLoadProgressing;
			Units.LoadCompleted += OnLoadCompleted;
		}

		// Use this for initialization
		void Start () {
		}

		/// <summary>
		/// データをロードする.
		/// </summary>
		public void Load(){

			string path = Application.dataPath + "/" + filename + ".zip";

			if (Application.isWebPlayer)
			{
				StartCoroutine(WebLoad(path));
			}
			else
			{
				Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
				StartCoroutine(Units.Load(stream));
			}
		}

		/// <summary>
		/// WEBからデータをダウンロードする.
		/// </summary>
		/// <returns>The load.</returns>
		/// <param name="URL">URL.</param>
		IEnumerator WebLoad(string URL)
		{
			WWW www = new WWW(URL);
			
			while (!www.isDone){			
				DownLoadProgressing((int)(www.progress * 100));
				yield return null;
			}
			
			if (string.IsNullOrEmpty(www.error)){
				
			} 
			DownLoadCompleted(this, EventArgs.Empty);
			
			Stream stream = new MemoryStream (www.bytes, false);
			StartCoroutine(Units.Load(stream));
			
		}

		/// <summary>
		/// ロード完了イベント.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void OnLoadCompleted(object sender, EventArgs e){
			LoadCompleted(Units);
		}

		/// <summary>
		/// ロード進行イベント.
		/// </summary>
		/// <param name="progress">Progress.</param>
		void OnLoadProgressing(int progress){
			LoadProgressing(progress);
		}

	}
}

