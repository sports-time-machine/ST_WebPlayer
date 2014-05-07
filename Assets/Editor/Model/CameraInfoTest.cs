using UnityEngine;
using System.IO;
using UnityTest;
using NUnit.Framework;
using SportsTimeMachinePlayer.Reader;
using SportsTimeMachinePlayer.Model;
using System.Collections.Generic;

namespace SportsTimeMachinePlayerTest.Model{
	
	/// <summary>
	/// カメラ情報クラスのテスト.
	/// </summary>
	public class CameraInfoTest {	
		Vector3 pos;
		Vector3 rot;
		Vector3 scale;

		[SetUp]
		public void SetUp(){
			pos = new Vector3 (0.1f, -0.1f, 0.1f);
			rot = new Vector3 (0.123f, 0.14f, -0.15f);
			scale = new Vector3 (1.1f, 1.1f, 1.1f);
		}

		/// <summary>
		/// 構築テスト.
		/// 位置,回転,拡縮がセットされていること.
		/// </summary>
		[Test]
		public void CameraInfoTest01(){
			CameraInfo info = new CameraInfo (pos, rot, scale);

			Assert.AreEqual (pos, info.Position);
			Assert.AreEqual (rot, info.Rotation);
			Assert.AreEqual (scale, info.Scale);
		}

		/// <summary>
		/// カメラ行列取得テスト.
		/// 位置,回転,拡縮からカメラの行列が正しく取得できること.
		/// </summary>
		/*
		[Test]
		public void GetMatrixTest01(){
			CameraInfo info = new CameraInfo (pos, rot, scale);
			Matrix4x4 target = info.GetMatrix ();
			Matrix4x4 mat = new Matrix4x4 ();
			mat.m00 = 1.07701f;
			mat.m01 = 0.18176f;
			mat.m02 = 0.13046f;
			mat.m03 = 0.10000f;
			mat.m10 = -0.16277f;
			mat.m11 = 1.07622f;
			mat.m12 = -0.15621f;
			mat.m13 = -0.10000f;
			mat.m20 = -0.15350f;
			mat.m21 = 0.13364f;
			mat.m22 = 1.08101f;
			mat.m23 = 0.10000f;
			mat.m30 = 0.00000f;
			mat.m31 = 0.00000f;
			mat.m32 = 0.00000f;
			mat.m33 = 1.00000f;

			Assert.AreEqual (mat, target);
		}
		*/
	}
}
