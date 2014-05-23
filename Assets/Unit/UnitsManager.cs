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
	/// 複数ユニットを統括するクラス.
	/// </summary>
	public class UnitsManager {

		// ロード完了イベント
		public event EventHandler LoadCompleted = delegate(object s, EventArgs e){};
		
		// ロード進捗イベント
		public event Action<int> LoadProgressing = delegate(int progress){};


		private Unit[] units;
		public bool IsEnd{get;private set;}
		public int MaxFrameCount{get; private set;}

		public UnitsManager() {

			IsEnd = false;
			MaxFrameCount = 0;

			units = new Unit[6];
			units[0] = (Unit)GameObject.Find("Unit1").GetComponent("Unit");
			units[1] = (Unit)GameObject.Find("Unit2").GetComponent("Unit");
			units[2] = (Unit)GameObject.Find("Unit3").GetComponent("Unit");
			units[3] = (Unit)GameObject.Find("Unit4").GetComponent("Unit");
			units[4] = (Unit)GameObject.Find("Unit5").GetComponent("Unit");
			units[5] = (Unit)GameObject.Find("Unit6").GetComponent("Unit");
		}

		/// <summary>
		/// ユニットデータをロードする.
		/// </summary>
		/// <param name="stream">6ユニットのデータが入ったストリーム.</param>
		public IEnumerator Load(Stream stream){
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
							LoadProgressing(progress);
							yield return null;
						}
						units[i].Load (unitStream);
					}
				}
			}
			stream.Dispose();
			MaxFrameCount = units[0].MaxFrameCount;

			LoadCompleted(this, EventArgs.Empty);
		}

		public void Play(int frame){
			foreach (Unit unit in units){
				unit.Play(frame);
			}
			IsEnd = units[0].IsEnd;
		}

	
	}
}
