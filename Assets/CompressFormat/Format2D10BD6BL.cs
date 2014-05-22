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
		private const int WIDTH = 640;
		private const int HEIGHT = 480;
		private const int DOT_COUNT = WIDTH * HEIGHT;

		private int count;

		private int X{get{return count % WIDTH;}}
		private int Y{get{return (int)Math.Floor(count/(double)WIDTH);}}

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
		/// 圧縮されたフレーム情報を解凍する.
		/// </summary>
		/// <param name="bytes">圧縮されたフレーム情報.</param>
		public UnitDepth Decompress(byte[] bytes)
		{

			UnitDepth unitDepth = new UnitDepth(DOT_COUNT);
			List<Depth> depthList = unitDepth.DepthList1;

			int size = bytes.Length;
			count = 0;

			for(int i=0; i < size; i+=2)
			{
				byte[] compressBytes = new byte[2];
				for (int j = 0; j < 2; ++j){
					compressBytes[j] = bytes[i + j];
				}
				
				int first = compressBytes[0];
				int second = compressBytes[1];
				
				int runLength = (second >> 2) + 1;
				int depth = ((first) | ((second&0x03) << 8)) * 2502 >> 8;


				if (depth == 0 || depth > 8.0f * 1000.0f){
					count+=runLength;
				}else{
					for (int j = 0; j < runLength; ++j)
					{
						depthList.Add(new Depth(X,Y,depth));
						count++;
					}
				}

				if (count == DOT_COUNT){
					depthList = unitDepth.DepthList2;
					count=0;
				}
			}

			return unitDepth;
		}

	}
}

