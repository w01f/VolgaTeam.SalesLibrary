using System;
using System.IO;
using Newtonsoft.Json;

namespace SalesLibraries.Common.Objects.Video
{
	public class FFMpegData
	{
		public string Codec { get; private set; }
		public int Bitrate { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }
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

			var widthValue = source.streams[0].width;
			if (widthValue != null)
				Width = Int32.Parse(widthValue.ToString());

			var heightValue = source.streams[0].height;
			if (heightValue != null)
				Height = Int32.Parse(heightValue.ToString());

			var durationValue = source.streams[0].duration;
			if (durationValue != null)
				Duration = Math.Floor(Double.Parse(durationValue.ToString(), new System.Globalization.CultureInfo("en-us")));
		}

		public static FFMpegData LoadFromFile(string infoFilePath)
		{
			return new FFMpegData(JsonConvert.DeserializeObject(File.ReadAllText(infoFilePath)));
		}
	}
}
