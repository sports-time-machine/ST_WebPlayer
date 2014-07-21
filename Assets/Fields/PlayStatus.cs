using System;

namespace SportsTimeMachinePlayer.Fields
{
	public class PlayStatus
	{
		public event Action FrameCountChanged = delegate {};

		public bool IsPlaying {get; set;}
		public bool IsEnd{get; set;}
		public int FrameCount{
			get { return frameCount;}
			set 
			{
				if (FrameCount != value){
					if (value < 0) frameCount = 0;
					else if(value > MaxFrameCount -1) frameCount = MaxFrameCount - 1;
					else frameCount = value;
					FrameCountChanged();
				}
			}
		}
		public int MaxFrameCount{get; set;}
		public int MaxPlayTime{get; set;}

		public int Fps{get;set;}

		private int frameCount;
		public PlayStatus ()
		{
			IsPlaying = false;
			IsEnd  = false;
			frameCount = 0;
			MaxFrameCount = 0;
			MaxPlayTime = 0;
			Fps = 0;
		}
	}
}

