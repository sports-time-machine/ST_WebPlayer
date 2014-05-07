using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using SportsTimeMachinePlayer.Reader;
using SportsTimeMachinePlayer.Model;
using SportsTimeMachinePlayer.VoxcelTransformer;

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
		private IVoxcelTransformer transformer;
		private int frameCount;
		private int totalFrames;
		private Unit unit;
		
		// ロード完了イベント
		public event EventHandler LoadCompleted = delegate(object s, EventArgs e){};

		// Use this for initialization
		void Start () {
			frameCount = 0;
		}

		/// <summary>
		/// ストリームからStmovデータをロードする.
		/// </summary>
		/// <param name="stream">Stream.</param>
		public void Load(Stream stream) {
			StmovReader reader = new StmovReader(stream);
			frames = reader.ReadFrames ();
			transformer = new VoxcelTransformer.VoxcelTransformer (
				reader.ReadCamera1Info (),
				reader.ReadCamera2Info ()
			);
			totalFrames = reader.ReadTotalFrames();

			// ロード完了イベント発生
			if (LoadCompleted != null) LoadCompleted(this, EventArgs.Empty);
		}

		/// <summary>
		/// データを再生する.
		/// </summary>
		public void Play() {

			List<Vector3> oldDots = null;
			if (dots != null){
				oldDots = new List<Vector3>(dots);
			}

			dots = transformer.GetVocelList (frames[frameCount]); // unit.GetDepth (frameCount);
			frameCount+=2;
			if (frameCount > totalFrames - 1) frameCount = 0;
			if (dots.Count == 0){
				dots = oldDots;
			}
		}
		
		void OnRenderObject(){
			if  (dots == null || dots.Count == 0) return;
			float size = 0.012f;

			foreach(Vector3 vec in dots){
				GL.PushMatrix();
				voxcelMaterial.SetPass(0);
				GL.Begin(GL.QUADS);
				GL.Color(Color.magenta);
				GL.Vertex3(transform.position.x + vec.x - size,transform.position.y + vec.y + size, transform.position.z + vec.z);
				GL.Vertex3(transform.position.x + vec.x + size,transform.position.y + vec.y + size, transform.position.z + vec.z);
				GL.Vertex3(transform.position.x + vec.x + size,transform.position.y + vec.y - size, transform.position.z + vec.z);
				GL.Vertex3(transform.position.x + vec.x - size,transform.position.y + vec.y - size, transform.position.z + vec.z);
				GL.End();
				GL.PopMatrix();
			}

		}
	}
}
