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
		/// <returns>フォーマット名を表す文字列.</returns>
		String GetName();
	
		/// <summary>
		/// 圧縮されたフレーム情報を解凍する.
		/// </summary>
		/// <param name="bytes">フレーム情報バイト列.</param>
		/// <returns>ユニット深度情報.</returns>
		UnitDepth Decompress(byte[] bytes);

	}
}

