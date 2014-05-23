using UnityEngine;
using System.Collections;
using SportsTimeMachinePlayer.Unit;
using System;

namespace SportsTimeMachinePlayer.Gui
{
	public class PlayOptionGui : MonoBehaviour
	{
		private Rect position = new Rect(640-200,0,200,100);
		public GUIStyle style;
		private bool isDoubleFrame;
		private PlayOption option;
		private PlayStatus status;
		
		// Use this for initialization
		void Start()
		{
			PlayManager manager = ((PlayManager)GameObject.Find("UnitsController").
			          	GetComponent("PlayManager"));

			option = manager.Option;
			status = manager.Status;
		}
				
		void OnGUI()
		{
			if (status.IsPlayable){
				GUILayout.Window(1, position, DrawWindow, "さいせいオプション");
			}
		}

		private void DrawWindow(int id){
			option.IsDoubleFrame = 
				GUILayout.Toggle(option.IsDoubleFrame, "1フレームづつ飛ばす");
		}
	}
}
