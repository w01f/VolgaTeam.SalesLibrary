using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace SalesDepot.ConfigurationClasses
{
	public class QBuilderSettings
	{
		private string _localSettingsPath;

		public string Host { get; set; }
		public string User { get; set; }
		public string Password { get; set; }
		public bool SavePassword { get; set; }

		public List<string> AvailableHosts { get; private set; }

		public QBuilderSettings()
		{
			AvailableHosts = new List<string>();
			LoadLocalSettings();
			LoadAvailableHosts();
		}

		private void LoadLocalSettings()
		{
			_localSettingsPath = string.Format(@"{0}\newlocaldirect.com\xml\sales depot\Settings\QBuilder.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			if (!File.Exists(_localSettingsPath)) return;
			var document = new XmlDocument();
			document.Load(_localSettingsPath);

			var node = document.SelectSingleNode(@"/LocalSettings/Host");
			if (node != null)
				Host = node.InnerText;
			node = document.SelectSingleNode(@"/LocalSettings/User");
			if (node != null)
				User = node.InnerText;
			node = document.SelectSingleNode(@"/LocalSettings/Password");
			if (node != null)
				Password = node.InnerText;
			node = document.SelectSingleNode(@"/LocalSettings/SavePassword");
			bool tempBool;
			if (node != null && Boolean.TryParse(node.InnerText, out tempBool))
				SavePassword = tempBool;
		}

		public void SaveLocalSettings()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<LocalSettings>");
			xml.AppendLine(@"<Host>" + Host.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Host>");
			xml.AppendLine(@"<User>" + User.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</User>");
			xml.AppendLine(@"<Password>" + Password.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Password>");
			xml.AppendLine(@"<SavePassword>" + SavePassword.ToString() + @"</SavePassword>");
			xml.AppendLine(@"</LocalSettings>");

			using (var sw = new StreamWriter(_localSettingsPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}

		private void LoadAvailableHosts()
		{
			AvailableHosts.Clear();
			var path = string.Format(@"{0}\newlocaldirect.com\Sales Depot\Sites.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			if (!File.Exists(path)) return;
			var document = new XmlDocument();
			document.Load(path);

			var node = document.SelectSingleNode(@"/Sites");
			if (node != null)
				foreach (XmlNode childNode in node.ChildNodes)
					AvailableHosts.Add(childNode.InnerText);
		}
	}
}
