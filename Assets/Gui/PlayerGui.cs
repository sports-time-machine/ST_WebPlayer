using UnityEngine;
using System.Collections;
using SportsTimeMachinePlayer.Unit;
using SportsTimeMachinePlayer.Common;
using System;

namespace SportsTimeMachinePlayer.Gui
{
	public class PlayerGui : MonoBehaviour
	{
		int x,y;
		private UnitsManager manager;
		private double time;
		private Fps fps;
		private PlayStatus status;

		void Awake(){
			status = ((PlayManager)GameObject.Find("UnitsController").
			          GetComponent("PlayManager")).Status;
		}

		// Use this for initialization
		void Start()
		{
			x = 30;
			y = 420;
			fps = new Fps();

		}
		
		// Update is called once per frame
		void Update()
		{
			fps.Update();
			time = Math.Round(status.PlayTime, 3, MidpointRounding.AwayFromZero);
		}
		
		void OnGUI()
		{
			if (status.IsPlayable){
				GUI.Box(new Rect(0,425,640,480-425),"");
				string buttonText = "再生";
				if (status.IsPlaying == true) buttonText = "停止";

				if (GUI.Button(new Rect(x+20,y+20,40,25),buttonText)){
					if (status.IsPlaying == false){
						status.IsPlaying = true;
						if (status.IsEnd == true){
							status.FrameCount = 0;
						}
					}else{
						status.IsPlaying = false;
					}
				}
				status.FrameCount = (int)GUI.HorizontalSlider(new Rect(x+68,y+26,350,25),
				                    status.FrameCount,0.0f,status.MaxFrameCount);

				GUI.Label(new Rect(x+426,y+13,200,25),time.ToString("00.000") + "秒");

				int frameCount = status.FrameCount + 1;
				if ( frameCount > status.MaxFrameCount) frameCount = status.MaxFrameCount;
				GUI.Label(new Rect(x+426,y+32,200,25),
				          "(" + frameCount.ToString("0000")+ "/" +
				          status.MaxFrameCount.ToString("0000") + ")");

				GUI.Label(new Rect(x+530,y+35,100,50), "FPS：" + fps.Value);
			}
		}


	}
}
