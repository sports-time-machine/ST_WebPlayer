using System;
using System.Collections.Generic;
using UnityEngine;
using SportsTimeMachinePlayer.Model;

namespace SportsTimeMachinePlayer.VoxcelTransformer
{
	/// <summary>
	/// 深度データを三次元のボクセルデータに変換する.
	/// 深度データを三次元のボクセルデータに変換する際はこれを実装すること.
	/// </summary>
	public interface IVoxcelTransformer
	{
		/// <summary>
		/// フレーム情報からボクセルのリストを作成する.
		/// </summary>
		/// <returns>ボクセルのリスト</returns>
		/// <param name="frame">フレーム.</param>
		List<Vector3> GetVocelList(Frame frame);
	}
}

