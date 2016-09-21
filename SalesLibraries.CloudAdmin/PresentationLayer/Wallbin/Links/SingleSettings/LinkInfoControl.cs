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
			var selectedFolder = MainController.Instance.WallbinViews.Selection.SelectedFolder;
			if (selectedFolder == null)
			{
				Text = String.Empty;
				return;
			}

			if (MainController.Instance.WallbinViews.Selection.SelectedObjectsCount > 1)
			{
				Text = String.Format("<b>({0})</b> {1}",
						MainController.Instance.WallbinViews.Selection.SelectedObjectsCount,
						MainController.Instance.WallbinViews.Selection.SelectedObjects.GetCommonTags());
			}
			else
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
		}
	}
}
