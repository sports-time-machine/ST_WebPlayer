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
		private UnitsController controller;
		
		// Use this for initialization
		void Start()
		{
			controller = (UnitsController)GameObject.Find("UnitsController").
				GetComponent("UnitsController");
		}
				
		void OnGUI()
		{
			if (controller.IsPlayable){
				GUILayout.Window(1, position, DrawWindow, "さいせいオプション");
			}
		}

		private void DrawWindow(int id){
			controller.Option.IsDoubleFrames = 
				GUILayout.Toggle(controller.Option.IsDoubleFrames, "1フレームづつ飛ばす");
		}
	}
}
