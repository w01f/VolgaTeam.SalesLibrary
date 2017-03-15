using System;
using System.ComponentModel;
using DevExpress.XtraEditors;
using SalesLibraries.CloudAdmin.Controllers;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
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
			if (MainController.Instance.WallbinViews.Selection.SelectedLinks.Count > 1)
			{
				Text = String.Format("Selected Links: {0}",
						MainController.Instance.WallbinViews.Selection.SelectedLinks.Count);
			}
			else if (MainController.Instance.WallbinViews.Selection.SelectedLinks.Count == 1)
			{
				var selectedLink = MainController.Instance.WallbinViews.Selection.SelectedLink;
				Text = selectedLink != null
					? String.Format("{0}", selectedLink.LinkInfoDisplayName)
					: String.Empty;
			}
			else
			{
				Text = String.Empty;
			}
		}
	}
}
