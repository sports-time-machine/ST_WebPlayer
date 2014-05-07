using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using ICSharpCode.SharpZipLib.Zip;

namespace SportsTimeMachinePlayer.Unit
{
	public class UnitsController : MonoBehaviour {

		public string fileName;
		private Unit[] units;
		private Stream stream;
		
		int playableCount = 0;

		void Awake() {
			Application.targetFrameRate = 15;	// 15fps
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

			string path = Application.dataPath + "/" + fileName + ".zip";

			foreach (Unit unit in units){
				unit.LoadCompleted += OnLoadCompleted;
			}

			if (Application.isWebPlayer)
			{
				StartCoroutine(WebLoad(path));
			}
			else
			{
				using (stream = new FileStream(path, FileMode.Open, FileAccess.Read)){
					LoadTracks(stream);
				}
			}
		}
		
		IEnumerator WebLoad(string URL)
		{
			WWW www = new WWW(URL);
			yield return www;

			using (stream = new MemoryStream (www.bytes, false)) {
				LoadTracks (stream);
			}
		}
		
		void OnLoadCompleted(object sender, EventArgs e){
			CheckPlayable();
		}

		void CheckPlayable(){
			playableCount++;
		}
		
		void Update (){
			if (playableCount == 6){
				foreach (Unit unit in units){
					unit.Play();
				}
			}
		}

		void LoadTracks(Stream stream){
			using (ZipInputStream zipInputStream = new ZipInputStream(stream)) {
				for (int i = 0; i < 6; i++) {
					zipInputStream.GetNextEntry ();
					using (MemoryStream unitStream = new MemoryStream ()){
						byte[] buffer = new byte[1024];
						int len;
						while ((len = zipInputStream.Read(buffer,0, 1024)) > 0) {
								unitStream.Write (buffer, 0, len);
						}
						units [i].Load (unitStream);
					}
				}
			}
		}

	}
}
