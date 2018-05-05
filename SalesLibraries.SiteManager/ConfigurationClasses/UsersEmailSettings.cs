using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace SalesLibraries.SiteManager.ConfigurationClasses
{
	public class UsersEmailSettings
	{
		private static string _filePath = Path.Combine(Path.GetDirectoryName(typeof(SettingsManager).Assembly.Location), "UsersEmailSettings.xml");

		public string SiteUrl { get; set; }
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

		public string DeleteAccountRecipients { get; set; }
		public string DeleteAccountSubject { get; set; }
		public string DeleteAccountBodyPlaceholder1 { get; set; }
		public string DeleteAccountBodyPlaceholder2 { get; set; }
		public string DeleteAccountBodyPlaceholder3 { get; set; }

		public UsersEmailSettings()
		{
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

			DeleteAccountSubject = "User Account Termination Notice";
			DeleteAccountBodyPlaceholder1 = "Account Closed:";
			DeleteAccountBodyPlaceholder2 = "Account Name:";
			DeleteAccountBodyPlaceholder3 = "User Name:";
		}

		public static IList<UsersEmailSettings> LoadFromXml()
		{
			var items = new List<UsersEmailSettings>();

			if (File.Exists(_filePath))
			{
				var document = new XmlDocument();
				document.Load(_filePath);

				var configNodes = (document.SelectNodes("//Settings/Item").OfType<XmlNode>() ?? new XmlNode[] { }).ToList();
				foreach (var configNode in configNodes)
				{
					var item = new UsersEmailSettings();
					item.Load(configNode);
					items.Add(item);
				}

			}
			return items;
		}

		private void Load(XmlNode configNode)
		{
			SiteUrl = configNode.SelectSingleNode(@"./SiteUrl")?.InnerText;

			if (Boolean.TryParse(configNode.SelectSingleNode(@"./SendLocalEmail")?.InnerText?.Trim() ?? "false", out var temp))
				SendLocalEmail = temp;

			LocalEmailAccountName = configNode.SelectSingleNode(@"./LocalEmailAccountName")?.InnerText;
			LocalEmailCopyAddresses = configNode.SelectSingleNode(@"./LocalEmailCopyAddresses")?.InnerText;

			NewAccountSubject = configNode.SelectSingleNode(@"./NewAccount/Subject")?.InnerText ?? NewAccountSubject;
			NewAccountBodyPlaceholder1 = configNode.SelectSingleNode(@"./NewAccount/Placeholder1")?.InnerText ?? NewAccountBodyPlaceholder1;
			NewAccountBodyPlaceholder2 = configNode.SelectSingleNode(@"./NewAccount/Placeholder2")?.InnerText ?? NewAccountBodyPlaceholder2;
			NewAccountBodyPlaceholder3 = configNode.SelectSingleNode(@"./NewAccount/Placeholder3")?.InnerText ?? NewAccountBodyPlaceholder3;
			NewAccountBodyPlaceholder4 = configNode.SelectSingleNode(@"./NewAccount/Placeholder4")?.InnerText ?? NewAccountBodyPlaceholder4;
			NewAccountBodyPlaceholder5 = configNode.SelectSingleNode(@"./NewAccount/Placeholder5")?.InnerText ?? NewAccountBodyPlaceholder5;
			NewAccountBodyPlaceholder6 = configNode.SelectSingleNode(@"./NewAccount/Placeholder6")?.InnerText ?? NewAccountBodyPlaceholder6;
			NewAccountBodyPlaceholder7 = configNode.SelectSingleNode(@"./NewAccount/Placeholder7")?.InnerText ?? NewAccountBodyPlaceholder7;
			NewAccountBodyPlaceholder8 = configNode.SelectSingleNode(@"./NewAccount/Placeholder8")?.InnerText ?? NewAccountBodyPlaceholder8;

			ResetAccountSubject = configNode.SelectSingleNode(@"./ResetAccount/Subject")?.InnerText ?? ResetAccountSubject;
			ResetAccountBodyPlaceholder1 = configNode.SelectSingleNode(@"./ResetAccount/Placeholder1")?.InnerText ?? ResetAccountBodyPlaceholder1;
			ResetAccountBodyPlaceholder2 = configNode.SelectSingleNode(@"./ResetAccount/Placeholder2")?.InnerText ?? ResetAccountBodyPlaceholder2;
			ResetAccountBodyPlaceholder3 = configNode.SelectSingleNode(@"./ResetAccount/Placeholder3")?.InnerText ?? ResetAccountBodyPlaceholder3;
			ResetAccountBodyPlaceholder4 = configNode.SelectSingleNode(@"./ResetAccount/Placeholder4")?.InnerText ?? ResetAccountBodyPlaceholder4;
			ResetAccountBodyPlaceholder5 = configNode.SelectSingleNode(@"./ResetAccount/Placeholder5")?.InnerText ?? ResetAccountBodyPlaceholder5;
			ResetAccountBodyPlaceholder6 = configNode.SelectSingleNode(@"./ResetAccount/Placeholder6")?.InnerText ?? ResetAccountBodyPlaceholder6;
			ResetAccountBodyPlaceholder7 = configNode.SelectSingleNode(@"./ResetAccount/Placeholder7")?.InnerText ?? ResetAccountBodyPlaceholder7;
			ResetAccountBodyPlaceholder8 = configNode.SelectSingleNode(@"./ResetAccount/Placeholder8")?.InnerText ?? ResetAccountBodyPlaceholder8;

			DeleteAccountRecipients = configNode.SelectSingleNode(@"./DeleteAccount/Recipients")?.InnerText;
			DeleteAccountSubject = configNode.SelectSingleNode(@"./DeleteAccount/Subject")?.InnerText ?? DeleteAccountSubject;
			DeleteAccountBodyPlaceholder1 = configNode.SelectSingleNode(@"./DeleteAccount/Placeholder1")?.InnerText ?? DeleteAccountBodyPlaceholder1;
			DeleteAccountBodyPlaceholder2 = configNode.SelectSingleNode(@"./DeleteAccount/Placeholder2")?.InnerText ?? DeleteAccountBodyPlaceholder2;
			DeleteAccountBodyPlaceholder3 = configNode.SelectSingleNode(@"./DeleteAccount/Placeholder3")?.InnerText ?? DeleteAccountBodyPlaceholder3;
		}

		public static void SaveToFile(IList<UsersEmailSettings> items)
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

		private string Serialize()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<SiteUrl>" + SiteUrl?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;") + @"</SiteUrl>");
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

			xml.AppendLine(@"<DeleteAccount>");
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Recipients", DeleteAccountRecipients?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Subject", DeleteAccountSubject?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Placeholder1", DeleteAccountBodyPlaceholder1?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Placeholder2", DeleteAccountBodyPlaceholder2?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(String.Format("<{0}>{1}</{0}>", "Placeholder3", DeleteAccountBodyPlaceholder3?.Replace(@"&", "&#38;")?.Replace("\"", "&quot;")));
			xml.AppendLine(@"</DeleteAccount>");

			return xml.ToString();
		}
	}
}