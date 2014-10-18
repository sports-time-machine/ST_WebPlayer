using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using SportsTimeMachinePlayer.Common;
using ICSharpCode.SharpZipLib.Zip;
using SportsTimeMachine.IO;
using SportsTimeMachine.Data.Tracks;
using System.Security;
using System.Text;

namespace SportsTimeMachinePlayer.Fields
{

	/// <summary>
	/// スポーツタイムマシンのロードマネージャ.
	/// WEBからのデータのダウンロードとダウンロードしたデータのロードの操作を行う.
	/// </summary>
	public class LoadManager : MonoBehaviour 
	{

		
		/// <summary>
		/// パスコード.
		/// サーバー側と一致すること.
		/// </summary>
		public string passcode;
		
		/// <summary>
		/// ホスト名.
		/// </summary>
		public string hostname;
		
		public delegate void getRecieverEvent (String url);
		private getRecieverEvent recieveEv;
		
	
		/// <summary>
		/// ファイル名.DIされる.
		/// </summary>
		public string filename;

		public bool isDebug;

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

			if (Application.isWebPlayer)
			{
				string path = "";
				if (isDebug) {
					path = Application.dataPath + "/" + filename + ".zip";
					StartCoroutine(WebLoad(path));
				}else{
					Application.ExternalCall ("getData");
				}
			}
			else
			{
				string path = Application.dataPath + "/" + filename + ".zip";
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

		void Recieve(String listString)
		{
			String[] paramStrings = listString.Split (',');

			String userId = paramStrings [0];
			String recordId = paramStrings [1];
			String hash = paramStrings [2];

			// ハッシュ値計算.
			// テキストをUTF-8エンコードでバイト配列化
			byte[] byteValue = Encoding.UTF8.GetBytes(userId + recordId + passcode);
			// MD5のハッシュ値を取得する
			System.Security.Cryptography.MD5 md5 =
				System.Security.Cryptography.MD5.Create ();
			byte[] bs = md5.ComputeHash(byteValue);
			md5.Clear ();
			String generateHash = BitConverter.ToString(bs).ToLower().Replace("-","");
			if (!hash.Equals(generateHash)){
				throw new SecurityException("ハッシュ値が異なります.生成されたhash:" + generateHash);
			}

			string userIdUrl = "";
			for (int i = userId.Length -1 ; i >= 0; i--){
				userIdUrl += userId[i] + "/";
			}

			string path = hostname + userIdUrl + recordId + ".zip";

			StartCoroutine(WebLoad(path));

		}			
	}
}

