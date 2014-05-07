using System;
using System.IO;
using UnityEngine;
using SportsTimeMachinePlayer.Reader;

namespace SportsTimeMachinePlayer.CompressFormat
{
	/// <summary>
	/// フォーマット depth 2d 10b/6b でランレングス圧縮されたデータを表すクラス.
	/// 各ピクセルは2バイトで表現され,ランレングス長6bit,深度値10bitで表される.
	/// </summary>
	public class Format2D10BD6BL : ICompressFormat
	{
		public Format2D10BD6BL ()
		{
		}

		/// <summary>
		/// フォーマットの名称を取得する.
		/// </summary>
		/// <returns>The format name.</returns>
		public String GetFormatName(){
			return "depth 2d 10b/6b";
		}

		/// <summary>
		/// 圧縮された深度情報を解凍する.
		/// </summary>
		/// <param name="bytes">圧縮されたバイト列.</param>
		public int[] Decompress(byte[] bytes)
		{
			int first = bytes[0];
			int second = bytes[1];
			
			int runLength = (second >> 2) + 1;
			int depth = ((first) | ((second&0x03) << 8)) * 2502 >> 8;

			int[] decompressDepth = new int[runLength];

			for (int i = 0; i < runLength; ++i)
			{
				decompressDepth[i] = depth;
			}
			return decompressDepth;
		}

		/// <summary>
		/// 圧縮されたフォーマットのサイズを取得する.
		/// </summary>
		/// <returns>圧縮されたフォーマットのサイズ(byte).</returns>
		public int GetCompressSize()
		{
			return 2;
		}
	}
}

