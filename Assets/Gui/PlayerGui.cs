using UnityEngine;
using System.Collections;
using SportsTimeMachinePlayer.Unit;
using SportsTimeMachinePlayer.Model;
using System;

namespace SportsTimeMachinePlayer.Gui
{
	public class PlayerGui : MonoBehaviour
	{
		int x,y;
		private UnitsController controller;
		private double time;
		private Fps fps;
		// Use this for initialization
		void Start()
		{
			x = 30;
			y = 420;
			controller = (UnitsController)GameObject.Find("UnitsController").
				GetComponent("UnitsController");
			fps = new Fps();

		}
		
		// Update is called once per frame
		void Update()
		{
			fps.Update();
			time = Math.Round(controller.PlayTime, 3, MidpointRounding.AwayFromZero);
		}
		
		void OnGUI()
		{
			GUI.Box(new Rect(0,425,640,480-425),"");

			if (controller.IsPlayable){
				string buttonText = "再生";
				if (controller.IsPlaying == true) buttonText = "停止";

				if (GUI.Button(new Rect(x+20,y+20,40,25),buttonText)){
					if (controller.IsPlaying == false){
						controller.IsPlaying = true;
						if (controller.IsEnd == true){
							controller.FrameCount = 0;
						}
					}else{
						controller.IsPlaying = false;
					}
				}
				controller.FrameCount = (int)GUI.HorizontalSlider(new Rect(x+68,y+26,350,25),
				                     controller.FrameCount,0.0f,controller.MaxFrameCount);

				GUI.Label(new Rect(x+426,y+13,200,25),time.ToString("00.000") + "秒");
				GUI.Label(new Rect(x+426,y+32,200,25),
				          "(" + controller.FrameCount.ToString("0000")+ "/" +
				          controller.MaxFrameCount.ToString("0000") + ")");

				GUI.Label(new Rect(x+530,y+35,100,50), "FPS：" + fps.Value);
			}
		}


	}
}
