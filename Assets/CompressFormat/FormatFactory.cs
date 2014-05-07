using System;

namespace SportsTimeMachinePlayer.CompressFormat
{
	static public class FormatFactory
	{
		static public ICompressFormat GetFormat (string formatString)
		{
			ICompressFormat format = null;
			switch (formatString){
				case "depth 2d 10b/6b ":
					format = new Format2D10BD6BL();
				break;
			}
			return format;
		}
	}
}

