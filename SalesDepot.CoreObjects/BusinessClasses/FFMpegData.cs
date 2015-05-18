using System;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class FFMpegData
	{
		public string Codec { get; private set; }
		public int Bitrate { get; private set; }
		public double Duration { get; private set; }

		public bool IsH264Encoded
		{
			get { return String.Equals(Codec, "h264", StringComparison.OrdinalIgnoreCase); }
		}

		public FFMpegData(dynamic source)
		{
			var codecValue = source.streams[0].codec_name;
			if (codecValue != null)
				Codec = codecValue.ToString();

			var bitrateValue = source.streams[0].bit_rate;
			if (bitrateValue != null)
				Bitrate = Int32.Parse(bitrateValue.ToString());

			var durationValue = source.streams[0].duration;
			if (durationValue != null)
				Duration = Math.Floor(Double.Parse(durationValue.ToString(), new System.Globalization.CultureInfo("en-us")));
		}
	}
}
