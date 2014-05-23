using System;
using System.Collections.Generic;
using System.IO;

namespace SportsTimeMachinePlayer.Unit{

	/// <summary>
	/// 再生中のオプションを表すクラス.
	/// </summary>
	public class PlayOption
	{
		public bool IsDoubleFrame{get;set;}

		public int IncreaseFrame{
			get{
				if (IsDoubleFrame) return 2;
				return 1;
			}
		}

		public PlayOption()
		{
			IsDoubleFrame = false;
		}
	}
}

