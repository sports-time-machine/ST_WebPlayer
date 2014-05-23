
namespace SportsTimeMachinePlayer.Unit
{
	public class PlayStatus
	{
		public bool IsPlayable{ get;set;}
		public bool IsPlaying{get; set;}
		public bool IsEnd{get; set;}
		public int FrameCount{get; set;}
		public int MaxFrameCount{get; set;}
		public double PlayTime{get; set;}

		public PlayStatus ()
		{
			IsPlayable = false;
			IsPlaying = false;
			IsEnd  = false;
			FrameCount = 0;
			MaxFrameCount = 0;
			PlayTime = 0.0;
		}
	}
}

