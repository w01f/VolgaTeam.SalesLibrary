using System;
using SalesLibraries.Browser.Controls.Controls.WebPage;

namespace SalesLibraries.Browser.Controls.BusinessClasses.Events
{
	public class ClosePageEventArgs : EventArgs
	{
		public WebKitPage Page { get; set; }
		public bool NeedReleasePage { get; set; }
	}
}
