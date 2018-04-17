using System;
using System.IO;
using System.Text;
using System.Xml;

namespace SalesLibraries.SiteManager.ConfigurationClasses
{
	public class UsersEmailSettings
	{
		private readonly string _filePath;

		public bool SendLocalEmail { get; set; }
		public string LocalEmailAccountName { get; set; }
		public string LocalEmailCopyAddresses { get; set; }

		public string NewAccountSubject { get; set; }
		public string NewAccountBodyPlaceholder1 { get; set; }
		public string NewAccountBodyPlaceholder2 { get; set; }
		public string NewAccountBodyPlaceholder3 { get; set; }
		public string NewAccountBodyPlaceholder4 { get; set; }
		public string NewAccountBodyPlaceholder5 { get; set; }
		public string NewAccountBodyPlaceholder6 { get; set; }
		public string NewAccountBodyPlaceholder7 { get; set; }
		public string NewAccountBodyPlaceholder8 { get; set; }

		public string ResetAccountSubject { get; set; }
		public string ResetAccountBodyPlaceholder1 { get; set; }
		public string ResetAccountBodyPlaceholder2 { get; set; }
		public string ResetAccountBodyPlaceholder3 { get; set; }
		public string ResetAccountBodyPlaceholder4 { get; set; }
		public string ResetAccountBodyPlaceholder5 { get; set; }
		public string ResetAccountBodyPlaceholder6 { get; set; }
		public string ResetAccountBodyPlaceholder7 { get; set; }
		public string ResetAccountBodyPlaceholder8 { get; set; }

		public UsersEmailSettings()
		{
			_filePath = Path.Combine(Path.GetDirectoryName(typeof(SettingsManager).Assembly.Location), "UsersEmailSettings.xml");

			NewAccountSubject = "Your NEW RAYCOM RESULTS ACCOUNT is READY!";
			NewAccountBodyPlaceholder1 = "You have a NEW account at RaycomResults.tv";
			NewAccountBodyPlaceholder2 = "1. Account Username:";
			NewAccountBodyPlaceholder3 = "2. Temporary Password:";
			NewAccountBodyPlaceholder4 = "3. Open Google Chrome";
			NewAccountBodyPlaceholder5 = "4. Click the link below, or paste it into Google Chrome";
			NewAccountBodyPlaceholder6 = "5. When you log in, create your NEW PASSWORD  (Facebook or LinkedIn passwords work best)";
			NewAccountBodyPlaceholder7 = "6. If you have connection issues, email billy@adSALESapps.com or llambert@raycommedia.com";
			NewAccountBodyPlaceholder8 = "Happy Selling!";

			ResetAccountSubject = "Your RAYCOM RESULTS ACCOUNT is RESET";
			ResetAccountBodyPlaceholder1 = "Your Raycom Results account is RESET";
			ResetAccountBodyPlaceholder2 = "1. Account Username:";
			ResetAccountBodyPlaceholder3 = "2. Temporary Password:";
			ResetAccountBodyPlaceholder4 = "3. Open Google Chrome";
			ResetAccountBodyPlaceholder5 = "4. Click the link below, or paste it into Google Chrome";
			ResetAccountBodyPlaceholder6 = "5. When you log in, create your NEW PASSWORD  (Facebook or LinkedIn passwords work best)";
			ResetAccountBodyPlaceholder7 = "6. If you have connection issues, email billy@adSALESapps.com or llambert@raycommedia.com";
			ResetAccountBodyPlaceholder8 = "Happy Selling!";
		}

		public void Load()
		{
			if (!File.Exists(_filePath)) return;
			var document = new XmlDocument();
			document.Load(_filePath);

			var node = document.SelectSingleNode(@"//Settings/SendLocalEmail");
			if (Boolean.TryParse(node?.InnerText?.Trim() ?? "false", out var temp))
				SendLocalEmail = temp;

			LocalEmailAccountName = document.SelectSingleNode(@"//Settings/LocalEmailAccountName")?.InnerText;
			LocalEmailCopyAddresses = document.SelectSingleNode(@"//Settings/LocalEmailCopyAddresses")?.InnerText;

			NewAccountSubject = document.SelectSingleNode(@"//Settings/NewAccount/Subject")?.InnerText ?? NewAccountSubject;
			NewAccountBodyPlaceholder1 = document.SelectSingleNode(@"//Settings/NewAccount/Placeholder1")?.InnerText ?? NewAccountBodyPlaceholder1;
			NewAccountBodyPlaceholder2 = document.SelectSingleNode(@"//Settings/NewAccount/Placeholder2")?.InnerText ?? NewAccountBodyPlaceholder2;
			NewAccountBodyPlaceholder3 = document.SelectSingleNode(@"//Settings/NewAccount/Placeholder3")?.InnerText ?? NewAccountBodyPlaceholder3;
			NewAccountBodyPlaceholder4 = document.SelectSingleNode(@"//Settings/NewAccount/Placeholder4")?.InnerText ?? NewAccountBodyPlaceholder4;
			NewAccountBodyPlaceholder5 = document.SelectSingleNode(@"//Settings/NewAccount/Placeholder5")?.InnerText ?? NewAccountBodyPlaceholder5;
			NewAccountBodyPlaceholder6 = document.SelectSingleNode(@"//Settings/NewAccount/Placeholder6")?.InnerText ?? NewAccountBodyPlaceholder6;
			NewAccountBodyPlaceholder7 = document.SelectSingleNode(@"//Settings/NewAccount/Placeholder7")?.InnerText ?? NewAccountBodyPlaceholder7;
			NewAccountBodyPlaceholder8 = document.SelectSingleNode(@"//Settings/NewAccount/Placeholder8")?.InnerText ?? NewAccountBodyPlaceholder8;

			ResetAccountSubject = document.SelectSingleNode(@"//Settings/ResetAccount/Subject")?.InnerText ?? ResetAccountSubject;
			ResetAccountBodyPlaceholder1 = document.SelectSingleNode(@"//Settings/ResetAccount/Placeholder1")?.InnerText ?? ResetAccountBodyPlaceholder1;
			ResetAccountBodyPlaceholder2 = document.SelectSingleNode(@"//Settings/ResetAccount/Placeholder2")?.InnerText ?? ResetAccountBodyPlaceholder2;
			ResetAccountBodyPlaceholder3 = document.SelectSingleNode(@"//Settings/ResetAccount/Placeholder3")?.InnerText ?? ResetAccountBodyPlaceholder3;
			ResetAccountBodyPlaceholder4 = document.SelectSingleNode(@"//Settings/ResetAccount/Placeholder4")?.InnerText ?? ResetAccountBodyPlaceholder4;
			ResetAccountBodyPlaceholder5 = document.SelectSingleNode(@"//Settings/ResetAccount/Placeholder5")?.InnerText ?? ResetAccountBodyPlaceholder5;
			ResetAccountBodyPlaceholder6 = document.SelectSingleNode(@"//Settings/ResetAccount/Placeholder6")?.InnerText ?? ResetAccountBodyPlaceholder6;
			ResetAccountBodyPlaceholder7 = document.SelectSingleNode(@"//Settings/ResetAccount/Placeholder7")?.InnerText ?? ResetAccountBodyPlaceholder7;
			ResetAccountBodyPlaceholder8 = document.SelectSingleNode(@"//Settings/ResetAccount/Placeholder8")?.InnerText ?? ResetAccountBodyPlaceholder8;
		}

		public void Save()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<Settings>");
			xml.AppendLine(@"<SendLocalEmail>" + SendLocalEmail + @"</SendLocalEmail>");
			xml.AppendLine(@"<LocalEmailAccountName>" + LocalEmailAccountName?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;") + @"</LocalEmailAccountName>");
			xml.AppendLine(@"<LocalEmailCopyAddresses>" + LocalEmailCopyAddresses?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;") + @"</LocalEmailCopyAddresses>");

			xml.AppendLine(@"<NewAccount>");
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Subject", NewAccountSubject?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Placeholder1", NewAccountBodyPlaceholder1?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Placeholder2", NewAccountBodyPlaceholder2?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Placeholder3", NewAccountBodyPlaceholder3?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Placeholder4", NewAccountBodyPlaceholder4?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Placeholder5", NewAccountBodyPlaceholder5?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Placeholder6", NewAccountBodyPlaceholder6?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Placeholder7", NewAccountBodyPlaceholder7?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Placeholder8", NewAccountBodyPlaceholder8?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(@"</NewAccount>");

			xml.AppendLine(@"<ResetAccount>");
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Subject", ResetAccountSubject?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Placeholder1", ResetAccountBodyPlaceholder1?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Placeholder2", ResetAccountBodyPlaceholder2?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Placeholder3", ResetAccountBodyPlaceholder3?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Placeholder4", ResetAccountBodyPlaceholder4?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Placeholder5", ResetAccountBodyPlaceholder5?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Placeholder6", ResetAccountBodyPlaceholder6?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Placeholder7", ResetAccountBodyPlaceholder7?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Placeholder8", ResetAccountBodyPlaceholder8?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(@"</ResetAccount>");

			xml.AppendLine(@"</Settings>");

			using (var sw = new StreamWriter(_filePath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}
	}
}