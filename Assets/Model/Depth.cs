using System;
using System.Collections.Generic;
using System.IO;

namespace SportsTimeMachinePlayer.Model{
	public class Depth
	{
		public int X{get; private set;}
		public int Y{get; private set;}
		public int Value{get; private set;}

		public Depth (int x, int y, int value)
		{
			X = x;
			Y = y;
			Value = value;
		}
	}
}

