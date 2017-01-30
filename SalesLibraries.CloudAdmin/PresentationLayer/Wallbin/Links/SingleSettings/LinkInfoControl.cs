using System;
using System.ComponentModel;
using DevExpress.XtraEditors;
using SalesLibraries.Business.Entities.Helpers;
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
				Text = String.Format("<b>(Selected Links: {0})</b> {1}",
						MainController.Instance.WallbinViews.Selection.SelectedLinks.Count,
						MainController.Instance.WallbinViews.Selection.SelectedObjects.GetCommonTags());
			}
			else if (MainController.Instance.WallbinViews.Selection.SelectedLinks.Count == 1)
			{
				var selectedLink = MainController.Instance.WallbinViews.Selection.SelectedLink;
				Text = selectedLink != null
					? String.Format("{0}{1}",
						selectedLink.LinkInfoDisplayName,
						selectedLink.Tags.HasCategories || selectedLink.Tags.HasKeywords
							? String.Format("{0}<size=-2><color=gray>({1})</color></size>",
								Environment.NewLine,
								selectedLink.Tags.AllTags)
							: String.Empty)
					: String.Empty;
			}
			else
			{
				Text = String.Empty;
			}
		}
	}
}
