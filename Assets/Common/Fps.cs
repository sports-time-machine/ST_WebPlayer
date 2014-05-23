using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SportsTimeMachinePlayer.Common{

	/// <summary>
	/// FPS測定クラス.
	/// </summary>
	public class Fps
	{
		private int count;
		private float time;

		/// <summary>
		/// FPSを取得する.
		/// </summary>
		public int Value{get;private set;}

		/// <summary>
		/// コンストラクタ.
		/// </summary>
		public Fps ()
		{
		}

		/// <summary>
		/// FPSを測定する.毎フレーム行うこと.
		/// </summary>
		public void Update(){
			time += Time.deltaTime;
			count++;
			
			if (time >= 1.0f)
			{
				Value = count;
				time = 0.0f;
				count = 0;
			}
		}
	}
}
