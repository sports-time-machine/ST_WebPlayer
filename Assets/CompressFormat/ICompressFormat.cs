using System;
using System.Collections.Generic;
using SportsTimeMachinePlayer.Model;

namespace SportsTimeMachinePlayer.CompressFormat
{
	/// <summary>
	/// 深度情報圧縮フォーマットのインターフェイス.
	/// 深度情報圧縮フォーマットのクラスを作成する際はこれを実装する.
	/// </summary>
	public interface ICompressFormat
	{
		/// <summary>
		/// フォーマットの名称を取得する.
		/// </summary>
		/// <returns>The format name.</returns>
		String GetFormatName();

		/// <summary>
		/// 圧縮されたフレーム情報を解凍する.
		/// </summary>
		/// <param name="bytes">圧縮されたフレーム情報.</param>
		UnitDepth Decompress(byte[] bytes);

	}
}

