using System.IO;
using System.Xml;

namespace SalesDepot.Services.ContentManagmentService
{
	public partial class LibraryConfig
	{
		public void LoadData(string sourceFile)
		{
			if (!File.Exists(sourceFile)) return;
			var document = new XmlDocument();
			document.Load(sourceFile);

			var node = document.SelectSingleNode(@"/erroremail/sender");
			if (node != null)
				deadLinkSender = node.InnerText;
			node = document.SelectSingleNode(@"/erroremail/recipients");
			if (node != null)
				deadLinkRecipients = node.InnerText;
			node = document.SelectSingleNode(@"/erroremail/subject");
			if (node != null)
				deadLinkSubject = node.InnerText;
			node = document.SelectSingleNode(@"/erroremail/body");
			if (node != null)
				deadLinkBody = node.InnerText.Trim();
		}
	}
}