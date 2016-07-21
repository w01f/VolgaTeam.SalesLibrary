using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.SalesDepot.Configuration;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.Business.Services
{
	class EmailBinManager
	{
		public List<LibraryFileLink> EmailLinks { get; private set; }

		public event EventHandler<EventArgs> ListChanged;

		public EmailBinManager()
		{
			EmailLinks = new List<LibraryFileLink>();
		}

		public void Load()
		{
			EmailLinks.Clear();
			if (!RemoteResourceManager.Instance.EmailBinFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(RemoteResourceManager.Instance.EmailBinFile.LocalPath);
			foreach (var xmlNode in document.SelectNodes(@"/EmailBin/LinkId").OfType<XmlNode>())
			{
				Guid linkId;
				if (!Guid.TryParse(xmlNode.InnerText, out linkId)) continue;
				var link = MainController.Instance.Wallbin.Libraries
					.SelectMany(libraryContext => libraryContext.Libraries)
					.SelectMany(library => library.GetLinkById<LibraryFileLink>(linkId))
					.FirstOrDefault();
				if (link == null) continue;
				if (link.CheckIfDead()) continue;
				EmailLinks.Add(link);
			}
		}

		public void Save()
		{
			var xml = new StringBuilder();
			xml.AppendLine("<EmailBin>");
			foreach (var item in EmailLinks)
				xml.AppendLine(@"<LinkId>" + item.ExtId + @"</LinkId>");
			xml.AppendLine(@"</EmailBin>");
			using (var sw = new StreamWriter(RemoteResourceManager.Instance.EmailBinFile.LocalPath, false))
			{
				sw.Write(xml.ToString());
				sw.Flush();
			}
		}

		public void AddLink(LibraryFileLink fileLink)
		{
			EmailLinks.Add(fileLink);
			Save();
			ListChanged?.Invoke(this, EventArgs.Empty);
		}

		public void RemoveLink(LibraryFileLink fileLink)
		{
			EmailLinks.Remove(fileLink);
			Save();
			ListChanged?.Invoke(this, EventArgs.Empty);
		}

		public void ClearAll()
		{
			EmailLinks.Clear();
			Save();
			ListChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
