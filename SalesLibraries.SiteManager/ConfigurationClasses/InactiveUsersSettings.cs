using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace SalesLibraries.SiteManager.ConfigurationClasses
{
	public class InactiveUsersSettings
	{
		private static string _filePath = Path.Combine(Path.GetDirectoryName(typeof(SettingsManager).Assembly.Location), "InactiveUserEmailSettings.xml");

		public string SiteUrl { get; set; }

		public string ResetEmailSubject { get; set; }
		public string ResetEmailBodyPlaceholder1 { get; set; }
		public string ResetEmailBodyPlaceholder2 { get; set; }
		public string ResetEmailBodyPlaceholder3 { get; set; }
		public string DeleteEmailSubject { get; set; }
		public string DeleteEmailBodyPlaceholder1 { get; set; }
		public string DeleteEmailBodyPlaceholder2 { get; set; }

		public InactiveUsersSettings()
		{
			ResetEmailSubject = "Raycom Results Account Inactivity Notification…";
			ResetEmailBodyPlaceholder1 = "If you want to reset your account, click this link below:";
			ResetEmailBodyPlaceholder2 = "If you have connection issues, email billy@adSALESapps.com";
			ResetEmailBodyPlaceholder3 = "Happy Selling!";
			DeleteEmailSubject = "Raycom Results Account Inactivity Notification…";
			DeleteEmailBodyPlaceholder1 = "If you have any questions, or need the account re-activated, email: billy@adSALESapps.com";
			DeleteEmailBodyPlaceholder2 = "Best regards,";
		}

		public static IList<InactiveUsersSettings> LoadFromXml()
		{
			var items = new List<InactiveUsersSettings>();

			if (File.Exists(_filePath))
			{
				var document = new XmlDocument();
				document.Load(_filePath);

				var configNodes = (document.SelectNodes("//Settings/Item").OfType<XmlNode>() ?? new XmlNode[] { }).ToList();
				foreach (var configNode in configNodes)
				{
					var item = new InactiveUsersSettings();
					item.Load(configNode);
					items.Add(item);
				}

			}
			return items;
		}

		public static void SaveToFile(IList<InactiveUsersSettings> items)
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<Settings>");
			foreach (var item in items)
			{
				xml.AppendLine(@"<Item>");
				xml.AppendLine(item.Serialize());
				xml.AppendLine(@"</Item>");
			}
			xml.AppendLine(@"</Settings>");

			using (var sw = new StreamWriter(_filePath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}

		private void Load(XmlNode configNode)
		{
			SiteUrl = configNode.SelectSingleNode(@"./SiteUrl")?.InnerText;

			ResetEmailSubject = configNode.SelectSingleNode(@"./ResetEmail/Subject")?.InnerText ?? ResetEmailSubject;
			ResetEmailBodyPlaceholder1 = configNode.SelectSingleNode(@"./ResetEmail/Placeholder1")?.InnerText ?? ResetEmailBodyPlaceholder1;
			ResetEmailBodyPlaceholder2 = configNode.SelectSingleNode(@"./ResetEmail/Placeholder2")?.InnerText ?? ResetEmailBodyPlaceholder2;
			ResetEmailBodyPlaceholder3 = configNode.SelectSingleNode(@"./ResetEmail/Placeholder3")?.InnerText ?? ResetEmailBodyPlaceholder3;

			DeleteEmailSubject = configNode.SelectSingleNode(@"./DeleteEmail/Subject")?.InnerText ?? DeleteEmailSubject;
			DeleteEmailBodyPlaceholder1 = configNode.SelectSingleNode(@"./DeleteEmail/Placeholder1")?.InnerText ?? DeleteEmailBodyPlaceholder1;
			DeleteEmailBodyPlaceholder2 = configNode.SelectSingleNode(@"./DeleteEmail/Placeholder2")?.InnerText ?? DeleteEmailBodyPlaceholder2;
		}

		private string Serialize()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<SiteUrl>" + SiteUrl?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;") + @"</SiteUrl>");

			xml.AppendLine(@"<ResetEmail>");
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Subject", ResetEmailSubject?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Placeholder1", ResetEmailBodyPlaceholder1?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Placeholder2", ResetEmailBodyPlaceholder2?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Placeholder3", ResetEmailBodyPlaceholder3?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(@"</ResetEmail>");

			xml.AppendLine(@"<DeleteEmail>");
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Subject", DeleteEmailSubject?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Placeholder1", DeleteEmailBodyPlaceholder1?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Placeholder2", DeleteEmailBodyPlaceholder2?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(@"</DeleteEmail>");

			return xml.ToString();
		}
	}
}