using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ProgramManager.CoreObjects;

namespace SalesLibraries.SalesDepot.Configuration
{
	public class ProgramScheduleSettings
	{
		public string SelectedStation { get; set; }
		public BrowseType BrowseType { get; set; }
		public bool ShowInfo { get; set; }

		public OutputFont HeaderFont { get; set; }
		public OutputFont FooterFont { get; set; }
		public OutputFont BodyFont { get; set; }
		public bool UsePrimeTimeSpecialFontSize { get; set; }
		public int PrimeTimeSpecialFontSize { get; set; }
		public DateTime WeekPrimeTimeStart { get; set; }
		public DateTime WeekPrimeTimeEnd { get; set; }
		public DateTime SundayPrimeTimeStart { get; set; }
		public DateTime SundayPrimeTimeEnd { get; set; }

		public List<OutputFont> HeaderFonts { get; private set; }
		public List<OutputFont> FooterFonts { get; private set; }
		public List<OutputFont> BodyFonts { get; private set; }

		public ProgramScheduleSettings()
		{
			ShowInfo = true;

			HeaderFont = new OutputFont("Arial", 12, true);
			FooterFont = new OutputFont("Arial", 11);
			BodyFont = new OutputFont("Arial", 10);
			UsePrimeTimeSpecialFontSize = false;
			PrimeTimeSpecialFontSize = 8;
			WeekPrimeTimeStart = new DateTime(1, 1, 1, 20, 0, 0);
			WeekPrimeTimeEnd = new DateTime(1, 1, 1, 23, 0, 0);
			SundayPrimeTimeStart = new DateTime(1, 1, 1, 20, 0, 0);
			SundayPrimeTimeEnd = new DateTime(1, 1, 1, 23, 0, 0);

			HeaderFonts = new List<OutputFont>();
			HeaderFonts.Add(new OutputFont("Arial", 12, true));
			HeaderFonts.Add(new OutputFont("Verdana", 12, true));
			HeaderFonts.Add(new OutputFont("Calibri", 12, true));
			HeaderFonts.Add(new OutputFont("Trebuchet MS", 12, true));

			FooterFonts = new List<OutputFont>();
			FooterFonts.Add(new OutputFont("Arial", 11));
			FooterFonts.Add(new OutputFont("Verdana", 11));
			FooterFonts.Add(new OutputFont("Calibri", 11));
			FooterFonts.Add(new OutputFont("Trebuchet MS", 11));

			BodyFonts = new List<OutputFont>();
			BodyFonts.Add(new OutputFont("Arial", 8));
			BodyFonts.Add(new OutputFont("Arial", 9));
			BodyFonts.Add(new OutputFont("Arial", 10));
			BodyFonts.Add(new OutputFont("Verdana", 8));
			BodyFonts.Add(new OutputFont("Calibri", 8));
			BodyFonts.Add(new OutputFont("Trebuchet MS", 8));
			BodyFonts.Add(new OutputFont("Verdana", 9));
			BodyFonts.Add(new OutputFont("Calibri", 9));
			BodyFonts.Add(new OutputFont("Trebuchet MS", 9));
			BodyFonts.Add(new OutputFont("Verdana", 10));
			BodyFonts.Add(new OutputFont("Calibri", 10));
			BodyFonts.Add(new OutputFont("Trebuchet MS", 10));
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			if (!String.IsNullOrEmpty(SelectedStation))
				result.AppendLine(@"<SelectedStation>" + SelectedStation.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedStation>");
			result.AppendLine(@"<BrowseType>" + BrowseType + @"</BrowseType>");
			result.AppendLine(@"<ShowInfo>" + ShowInfo + @"</ShowInfo>");

			result.AppendLine(@"<HeaderFont>" + HeaderFont.Serialize() + @"</HeaderFont>");
			result.AppendLine(@"<FooterFont>" + FooterFont.Serialize() + @"</FooterFont>");
			result.AppendLine(@"<BodyFont>" + BodyFont.Serialize() + @"</BodyFont>");
			result.AppendLine(@"<UsePrimeTimeSpecialFontSize>" + UsePrimeTimeSpecialFontSize + @"</UsePrimeTimeSpecialFontSize>");
			result.AppendLine(@"<PrimeTimeSpecialFontSize>" + PrimeTimeSpecialFontSize + @"</PrimeTimeSpecialFontSize>");
			result.AppendLine(@"<WeekPrimeTimeStart>" + WeekPrimeTimeStart + @"</WeekPrimeTimeStart>");
			result.AppendLine(@"<WeekPrimeTimeEnd>" + WeekPrimeTimeEnd + @"</WeekPrimeTimeEnd>");
			result.AppendLine(@"<SundayPrimeTimeStart>" + SundayPrimeTimeStart + @"</SundayPrimeTimeStart>");
			result.AppendLine(@"<SundayPrimeTimeEnd>" + SundayPrimeTimeEnd + @"</SundayPrimeTimeEnd>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "SelectedStation":
						SelectedStation = childNode.InnerText;
						break;
					case "BrowseType":
						{
							BrowseType temp;
							if (Enum.TryParse(childNode.InnerText, out temp))
								BrowseType = temp;
						}
						break;
					case "ShowInfo":
						{
							bool temp;
							if (bool.TryParse(childNode.InnerText, out temp))
								ShowInfo = temp;
						}
						break;
					case "HeaderFont":
						this.HeaderFont.Deserialize(childNode);
						break;
					case "FooterFont":
						this.FooterFont.Deserialize(childNode);
						break;
					case "BodyFont":
						this.BodyFont.Deserialize(childNode);
						break;
					case "UsePrimeTimeSpecialFontSize":
						{
							bool temp;
							if (bool.TryParse(childNode.InnerText, out temp))
								UsePrimeTimeSpecialFontSize = temp;
						}
						break;
					case "PrimeTimeSpecialFontSize":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								PrimeTimeSpecialFontSize = temp;
						}
						break;
					case "WeekPrimeTimeStart":
						{
							DateTime temp;
							if (DateTime.TryParse(childNode.InnerText, out temp))
								WeekPrimeTimeStart = temp;
						}
						break;
					case "WeekPrimeTimeEnd":
						{
							DateTime temp;
							if (DateTime.TryParse(childNode.InnerText, out temp))
								WeekPrimeTimeEnd = temp;
						}
						break;
					case "SundayPrimeTimeStart":
						{
							DateTime temp;
							if (DateTime.TryParse(childNode.InnerText, out temp))
								SundayPrimeTimeStart = temp;
						}
						break;
					case "SundayPrimeTimeEnd":
						{
							DateTime temp;
							if (DateTime.TryParse(childNode.InnerText, out temp))
								SundayPrimeTimeEnd = temp;
						}
						break;
				}
			}
		}
	}
}