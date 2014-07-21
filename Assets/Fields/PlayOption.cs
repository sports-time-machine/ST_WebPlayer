using System;
using System.Collections.Generic;
using System.IO;

namespace SportsTimeMachinePlayer.Fields{

	/// <summary>
	/// 再生中のオプションを表すクラス.
	/// </summary>
	public class PlayOption
	{
		/// <summary>
		/// 固定カメラかどうか.
		/// </summary>
		public bool IsFixCamera;

		/// <summary>
		/// 処理落ちしたときに描画をスキップするかどうか.
		/// </summary>
		public bool IsDrawSkip;

		/// <summary>
		/// FPSを表示させるかどうか.
		/// </summary>
		public bool ShowsFps;
	
		public PlayOption()
		{
			IsFixCamera = true;
			IsDrawSkip = false;
			ShowsFps = false;
		}
	}
}

