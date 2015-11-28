using System.Text;
using System.Xml;

namespace SalesLibraries.SalesDepot.Configuration
{
	class EmailBinSettings
	{
		public bool ShowEmailBin { get; set; }
		public bool EmailBinSendAsPdf { get; set; }
		public bool EmailBinSendAsZip { get; set; }

		public EmailBinSettings()
		{
			ShowEmailBin = false;
			EmailBinSendAsPdf = false;
			EmailBinSendAsZip = false;
		}

		public string Serialize()
		{
			var xml = new StringBuilder();
			xml.AppendLine(@"<ShowEmailBin>" + ShowEmailBin + @"</ShowEmailBin>");
			xml.AppendLine(@"<EmailBinSendAsPdf>" + EmailBinSendAsPdf + @"</EmailBinSendAsPdf>");
			xml.AppendLine(@"<EmailBinSendAsZip>" + EmailBinSendAsZip + @"</EmailBinSendAsZip>");
			return xml.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				bool tempBool;
				switch (childNode.Name)
				{
					case "ShowEmailBin":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowEmailBin = tempBool;
						break;
					case "EmailBinSendAsZip":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EmailBinSendAsZip = tempBool;
						break;
					case "EmailBinSendAsPdf":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EmailBinSendAsPdf = tempBool;
						break;
				}
			}
		}

		public void LoadDefault()
		{
			if (RemoteResourceManager.Instance.DefaultViewFile.ExistsLocal())
			{
				var document = new XmlDocument();
				try
				{
					document.Load(RemoteResourceManager.Instance.DefaultViewFile.LocalPath);
				}
				catch { }

				bool tempBool;
				var node = document.SelectSingleNode(@"/defaultview/SalesLibrary/emailbinpdf");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						EmailBinSendAsPdf = tempBool;
			}
		}
	}
}
