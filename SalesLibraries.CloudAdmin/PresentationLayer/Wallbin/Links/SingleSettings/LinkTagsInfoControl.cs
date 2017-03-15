using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.CloudAdmin.Controllers;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
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
				var tagsText = MainController.Instance.WallbinViews.Selection.SelectedLinks.Count == 1
					? MainController.Instance.WallbinViews.Selection.SelectedObjects.First().Tags.AllTags
					: MainController.Instance.WallbinViews.Selection.SelectedObjects.GetCommonTags();
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
