using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SportsTimeMachinePlayer.CompressFormat;

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
		public ICompressFormat Format{ get; private set; }

		/// <summary>
		/// コンストラクタ.
		/// </summary>
		/// <param name="bytes">フレーム情報バイト列.</param>
		/// <param name="format">圧縮フォーマット.</param>
		public Frame (byte[] bytes, ICompressFormat format)
		{
			this.bytes = bytes;
			Size = bytes.Length;
			Format = format;
		}

		/// <summary>
		/// 圧縮されたフレーム情報を解凍し,深度情報のリストを取得する.
		/// </summary>
		/// <returns>深度情報</returns>
		/// <param name="prevBytes">前フレームの深度情報リスト.</param>
		public UnitDepth GetDepthList()
		{
			return Format.Decompress(bytes);
		}
	}
}

