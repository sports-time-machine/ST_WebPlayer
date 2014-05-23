using System;
using UnityEngine;
using System.Collections.Generic;
using SportsTimeMachinePlayer.Model;
using SportsTimeMachinePlayer.CompressFormat;

namespace SportsTimeMachinePlayer.VoxcelTransformer
{
	/// <summary>
	/// 深度情報を三次元ボクセル情報に変換するクラス.
	/// カメラ2台で1つのスクリーンを投影させる標準的なスポーツタイムマシンの記録形式の変換を行う.
	/// </summary>
	public class VoxcelTransformer : IVoxcelTransformer
	{
		/// <summary>
		/// 横方向解像度.
		/// </summary>
		private const int RESOLUTION_WIDTH = 640;

		/// <summary>
		/// 縦方向解像度.
		/// </summary>
		private const int RESOLUTION_HEIGHT = 480;

		/// <summary>
		/// カメラ1のカメラ情報.
		/// </summary>
		private CameraInfo camera1Info;

		/// <summary>
		/// カメラ2のカメラ情報.
		/// </summary>
		private CameraInfo camera2Info;

		/// <summary>
		/// コンストラクタ.
		/// </summary>
		/// <param name="camera1Info">カメラ1情報.</param>
		/// <param name="camera2Info">カメラ2情報.</param>
		public VoxcelTransformer (CameraInfo camera1Info, CameraInfo camera2Info)
		{
			this.camera1Info = camera1Info;
			this.camera2Info = camera2Info;
		}

		/// <summary>
		/// フレーム情報からボクセルのリストを作成する.
		/// </summary>
		/// <returns>ボクセルのリスト</returns>
		/// <param name="frame">フレーム.</param>
		public List<Vector3> GetVocelList(Frame frame)
		{
			UnitDepth unitDepth = frame.GetDepthList();

			List<Vector3> voxcels = new List<Vector3>();

			List<Depth> screen1DepthList = unitDepth.DepthList1;
			List<Depth> screen2DepthList = unitDepth.DepthList2;

			voxcels.AddRange(GetScreenVoxcels(camera1Info, screen1DepthList));
			voxcels.AddRange(GetScreenVoxcels(camera2Info, screen2DepthList));
			return voxcels;
		}

		/// <summary>
		/// カメラのスクリーンに投影された深度情報を元にボクセルのリストを作成する.
		/// </summary>
		/// <returns>ボクセルのリスト</returns>
		/// <param name="camera">カメラ情報.</param>
		/// <param name="depthList">深度情報のリスト.</param>
		private List<Vector3> GetScreenVoxcels(CameraInfo camera, List<Depth> depthList){

			List<Vector3> voxcels = new List<Vector3>(depthList.Count);
			Matrix4x4 camMatrix = camera.GetMatrix();

			for (int i = 0; i < depthList.Count; ++i){
				Depth depth = depthList[i];
							
				Vector3 vec = new Vector3(
					(((RESOLUTION_WIDTH/2)- depth.X)/(float)RESOLUTION_WIDTH),
					(((RESOLUTION_HEIGHT/2)- depth.Y)/(float)RESOLUTION_HEIGHT),
					depth.Value/1000.0f
				);
				
				Vector4 vec4 = new Vector4(vec.x * vec.z,vec.y * vec.z, vec.z,1.0f);
				Vector4 point = camMatrix * vec4;
				voxcels.Add(new Vector3(point.x, point.y, point.z));
			}
			return voxcels;
		}
	}
}

