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
				String.Format("<b>{0}</b>{1}",
					selectedLink.Name,
					selectedLink.Tags.HasCategories ? String.Format("{0}({1})", Environment.NewLine, selectedLink.Tags.AllCategories) : String.Empty) :
				String.Empty;
		}
	}
}
