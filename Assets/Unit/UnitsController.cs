using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using SportsTimeMachinePlayer.Model;
using ICSharpCode.SharpZipLib.Zip;

namespace SportsTimeMachinePlayer.Unit
{
	public class UnitsController : MonoBehaviour {

		public string fileName;
		private Unit[] units;
		private Stream stream;
		private int incFrame;
		private string path;
		public bool IsPlayable{get;private set;}
		public bool IsEnd{get;private set;}
		public int FrameCount{get;set;}
		public bool IsPlaying{get;set;}

		public PlayOption Option{get;private set;}
		public int MaxFrameCount{get; private set;}
		public double PlayTime{get; private set;}
		public double TotalTime{get; private set;}


		// ロード完了イベント
		public event EventHandler LoadCompleted = delegate(object s, EventArgs e){};

		// ロード進捗イベント
		public event Action<int> LoadProgressing = delegate(int progress){};

		// 解凍完了イベント
		public event EventHandler DeCompressCompleted = delegate(object s, EventArgs e){};

		// 解凍進捗イベント
		public event Action<int> DeCompressProgressing = delegate(int progress){};


		void Awake() {
			incFrame = 1;
			FrameCount = 0;
			Application.targetFrameRate = 30/incFrame;	// 15fps
			Option = new PlayOption();
			IsPlayable = false;
			IsPlaying = false;
		}

		// Use this for initialization
		void Start () {
			units = new Unit[6];
			
			units[0] = (Unit)GameObject.Find("Unit1").GetComponent("Unit");
			units[1] = (Unit)GameObject.Find("Unit2").GetComponent("Unit");
			units[2] = (Unit)GameObject.Find("Unit3").GetComponent("Unit");
			units[3] = (Unit)GameObject.Find("Unit4").GetComponent("Unit");
			units[4] = (Unit)GameObject.Find("Unit5").GetComponent("Unit");
			units[5] = (Unit)GameObject.Find("Unit6").GetComponent("Unit");

			path = Application.dataPath + "/" + fileName + ".zip";

			if (Application.isWebPlayer)
			{
				StartCoroutine(WebLoad(path));
			}
			else
			{
				stream = new FileStream(path, FileMode.Open, FileAccess.Read);
				StartCoroutine(LoadTracks(stream));
			}
		}
		
		IEnumerator WebLoad(string URL)
		{
			WWW www = new WWW(URL);

			while (!www.isDone){			
				LoadProgressing((int)(www.progress * 100));
				yield return null;
			}

			if (string.IsNullOrEmpty(www.error)){
			
			} 
			LoadCompleted(this, EventArgs.Empty);

			stream = new MemoryStream (www.bytes, false);
			StartCoroutine(LoadTracks(stream));

		}

		void Update (){
			if (Option.IsDoubleFrames == true) incFrame = 2;
			else incFrame = 1;

			Application.targetFrameRate = 30/incFrame; 

			if (IsPlayable == true ){
				foreach (Unit unit in units){
					unit.Play(FrameCount + incFrame);
				}

				IsEnd = units[0].IsEnd;
				if (IsEnd == true){
					IsPlaying = false;
				}

				if (IsPlaying == true){
					FrameCount += incFrame;
				}
				PlayTime = FrameCount * 0.03333;


			}
		}

		IEnumerator LoadTracks(Stream stream){
			using (ZipInputStream zipInputStream = new ZipInputStream(stream)) {
				for (int i = 0; i < 6; i++) {
					zipInputStream.GetNextEntry ();
					long maxLength = zipInputStream.Length;
					using (MemoryStream unitStream = new MemoryStream ()){
						byte[] buffer = new byte[2097152];
						int len;
						while ((len = zipInputStream.Read(buffer,0, 2097152)) > 0) {
							unitStream.Write (buffer, 0, len);
							int progress = (int)((unitStream.Length/(float)maxLength) * 16.666 + 16.666 * i);
							DeCompressProgressing(progress);
							yield return null;
						}
						units [i].Load (unitStream);
					}
				}
			}
			MaxFrameCount = units[0].MaxFrameCount;
			TotalTime = units[0].TotalMilliSecond/1000.0;
			IsPlayable = true;
			stream.Dispose();
			DeCompressCompleted(this, EventArgs.Empty);
		}

	}
}
