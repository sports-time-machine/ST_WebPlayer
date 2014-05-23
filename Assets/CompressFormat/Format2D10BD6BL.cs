using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using SportsTimeMachinePlayer.Reader;
using SportsTimeMachinePlayer.Model;

namespace SportsTimeMachinePlayer.CompressFormat
{
	/// <summary>
	/// フォーマット depth 2d 10b/6b でランレングス圧縮されたデータを表すクラス.
	/// 各ピクセルは2バイトで表現され,ランレングス長6bit,深度値10bitで表される.
	/// </summary>
	public class Format2D10BD6BL : ICompressFormat
	{
		/// <summary>
		/// スクリーン幅.
		/// </summary>
		private const int WIDTH = 640;

		/// <summary>
		/// スクリーン高さ.
		/// </summary>
		private const int HEIGHT = 480;

		/// <summary>
		/// 圧縮されたデータのバイト数.
		/// </summary>
		private const int BYTE_SIZE = 2;

		/// <summary>
		/// コンストラクタ.
		/// </summary>
		public Format2D10BD6BL ()
		{
		}

		/// <summary>
		/// フォーマットの名称を取得する.
		/// </summary>
		/// <returns>The format name.</returns>
		public String GetName(){
			return "depth 2d 10b/6b";
		}

		/// <summary>
		/// 圧縮されたフレーム情報を解凍する.
		/// </summary>
		/// <remarks>
		/// フレーム情報は,1Unit分の情報,つまり,カメラ2台分のスクリーンを持っている.
		/// 解凍後の深度情報はは640*480のスクリーンを
		/// 左上から右下に走査するように記録されている.
		/// 現在の読んでいる位置をcountとすると,
		/// X座標はcount % WIDTH
		/// Y座標は(int)Math.Floor(count/(double)WIDTH)
		/// で表すことができる.
		/// </remarks>
		/// <param name="bytes">圧縮されたフレーム情報.</param>
		public UnitDepth Decompress(byte[] bytes)
		{
			UnitDepth unitDepth = new UnitDepth(WIDTH * HEIGHT);
			List<Depth> depthList = unitDepth.DepthList1;

			int size = bytes.Length;
			int count = 0;

			for(int i=0; i < size; i+=BYTE_SIZE)
			{
				byte[] compressBytes = new byte[BYTE_SIZE];
				for (int j = 0; j < BYTE_SIZE; ++j){
					compressBytes[j] = bytes[i + j];
				}
				
				int first = compressBytes[0];
				int second = compressBytes[1];
				
				int runLength = (second >> 2) + 1;
				int depth = ((first) | ((second&0x03) << 8)) * 2502 >> 8;


				if (depth == 0 || depth > 8.0f * 1000.0f){
					// 深度が0もしくは8000よりも大きかったら深度情報に追加しない.
					// ラン分読み飛ばす.
					count+=runLength;
				}else{
					for (int j = 0; j < runLength; ++j)
					{
						// 深度情報を追加.
						depthList.Add(new Depth(GetX(count),GetY (count),depth));
						count++;
					}
				}

				// 1つ目のスクリーンの走査終了.2つ目のスクリーンの走査を始める.
				if (count == WIDTH * HEIGHT){
					depthList = unitDepth.DepthList2;
					count=0;
				}
			}

			return unitDepth;
		}

		/// <summary>
		/// 走査位置からX座標を取得する.
		/// </summary>
		/// <returns>X座標</returns>
		/// <param name="count">走査位置</param>
		private int GetX(int count){
			return count % WIDTH;
		}

		/// <summary>
		/// 走査位置からY座標を取得する.
		/// </summary>
		/// <returns>Y座標</returns>
		/// <param name="count">走査位置</param>
		private int GetY(int count){
			return (int)Math.Floor(count/(double)WIDTH);
		}
	}

}

