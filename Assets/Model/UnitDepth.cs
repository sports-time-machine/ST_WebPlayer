using System;
using System.Collections.Generic;
using System.IO;
using SportsTimeMachinePlayer.Model;

namespace SportsTimeMachinePlayer.Model{
	public class UnitDepth
	{
		public List<Depth> DepthList1 {get; private set;}
		public List<Depth> DepthList2 {get; private set;}

		public UnitDepth(int capacity)
		{
			DepthList1 = new List<Depth>(capacity);
			DepthList2 = new List<Depth>(capacity);
		}
	}
}

