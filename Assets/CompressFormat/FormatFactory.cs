using System;

namespace SportsTimeMachinePlayer.CompressFormat
{

	/// <summary>
	/// フォーマットを表す文字列から圧縮フォーマットクラスを取得可能な静的クラス.
	/// </summary>
	static public class FormatFactory
	{

		/// <summary>
		/// フォーマットを表す文字列から圧縮フォーマットクラスを取得する
		/// </summary>
		/// <returns>圧縮フォーマット.</returns>
		/// <param name="formatString">フォーマットを表す文字列.</param>
		static public ICompressFormat GetFormat (string formatString)
		{
			ICompressFormat format = null;
			switch (formatString){
			case "depth 2d 10b/6b " :
				format = new Format2D10BD6BL();
				break;
			default :
				throw new ArgumentException("フォーマットを表す文字列が正しくありません.");
			}
			return format;
		}
	}
}

