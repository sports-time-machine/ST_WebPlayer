using System;
using System.Collections.Generic;
using System.IO;
using SportsTimeMachinePlayer.Model;

namespace SportsTimeMachinePlayer.Model{

	/// <summary>
	/// 1Unitの深度情報を表すクラス.
	/// 1Unitにはカメラが2台あるため,2つの深度データリストがある.
	/// </summary>
	public class UnitDepth
	{
		/// <summary>
		/// 深度データリスト1を取得する.
		/// </summary>
		public List<Depth> DepthList1 {get; private set;}

		/// <summary>
		/// 深度データリスト2を取得する.
		/// </summary>
		public List<Depth> DepthList2 {get; private set;}

		/// <summary>
		/// コンストラクタ.
		/// リスト高速化のため,引数にリストの最大数を指定しなければならない.
		/// </summary>
		/// <param name="capacity">Capacity.</param>
		public UnitDepth(int capacity)
		{
			DepthList1 = new List<Depth>(capacity);
			DepthList2 = new List<Depth>(capacity);
		}
	}
}

