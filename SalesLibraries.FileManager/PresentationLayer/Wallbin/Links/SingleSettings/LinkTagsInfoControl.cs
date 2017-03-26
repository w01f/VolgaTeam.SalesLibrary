using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[ToolboxItem(true)]
	public partial class LinkTagsInfoControl : Panel
	{
		public LinkTagsInfoControl()
		{
			InitializeComponent();

			xtraScrollableControl.Controls.Add(labelControl);
			Controls.Add(xtraScrollableControl);
		}

		public void UpdateData()
		{
			if (MainController.Instance.WallbinViews.Selection.SelectedObjects.Count > 0)
			{
				string tagsText;
				if (MainController.Instance.WallbinViews.Selection.SelectedLinks.Count == 1)
				{
					var singleLink = MainController.Instance.WallbinViews.Selection.SelectedObjects.First();
					if (singleLink is LibraryFolderLink)
						tagsText = ((LibraryFolderLink)singleLink).AllGroupLinks.ToList().GetAllTags();
					else
						tagsText = singleLink.Tags.AllTags;
				}
				else
					tagsText = MainController.Instance.WallbinViews.Selection.SelectedObjects.GetCommonTags();

				if (!String.IsNullOrEmpty(tagsText))
				{
					labelControl.Text = String.Format("<color=gray>{0}</color>", tagsText);
					Visible = true;
					return;
				}
			}
			labelControl.Text = String.Empty;
			Visible = false;
		}
	}
}
