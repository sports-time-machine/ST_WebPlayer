using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using SportsTimeMachine.IO;
using SportsTimeMachine.Data.Units;

namespace SportsTimeMachinePlayer.Fields
{
	/// <summary>
	/// 1つのユニット(カメラ2台)を表すクラス.
	/// 点群データの表示を行う.
	/// </summary>
	public class Field : MonoBehaviour {
		// ボクセルのマテリアル
		public Material voxcelMaterial;

		public float voxcelSize = 0.01f;

		private List<SportsTimeMachine.Data.Commons.Vector3> dots;
		public bool IsEnd{get;private set;}

		private ParticleSystem particles;

		private ParticleSystem.Particle[] cloud;

		// Use this for initialization
		void Start () {
			particles = GetComponent<ParticleSystem>();
		}

		void OnGUI()
		{
			if (cloud != null && gameObject.name.Equals("Unit1")){
				GUI.Label(new Rect(0,10,100,50), "");
			}
		}

		public void SetUnitPointCloud(UnitPointCloud pointCloud){
			dots = pointCloud.VectorList;
			if (dots != null && dots.Count != 0) SetPoints(dots);
		}
	
		public void Update(){

		}

		/// <summary>
		/// 3次元情報をパーティクルにセットする.
		/// </summary>
		/// <param name="positions">Positions.</param>
		private void SetPoints(List<SportsTimeMachine.Data.Commons.Vector3> positions){
			particles.Emit (positions.Count);
			cloud = new ParticleSystem.Particle[positions.Count];
			particles.GetParticles(cloud);
			for (int i = 0; i < positions.Count; ++i){
				cloud[i].position = new Vector3(positions[i].x, positions[i].y, positions[i].z);
				cloud[i].color = particles.startColor;
				cloud[i].size =  voxcelSize;
				cloud[i].velocity = Vector3.zero;
			}
			particles.SetParticles(cloud, cloud.Length);
		}

	}
}
