using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using SportsTimeMachinePlayer.Common;
using ICSharpCode.SharpZipLib.Zip;
using SportsTimeMachine.IO;
using SportsTimeMachine.Data.Tracks;

namespace SportsTimeMachinePlayer.Fields
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
		public event Action<Track> LoadCompleted = delegate(Track track){};
		
		// ロード進捗イベント
		public event Action<int> LoadProgressing = delegate(int progress){};

		private Stream trackStream;

		void Awake (){
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
				trackStream = new FileStream(path, FileMode.Open, FileAccess.Read);
				TrackReader reader = new TrackReader(trackStream);
				reader.ReadProgressing += OnReadProgressing;
				reader.ReadCompleted += OnReadCompleted;
				StartCoroutine(reader.ReadAsync());
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

			trackStream = new MemoryStream (www.bytes, false);

			TrackReader reader = new TrackReader(trackStream);
			reader.ReadProgressing += OnReadProgressing;
			reader.ReadCompleted += OnReadCompleted;
			StartCoroutine(reader.ReadAsync());
		}

		/// <summary>
		/// 読み込み完了イベント.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void OnReadCompleted(object sender, CompleteEventArgs e){
			LoadCompleted(e.Track);
		}

		/// <summary>
		/// ロード進行イベント.
		/// </summary>
		/// <param name="progress">Progress.</param>
		void OnReadProgressing(object sender, ProgressEventArgs e){
			LoadProgressing(e.Value);
		}

	}
}

