using UnityEngine;
using System.Collections;
using SportsTimeMachinePlayer.Fields;
using SportsTimeMachineMovie.Data.Tracks;
using System;

namespace SportsTimeMachinePlayer.Gui
{
	public class ProgressCounter : MonoBehaviour
	{
		public Rect position;
		public GUIStyle style;
		private string text;
		private LoadManager manager;
		private int loadProgress;
		private int deCompressProgress;
		private bool isLoadCompleted;
		private bool isDeCompressCompleted;
		
		// Use this for initialization
		void Start()
		{
			loadProgress = 0;
			deCompressProgress = 0;
			isLoadCompleted = false;
			isDeCompressCompleted = false;
			text="";
			manager = (LoadManager)GameObject.Find("UnitsController").GetComponent("LoadManager");
			manager.DownLoadCompleted += OnDownLoadCompleted;
			manager.LoadCompleted += OnLoadCompleted;
			manager.DownLoadProgressing += OnDownLoadProgressing;
			manager.LoadProgressing += OnLoadProgressing;
		}
		
		// Update is called once per frame
		void Update()
		{
			if (Application.isWebPlayer)
			{
				if (isLoadCompleted == false){
					text = "ダウンロードちゅう…" + loadProgress + "％";
					
				}else if (isDeCompressCompleted == false){
					text = "じゅんびちゅう…" + deCompressProgress + "％";
				}
			}else{
				if (isDeCompressCompleted == false){
					text = "じゅんびちゅう…" + deCompressProgress + "％";
				}
			}
		}
		
		void OnGUI()
		{
			if (Application.isWebPlayer){
				if (isLoadCompleted == false || isDeCompressCompleted == false){
					GUI.Label(position, text, style);
				}
			}else{
				if (isDeCompressCompleted == false){
					GUI.Label(position, text, style);
				}
			}
		}
		
		void OnDownLoadCompleted(object sender, EventArgs e){
			isLoadCompleted = true;
		}
		
		void OnLoadCompleted(Track track){
			isDeCompressCompleted = true;
		}
		
		void OnDownLoadProgressing(int progress){
			loadProgress = progress;
		}

		void OnLoadProgressing(int progress){
			deCompressProgress = progress;
		}
	}
}
