using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SportsTimeMachinePlayer.Fields{

	/// <summary>
	/// 画角.
	/// </summary>
	public enum ViewAngle {
		Normal,
		Wide,
		Narrow
	}

	/// <summary>
	/// 再生中のオプションを表すクラス.
	/// </summary>
	public class PlayOption
	{
		/// <summary>
		/// 固定カメラかどうか.
		/// </summary>
		public bool IsFixCamera{
			get { return isFixCamera;}
			set{
				isFixCamera = value;
				if(isFixCamera){
					fixCamera.enabled = true;
					moveCamera.enabled = false;
				}else{
					fixCamera.enabled = false;
					moveCamera.enabled = true;
				}
			}
		}

		private bool isFixCamera;

		/// <summary>
		/// 処理落ちしたときに描画をスキップするかどうか.
		/// </summary>
		public bool IsDrawSkip;

		/// <summary>
		/// FPSを表示させるかどうか.
		/// </summary>
		public bool ShowsFps;

		/// <summary>
		/// 固定カメラの画角.
		/// </summary>
		public ViewAngle ViewAngle{
			get {return viewAngle;}
			set {
				viewAngle = value;
				if (viewAngle == ViewAngle.Normal) moveCamera.fieldOfView = 60.0f;
				else if (viewAngle == ViewAngle.Wide) moveCamera.fieldOfView = 90.0f;
				else if (viewAngle == ViewAngle.Narrow) moveCamera.fieldOfView = 30.0f;
				else moveCamera.fieldOfView = 60.0f;
			}
		}
		private ViewAngle viewAngle;

		/// <summary>
		/// 固定カメラ.
		/// </summary>
		private Camera fixCamera;

		/// <summary>
		/// 移動カメラ.
		/// </summary>
		private Camera moveCamera;

		public PlayOption()
		{
			isFixCamera = true;
			IsDrawSkip = false;
			ShowsFps = false;
			viewAngle = ViewAngle.Normal;
			fixCamera = GameObject.Find("Fix Camera").GetComponent<Camera>();
			moveCamera = GameObject.Find("Move Camera").GetComponent<Camera>();
		}
	
	}
}

