using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SportsTimeMachinePlayer.Model{
	public class Fps
	{
		private int count;
		private float time;
		public int Value{get;private set;}
		
		public Fps ()
		{
		}

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
