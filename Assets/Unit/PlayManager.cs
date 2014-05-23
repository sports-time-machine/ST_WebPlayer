using UnityEngine;
using System;
using SportsTimeMachinePlayer.Common;

namespace SportsTimeMachinePlayer.Unit
{
	public class PlayManager : MonoBehaviour 
	{

		public PlayOption Option{get; private set;}
		public PlayStatus Status{get; private set;}
		private UnitsManager units;

		void Awake () {
			Option = new PlayOption();
			Status = new PlayStatus();
		}

		// Use this for initialization
		void Start () {
			LoadManager loadManager = (LoadManager)GetComponent("LoadManager");
			loadManager.Load();
			loadManager.LoadCompleted += OnLoadCompleted;
		}

		void Update (){
						
			Application.targetFrameRate = 30/Option.IncreaseFrame; 
			
			if (Status.IsPlayable == true ){
				units.Play(Status.FrameCount + Option.IncreaseFrame);
				Status.IsEnd = units.IsEnd;
				if (Status.IsEnd == true) Status.IsPlaying = false;
				if (Status.IsPlaying == true) Status.FrameCount += Option.IncreaseFrame;
				
				Status.PlayTime = Status.FrameCount * 0.03333;
			}
		}

		void OnLoadCompleted(UnitsManager units){
			this.units = units;
			Status.IsPlayable = true;
			Status.MaxFrameCount = units.MaxFrameCount;
		}
	}
}

