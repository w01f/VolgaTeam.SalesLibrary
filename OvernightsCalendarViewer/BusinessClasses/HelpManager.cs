using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;

namespace OvernightsCalendarViewer.BusinessClasses
{
	internal class HelpManager
	{
		private static readonly HelpManager _instance = new HelpManager();
		private readonly Dictionary<string, string> _helpLinks = new Dictionary<string, string>();
		private readonly string _helpLinksPath = string.Empty;

		private HelpManager()
		{
			_helpLinksPath = string.Format(@"{0}\newlocaldirect.com\app\HelpUrls\OvernightsHelp.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			LoadHelpLinks();
		}

		public static HelpManager Instance
		{
			get { return _instance; }
		}

		private void LoadHelpLinks()
		{
			_helpLinks.Clear();
			XmlNode node;
			if (File.Exists(_helpLinksPath))
			{
				var document = new XmlDocument();
				document.Load(_helpLinksPath);
				node = document.SelectSingleNode(@"/Help");
				if (node != null)
				{
					foreach (XmlNode childNode in node.ChildNodes)
					{
						if (!_helpLinks.Keys.Contains(childNode.Name))
							_helpLinks.Add(childNode.Name, childNode.InnerText);
					}
				}
			}
		}

		public void OpenHelpLink(string helpKey)
		{
			if (_helpLinks.Keys.Contains(helpKey))
			{
				try
				{
					Process.Start(_helpLinks[helpKey]);
				}
				catch
				{
					AppManager.Instance.ShowWarning("Couldn't open Help link for this page");
				}
			}
			else
				AppManager.Instance.ShowWarning("Help link for this page was not found");
		}
	}
}