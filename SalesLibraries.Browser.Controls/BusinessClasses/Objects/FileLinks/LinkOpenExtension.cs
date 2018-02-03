using System;
using EO.WebBrowser;

namespace SalesLibraries.Browser.Controls.BusinessClasses.Objects.FileLinks
{
	class LinkOpenExtension
	{
		public const string OpenLinkFunctionName = "SalesLibraryExtensions_openLink";

		public BaseLinkData LinkData { get; private set; }

		public event EventHandler<EventArgs> LinkLoaded; 

		private void LoadData(params object[] args)
		{
			var format = args[0] as string;
			switch (format)
			{
				case "ppt":
					LinkData = new PowerPointLinkData();
					break;
				case "doc":
					LinkData = new WordLinkData();
					break;
				case "xls":
					LinkData = new ExcelLinkData();
					break;
				case "pdf":
					LinkData = new PdfLinkData();
					break;
				case "jpeg":
				case "png":
					LinkData = new ImageLinkData();
					break;
				case "lan":
					LinkData = new LanData();
					break;
				case "app":
					LinkData = new AppLinkData();
					break;
			}
			LinkData?.Load(args);
			LinkLoaded?.Invoke(this, EventArgs.Empty);
		}

		public void OnJavaScriptCall(object sender, JSExtInvokeArgs e)
		{
			switch (e.FunctionName)
			{
				case OpenLinkFunctionName:
					LoadData(e.Arguments);
					break;
			}
		}

		public void ReleaseData()
		{
			LinkData = null;
		}
	}
}
