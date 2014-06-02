using System.Diagnostics;
using SalesDepot.CoreObjects.BusinessClasses;

namespace OvernightsCalendarViewer.BusinessClasses
{
	public class LinkManager
	{
		private static LinkManager _instance;

		private LinkManager() { }

		public static LinkManager Instance
		{
			get
			{
				if (_instance == null)
					_instance = new LinkManager();
				return _instance;
			}
		}

		public void OpenLink(LibraryLink link, bool specialOptions = false)
		{
			switch (link.Type)
			{
				case FileTypes.OvernightsLink:
					StartProcess(link);
					break;
			}
		}

		public void StartProcess(LibraryLink link)
		{
			try
			{
				Process.Start(link.LocalPath);
			}
			catch
			{
				AppManager.Instance.ShowWarning("This Link is not active");
			}
		}
	}
}