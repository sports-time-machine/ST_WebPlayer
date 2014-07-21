using UnityEngine;
using System.Collections;
using SportsTimeMachinePlayer.Fields;
using System;

namespace SportsTimeMachinePlayer.Gui
{
	public class PlayOptionGui : MonoBehaviour
	{
		private Rect position = new Rect(640-200,0,200,100);
		public GUIStyle style;
		private PlayOption option;
		private PlayStatus status;
		
		// Use this for initialization
		void Start()
		{
			PlayManager manager = ((PlayManager)GameObject.Find("UnitsController").
			          	GetComponent("PlayManager"));

			manager.Playabled += OnPlayabled;
			option = manager.Option;
		}
				
		void OnGUI()
		{
			if (status != null){
				GUILayout.Window(1, position, DrawWindow, "さいせいオプション");
			}
		}

		private void DrawWindow(int id){
			option.IsDrawSkip = 
				GUILayout.Toggle(option.IsDrawSkip, "再生を実際の時間に合わせる");
		}

		void OnPlayabled(PlayStatus status){
			this.status = status;
		}
	}
}
