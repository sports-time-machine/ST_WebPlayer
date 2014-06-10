using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using SportsTimeMachinePlayer.Reader;
using SportsTimeMachinePlayer.Model;
using SportsTimeMachinePlayer.Transformer;

namespace SportsTimeMachinePlayer.Unit
{
	/// <summary>
	/// 1つのユニット(カメラ2台)を表すクラス.
	/// 動画の再生,停止を行う.
	/// </summary>
	public class Unit : MonoBehaviour {
		// ボクセルのマテリアル
		public Material voxcelMaterial;
		
		private List<Vector3> dots;
		private List<Frame> frames;
		public int MaxFrameCount{get;private set;}
		public int FrameCount{get;private set;}
		public int TotalMilliSecond{get;private set;}
		public bool IsEnd{get;private set;}
		private int totalFrames;
		private Unit unit;

		private ParticleSystem.Particle[] cloud;

		// Use this for initialization
		void Start () {
			FrameCount = 0;
		}

		/// <summary>
		/// ストリームからStmovデータをロードする.
		/// </summary>
		/// <param name="stream">Stream.</param>
		public void Load(Stream stream) {
			using (StmovReader reader = new StmovReader(stream)){
				frames = reader.ReadFrames ();
				totalFrames = reader.ReadTotalFrames();
				MaxFrameCount = reader.ReadTotalFrames();
				TotalMilliSecond = reader.ReadTotalMilliSeconds();
			}
		}


		/// <summary>
		/// データを再生する.
		/// </summary>
		/// <param name="frameCount">再生するフレーム番号.</param>
		public void Play(int frameCount) {
			if (frameCount > totalFrames - 1){
				IsEnd = true;
				return;
			}
			dots = frames[frameCount].GetPointCloud();
			if (dots.Count != 0) SetPoints(dots);
			IsEnd = false;
		}

		public void Update(){
			if  (dots == null || dots.Count == 0) return;
			particleSystem.SetParticles(cloud, cloud.Length);
		}

		/// <summary>
		/// 3次元情報をパーティクルにセットする.
		/// </summary>
		/// <param name="positions">Positions.</param>
		private void SetPoints(List<Vector3> positions){
			cloud = new ParticleSystem.Particle[positions.Count];
			for (int i = 0; i < positions.Count; ++i){
				cloud[i].position = positions[i];
				cloud[i].color = new Color(255,0,255);
				cloud[i].size = 0.03f;
			}
		}

	}
}
