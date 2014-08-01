using System;
using UnityEngine;
using System.Collections;
using SportsTimeMachinePlayer.Fields;
using SportsTimeMachinePlayer.Common;

namespace SportsTimeMachinePlayer.Gui
{
	public class ProgressBarGui
	{
		private PlayStatus status;
		private PlayOption option;
		public int X {get; set;}
		public int Y {get; set;}

		public int FrameCount {get; set;}
		public double PlayTime {get; set;}

		public int MaxFrameCount {get; set;}
		public int MaxPlayTime {get; set;}

		private int pushCount;
		private bool prevClicked;
		private bool nextClicked;

		public ProgressBarGui (PlayStatus status, PlayOption option)
		{
			this.status = status;
			this.option = option;
			X = 0;
			Y = 0;
			FrameCount = 0;
			PlayTime = 0.0;
			MaxFrameCount = 0;
			MaxPlayTime = 0;
			pushCount = 0;
		}

		public void Update(){

			// スペースキーで再生停止.
			if (Input.GetKeyDown(KeyCode.Space)){
				if (status.IsPlaying) status.IsPlaying = false;
				else status.IsPlaying = true;
			}	

			// Qで前コマ送り.
			if (Input.GetKeyDown(KeyCode.Q)) StepPrev();
			// Eで後コマ送り.
			if (Input.GetKeyDown(KeyCode.E)) StepNext();

			if (prevClicked == false && nextClicked == false){
				pushCount = 0;
			}else{
				pushCount++;
			}
			if (prevClicked && pushCount % 3 == 1) StepPrev();
			if (nextClicked && pushCount % 3 == 1) StepNext();


		}

		public void ShowGui(){

			// ボタン
			string buttonText = "再生";
			if (status.IsPlaying == true) buttonText = "停止";
			if (GUI.Button(new Rect(X, Y + 10, 40, 25),buttonText)){
				if (status.IsPlaying == false) status.IsPlaying = true;
				else status.IsPlaying = false;
			}

			// カメラ切り替え
			if (GUI.Button(new Rect(X + 465, Y + 10, 100, 25), "カメラ切り替え")){
				if (option.IsFixCamera) option.IsFixCamera = false;
				else option.IsFixCamera = true;
			}

			// 水平バー
			status.FrameCount = (int)GUI.HorizontalSlider(new Rect(X + 70 + 65, Y + 13,170,25),
			                                              status.FrameCount,0.0f,status.MaxFrameCount);

			// 再生時間・経過フレーム数
			GUI.Label(new Rect(X + 65,Y,200,25),PlayTime.ToString("00.000") + "秒");
			GUI.Label(new Rect(X + 65,Y + 19,200,25), "(" + FrameCount.ToString("0000") + ")");

			// 総再生時間・総フレーム数
			double maxPlayTimeMilliseconds = (double)(status.MaxPlayTime / 1000.0);
			GUI.Label(new Rect(X + 255 + 65, Y, 200,25),maxPlayTimeMilliseconds.ToString("00.000") + "秒");
			GUI.Label(new Rect(X + 255 + 65, Y + 19,200,25), "(" + status.MaxFrameCount.ToString("0000") + ")");

			// コマ送り
			GUI.Label(new Rect(X + 390, Y ,200,25), "コマ送り");

			prevClicked = GUI.RepeatButton(new Rect(X + 390, Y + 18, 20, 20), "<");
			nextClicked = GUI.RepeatButton(new Rect(X + 390 + 30, Y + 18, 20, 20), ">");
		}

		/// <summary>
		///  前のフレームに戻る.
		/// コマ送り時は再生を停止する.
		/// </summary>
		private void StepPrev(){
			status.FrameCount--;
			status.IsPlaying = false;
		}

		/// <summary>
		///  次のフレームへ進む.
		/// コマ送り時は再生を停止する.
		/// </summary>
		private void StepNext(){
			status.FrameCount++;
			status.IsPlaying = false;
		}
	}
}

