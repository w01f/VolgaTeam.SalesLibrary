using System;
using System.IO;
using System.Text;
using System.Xml;

namespace SalesDepot.SiteManager.ConfigurationClasses
{
	public class InactiveUsersSettings
	{
		private string _filePath;

		public string ResetEmailSender { get; set; }
		public string ResetEmailSubject { get; set; }
		public string ResetEmailBody { get; set; }
		public string DeleteEmailSender { get; set; }
		public string DeleteEmailSubject { get; set; }
		public string DeleteEmailBody { get; set; }

		public InactiveUsersSettings()
		{
			ResetEmailSender = String.Empty;
			ResetEmailSubject = String.Empty;
			ResetEmailBody = String.Empty;
			DeleteEmailSender = String.Empty;
			DeleteEmailSubject = String.Empty;
			DeleteEmailBody = String.Empty;

			_filePath = Path.Combine(Path.GetDirectoryName(typeof(SettingsManager).Assembly.Location), "InactiveUserEmailSettings.xml");
		}

		public void Load()
		{
			if (!File.Exists(_filePath)) return;
			var document = new XmlDocument();
			document.Load(_filePath);

			#region Local Settings
			var node = document.SelectSingleNode(@"/Settings/ResetEmail/Sender");
			if (node != null)
				ResetEmailSender = node.InnerText.Trim();
			node = document.SelectSingleNode(@"/Settings/ResetEmail/Subject");
			if (node != null)
				ResetEmailSubject = node.InnerText.Trim();
			node = document.SelectSingleNode(@"/Settings/ResetEmail/Body");
			if (node != null)
				ResetEmailBody = node.InnerText.Trim();

			node = document.SelectSingleNode(@"/Settings/DeleteEmail/Sender");
			if (node != null)
				DeleteEmailSender = node.InnerText.Trim();
			node = document.SelectSingleNode(@"/Settings/DeleteEmail/Subject");
			if (node != null)
				DeleteEmailSubject = node.InnerText.Trim();
			node = document.SelectSingleNode(@"/Settings/DeleteEmail/Body");
			if (node != null)
				DeleteEmailBody = node.InnerText.Trim();
			#endregion
		}

		public void Save()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<Settings>");

			xml.AppendLine(@"<ResetEmail>");
			xml.AppendLine(@"<Sender>" + ResetEmailSender.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Sender>");
			xml.AppendLine(@"<Subject>" + ResetEmailSubject.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Subject>");
			xml.AppendLine(@"<Body>" + ResetEmailBody.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Body>");
			xml.AppendLine(@"</ResetEmail>");

			xml.AppendLine(@"<DeleteEmail>");
			xml.AppendLine(@"<Sender>" + DeleteEmailSender.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Sender>");
			xml.AppendLine(@"<Subject>" + DeleteEmailSubject.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Subject>");
			xml.AppendLine(@"<Body>" + DeleteEmailBody.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Body>");
			xml.AppendLine(@"</DeleteEmail>");

			xml.AppendLine(@"</Settings>");

			using (var sw = new StreamWriter(_filePath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}
	}
}
