using UnityEngine;
using System;
using SportsTimeMachinePlayer.Common;
using SportsTimeMachineMovie.Data.Tracks;

namespace SportsTimeMachinePlayer.Fields
{
	public class PlayManager : MonoBehaviour 
	{
		// 再生可能イベント
		public event Action<PlayStatus> Playabled = delegate {};
	
		private Field[] fields;

		private bool isLoaded;
		public PlayOption Option{get; private set;}
		public PlayStatus Status{get; private set;}
		private Track track;

		private float time;

		void Awake () {
			Application.targetFrameRate = 30; 
			Option = new PlayOption();
			Status = new PlayStatus();
			isLoaded = false;
			time = 0.0f;
		}

		// Use this for initialization
		void Start () {
			LoadManager loadManager = (LoadManager)GetComponent("LoadManager");
			loadManager.LoadCompleted += OnLoadCompleted;
			loadManager.Load();

			Status.FrameCountChanged += OnFrameCountChanged;

			fields = new Field[6];
			fields[0] = (Field)GameObject.Find("Unit1").GetComponent("Field");
			fields[1] = (Field)GameObject.Find("Unit2").GetComponent("Field");
			fields[2] = (Field)GameObject.Find("Unit3").GetComponent("Field");
			fields[3] = (Field)GameObject.Find("Unit4").GetComponent("Field");
			fields[4] = (Field)GameObject.Find("Unit5").GetComponent("Field");
			fields[5] = (Field)GameObject.Find("Unit6").GetComponent("Field");

		}

		void Update (){
			if (isLoaded){
				if (Status.IsPlaying == true ){
					time += Time.deltaTime;
					int count = 0;
					while(time > 0.03333f){
						count++;
						time -= 0.03333f;
					}
					Status.FrameCount += count;
				}else
				{
					time = 0.0f;
				}
			}
		}

		public void Show(){
			TrackPointCloud trackPointCloud = track.GetTrackPointCloud(Status.FrameCount);
			if (trackPointCloud != null){
				fields[0].SetUnitPointCloud(trackPointCloud.GetUnitPointCloud(UnitNumber.ONE));
				fields[1].SetUnitPointCloud(trackPointCloud.GetUnitPointCloud(UnitNumber.TWO));
				fields[2].SetUnitPointCloud(trackPointCloud.GetUnitPointCloud(UnitNumber.THREE));
				fields[3].SetUnitPointCloud(trackPointCloud.GetUnitPointCloud(UnitNumber.FOUR));
				fields[4].SetUnitPointCloud(trackPointCloud.GetUnitPointCloud(UnitNumber.FIVE));
				fields[5].SetUnitPointCloud(trackPointCloud.GetUnitPointCloud(UnitNumber.SIX));
			}
		}

		void OnFrameCountChanged(){

			if (Option.IsDrawSkip){
				// 処理落ち時に描画のスキップを行う.
				if (Time.deltaTime < 0.03333){
					Show();
				}
			}else{
				// 処理落ち時でも全フレームの描画を行う.
				Show ();
			}
		}

		void OnLoadCompleted(Track track){
			this.track = track;
			isLoaded = true;
			Status.MaxPlayTime = track.TotalTime;
			Status.MaxFrameCount = track.TotalFrame;
			Playabled(Status);
			Show();
		}
	
	}
}

