using UnityEngine;
using System.Collections;
using SportsTimeMachinePlayer.Unit;
using System;

namespace SportsTimeMachinePlayer.Gui
{
	public class ProgressCounter : MonoBehaviour
	{
		public Rect position;
		public GUIStyle style;
		private string text;
		private UnitsController controller;
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
			controller = (UnitsController)GameObject.Find("UnitsController").
				GetComponent("UnitsController");
			controller.LoadCompleted += OnLoadCompleted;
			controller.DeCompressCompleted += OnDeCompressCompleted;
			controller.LoadProgressing += OnLoadProgressing;
			controller.DeCompressProgressing += OnDeCompressProgressing;
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
		
		void OnLoadCompleted(object sender, EventArgs e){
			isLoadCompleted = true;
		}
		
		void OnDeCompressCompleted(object sender, EventArgs e){
			isDeCompressCompleted = true;
		}
		
		void OnLoadProgressing(int progress){
			loadProgress = progress;
		}

		void OnDeCompressProgressing(int progress){
			deCompressProgress = progress;
		}
	}
}
