using System;
using System.Collections.Generic;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.Common.Objects.PowerPoint
{
	public class SlideSettings
	{
		public SlideSize SlideSize { get; }

		public SlideFormatEnum Format => SlideFormatParser.GetFormatBySlideSize(SlideSize);

		public string SizeFormatted
		{
			get
			{
				switch (Format)
				{
					case SlideFormatEnum.Format4x3:
						return "4 x 3";
					case SlideFormatEnum.Format3x4:
						return "3 x 4";
					case SlideFormatEnum.Format16x9:
						return "16 x 9";
					default:
						return "4 x 3";
				}
			}
		}

		public string SlideFolder
		{
			get
			{
				switch (Format)
				{
					case SlideFormatEnum.Format4x3:
						return "Slides43";
					case SlideFormatEnum.Format3x4:
						return "Slides34";
					case SlideFormatEnum.Format16x9:
						return "Slides169";
					default:
						return "Slides43";
				}
			}
		}

		public string SlideMasterFolder => SizeFormatted.Replace(" ", "");

		public string LauncherTemplateName => String.Format("adSALESapps{0}.potx", SlideFolder.Replace("Slides", ""));

		public SlideSettings()
		{
			SlideSize = new SlideSize
			{
				Width = 10,
				Height = 7.5m
			};
		}

		public bool IsEqual(SlideSettings target)
		{
			return target.SlideSize.Width == SlideSize.Width &&
				target.SlideSize.Height == SlideSize.Height;
		}

		public static SlideSettings ReadFromString(string size)
		{
			var slideSettings = new SlideSettings();
			switch (size)
			{
				case "4x3":
					slideSettings.SlideSize.Width = 10;
					slideSettings.SlideSize.Height = 7.5m;
					break;
				case "3x4":
					slideSettings.SlideSize.Width = 7.5m;
					slideSettings.SlideSize.Height = 10;
					break;
				case "16x9":
					slideSettings.SlideSize.Width = 13.333m;
					slideSettings.SlideSize.Height = 7.5m;
					break;
				default:
					throw new ArgumentOutOfRangeException("Can't parse slide configuration");
			}
			return slideSettings;
		}

		public static IEnumerable<SlideSettings> GetAvailableConfigurations()
		{
			return new[]
			{
				ReadFromString("4x3"),
				ReadFromString("16x9"),
				ReadFromString("3x4"),
			};
		}
	}
}
