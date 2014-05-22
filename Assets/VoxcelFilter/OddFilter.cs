using System;
using System.Collections.Generic;
using UnityEngine;

namespace SportsTimeMachinePlayer.VoxcelFilter
{
	/// <summary>
	/// 奇数番目のボクセルを削除するフィルタ.
	/// </summary>
	public class OddFilter : IVoxcelFilter
	{
		public List<Vector3> parentVoxcelList;
		public List<Vector3> VoxcelList { get; private set;}

		public OddFilter (List<Vector3> voxcelList)
		{
			parentVoxcelList = voxcelList;
			VoxcelList = new List<Vector3>(parentVoxcelList.Count/2);
			for (int i=0; i < parentVoxcelList.Count; i+=2){
				VoxcelList.Add(parentVoxcelList[i]);
			}
		}
	}
}

