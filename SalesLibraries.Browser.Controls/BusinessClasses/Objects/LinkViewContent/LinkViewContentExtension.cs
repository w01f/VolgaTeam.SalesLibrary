using System;
using SalesLibraries.Browser.Controls.BusinessClasses.Interfaces;
using EO.WebBrowser;

namespace SalesLibraries.Browser.Controls.BusinessClasses.Objects.LinkViewContent
{
	class LinkViewContentExtension
	{
		public const string SendLinkDataFunctionName = "SalesLibraryExtensions_sendLinkData";
		public const string ReleaseLinkDataFunctionName = "SalesLibraryExtensions_releaseLinkData";
		public const string SwitchDataFunctionName = "SalesLibraryExtensions_switchPage";

		public ViewContent CurrentLinkViewContent { get; private set; }
		public event EventHandler<EventArgs> ContetChanged;

		public PageContent CurrentPageContent => CurrentLinkViewContent as PageContent;
		public PowerPointContent CurrentPowerPointContent => CurrentLinkViewContent as PowerPointContent;
		public IPrintableContent CurrentPrintableContent => CurrentLinkViewContent as IPrintableContent;
		public VideoContent CurrentVideoContent => CurrentLinkViewContent as VideoContent;
		public bool ContentEnabled => CurrentLinkViewContent != null;
		public bool PowerPointEnabled => CurrentPowerPointContent != null;
		public bool PrintEnabled => CurrentPrintableContent != null;
		public bool VideoEnabled => CurrentVideoContent != null;

		private void LoadData(params object[] args)
		{
			var format = args[0] as string;
			switch (format)
			{
				case "ppt":
					CurrentLinkViewContent = new PowerPointContent();
					break;
				case "doc":
					CurrentLinkViewContent = new WordContent();
					break;
				case "xls":
					CurrentLinkViewContent = new ExcelContent();
					break;
				case "pdf":
					CurrentLinkViewContent = new PdfContent();
					break;
				case "video":
				case "mp4":
				case "wmv":
					CurrentLinkViewContent = new VideoContent();
					break;
			}
			CurrentLinkViewContent?.Load(args);
			ContetChanged?.Invoke(this, EventArgs.Empty);
		}

		private void ReleaseData()
		{
			CurrentLinkViewContent = null;
			ContetChanged?.Invoke(this, EventArgs.Empty);
		}

		private void SwitchDocumentPage(params object[] args)
		{
			CurrentPageContent?.SwitchCurrentPart(Int32.Parse(args[0].ToString()));
		}

		public void OnJavaScriptCall(object sender, JSExtInvokeArgs e)
		{
			switch (e.FunctionName)
			{
				case SendLinkDataFunctionName:
					LoadData(e.Arguments);
					break;
				case ReleaseLinkDataFunctionName:
					ReleaseData();
					break;
				case SwitchDataFunctionName:
					SwitchDocumentPage(e.Arguments);
					break;
			}
		}
	}
}
