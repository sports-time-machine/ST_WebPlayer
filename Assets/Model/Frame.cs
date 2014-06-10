using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SportsTimeMachinePlayer.Format;
using SportsTimeMachinePlayer.Transformer;

namespace SportsTimeMachinePlayer.Model{

	/// <summary>
	/// フレーム情報を扱うクラス.
	/// フレーム情報は圧縮された状態で格納されており,そのままでは使用することができない.
	/// GetDepthList関数を利用することにより,解凍された状態の深度リストを取得することが可能.
	/// </summary>
	public class Frame 
	{
		/// <summary>
		/// フレーム情報バイト列.
		/// </summary>
		private byte[] bytes;

		/// <summary>
		/// フレーム情報バイト数を取得する.
		/// </summary>
		/// <value>フレーム情報バイト数.</value>
		public int Size{ get; private set; }

		/// <summary>
		/// 圧縮情報フォーマットを取得する.
		/// </summary>
		/// <value>圧縮情報フォーマット.</value>
		private CompressFormat format;

		private VoxcelTransformer transformer;

		/// <summary>
		/// コンストラクタ.
		/// </summary>
		/// <param name="bytes">フレーム情報バイト列.</param>
		/// <param name="format">圧縮フォーマット.</param>
		public Frame (byte[] bytes, CompressFormat format, VoxcelTransformer transformer)
		{
			this.bytes = bytes;
			Size = bytes.Length;
			this.format = format;
			this.transformer = transformer;
		}

		/// <summary>
		/// 圧縮されたフレーム情報を解凍し,点群リストを作成する.
		/// </summary>
		/// <returns>The point cloud.</returns>
		public List<Vector3> GetPointCloud(){
			return transformer.GetVocelList(format.Decompress(bytes));
		}
	}
}

