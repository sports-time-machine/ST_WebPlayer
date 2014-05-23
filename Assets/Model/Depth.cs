using System;
using System.Collections.Generic;
using System.IO;

namespace SportsTimeMachinePlayer.Model{

	/// <summary>
	/// 深度情報と座標を持つクラス.
	/// </summary>
	public class Depth
	{
		/// <summary>
		/// X座標を取得する.
		/// </summary>
		/// <value>The x.</value>
		public int X{get; private set;}

		/// <summary>
		/// Y座標を取得する.
		/// </summary>
		/// <value>The y.</value>
		public int Y{get; private set;}

		/// <summary>
		/// 深度情報を取得する.
		/// </summary>
		/// <value>The value.</value>
		public int Value{get; private set;}

		/// <summary>
		/// コンストラクタ.
		/// </summary>
		/// <param name="x">X座標.</param>
		/// <param name="y">Y座標.</param>
		/// <param name="value">深度.</param>
		public Depth (int x, int y, int value)
		{
			X = x;
			Y = y;
			Value = value;
		}
	}
}

