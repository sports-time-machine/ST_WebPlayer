using System;
using System.Collections.Generic;
using UnityEngine;

namespace SportsTimeMachinePlayer.VoxcelFilter
{
	/// <summary>
	/// 三次元のボクセルデータにフィルタをかける.
	/// フィルタを作成する際はこのインターフェースを実装すること.
	/// </summary>
	public interface IVoxcelFilter
	{
		/// <summary>
		/// ボクセルのリストを取得する.
		/// </summary>
		/// <returns>The voxcel list.</returns>
		List<Vector3> VoxcelList{get;}
	}
}