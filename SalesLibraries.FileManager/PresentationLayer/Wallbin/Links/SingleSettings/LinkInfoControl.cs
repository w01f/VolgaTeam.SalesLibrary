using System;
using System.ComponentModel;
using DevExpress.XtraEditors;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[ToolboxItem(true)]
	public partial class LinkInfoControl : LabelControl
	{
		public LinkInfoControl()
		{
			InitializeComponent();
		}

		public void UpdateData()
		{
			var selectedFolder = MainController.Instance.WallbinViews.Selection.SelectedFolder;
			var selectedLink = MainController.Instance.WallbinViews.Selection.SelectedLink;
			Text = selectedFolder != null && selectedLink != null ?
				String.Format("{0}{1}",
					selectedLink.LinkInfoDisplayName,
					selectedLink.Tags.HasCategories || selectedLink.Tags.HasKeywords ?
						String.Format("{0}<size=-2><color=gray>({1})</color></size>",
							Environment.NewLine,
							selectedLink.Tags.AllTags) :
						String.Empty) :
				String.Empty;
		}
	}
}
