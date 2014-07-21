using UnityEngine;
using System.Collections;
using SportsTimeMachinePlayer.Fields;
using SportsTimeMachinePlayer.Common;
using System;

namespace SportsTimeMachinePlayer.Gui
{
	public class PlayerGui : MonoBehaviour
	{
		private double time;
		private Fps fps;
		private PlayStatus status;
		private PlayOption option;
		private ProgressBarGui progressBarGui;

		void Awake(){
		}

		// Use this for initialization
		void Start()
		{
			PlayManager manager = ((PlayManager)GameObject.Find("UnitsController").
			                       GetComponent("PlayManager"));
			
			option = manager.Option;
			
			manager.Playabled += OnPlayabled;
			fps = new Fps();
		}
		
		// Update is called once per frame
		void Update()
		{
			fps.Update();
			if (status != null){
				status.Fps = fps.Value;
				progressBarGui.FrameCount = status.FrameCount + 1;
				if ( progressBarGui.FrameCount > status.MaxFrameCount) progressBarGui.FrameCount = status.MaxFrameCount;
				progressBarGui.PlayTime = Math.Round((progressBarGui.FrameCount * 33.33333) / 1000.0, 3, MidpointRounding.AwayFromZero);
				progressBarGui.Update();

				// Fキーが押されていれば,FPS表示のトグルを行う.
				if (Input.GetKeyDown(KeyCode.F)){
					if (option.ShowsFps) option.ShowsFps = false;
					else option.ShowsFps = true;
				}
			}
		}
		
		void OnGUI()
		{
			if (status != null){
				GUI.Box(new Rect(0,425,640,480-425),"");
				progressBarGui.ShowGui();
			}
	
			if (option.ShowsFps){
				GUI.Label(new Rect(570,10,100,50), "FPS:" + status.Fps);
			}
		}

		void OnPlayabled(PlayStatus status){
			this.status = status;
			progressBarGui = new ProgressBarGui(status, option);
			progressBarGui.X = 35;
			progressBarGui.Y = 433;
		}
	}
}
