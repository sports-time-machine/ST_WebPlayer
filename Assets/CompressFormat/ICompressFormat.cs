using System;
using System.Collections.Generic;

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
		/// 圧縮された深度情報を解凍する.
		/// </summary>
		/// <param name="bytes">圧縮されたバイト列.</param>
		int[] Decompress(byte[] bytes);

		/// <summary>
		/// 圧縮されたフォーマットのサイズを取得する.
		/// </summary>
		/// <returns>圧縮されたフォーマットのサイズ(byte).</returns>
		int GetCompressSize();
	}
}

