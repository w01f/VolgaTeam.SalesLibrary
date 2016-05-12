using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.FileManager.Business.Models.InactiveLinks;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Libraries
{
	public partial class FormDeleteInactiveLinks : MetroForm
	{
		public List<InactiveLink> InactiveLinks { get; private set; }

		public FormDeleteInactiveLinks(IEnumerable<InactiveLink> links)
		{
			InitializeComponent();
			InactiveLinks = new List<InactiveLink>();
			InactiveLinks.AddRange(links);
			LoadLinks();
		}

		private void LoadLinks()
		{
			gridControl.DataSource = InactiveLinks.Where(link => !link.IsDeleted).ToList();
		}

		private void btDelete_Click(object sender, EventArgs e)
		{
			var selectedLink = advBandedGridView.GetFocusedRow() as InactiveLink;
			if (selectedLink == null)
			{
				MainController.Instance.PopupMessages.ShowWarning("There are no selected links");
				return;
			}
			if (MainController.Instance.PopupMessages.ShowQuestion("Are you Sure you want to REMOVE this Link from the Library?") != DialogResult.Yes)
				return;
			selectedLink.IsDeleted = true;
			LoadLinks();
		}

		private void btExpiredDate_Click(object sender, EventArgs e)
		{
			var selectedLink = advBandedGridView.GetFocusedRow() as InactiveLink;
			if (selectedLink == null)
			{
				MainController.Instance.PopupMessages.ShowWarning("There are no selected links");
				return;
			}
			if (SettingsEditorFactory.Run(selectedLink.Link, LinkSettingsType.ExpirationDate) != DialogResult.OK)
				return;
			selectedLink.IsExpired = selectedLink.Link.ExpirationSettings.IsExpired;
			selectedLink.IsChanged = true;
		}

		private void advBandedGridView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
		{
			var link = advBandedGridView.GetRow(e.RowHandle) as InactiveLink;
			if (link == null) return;
			if (!(link.IsDead || link.IsExpired))
				e.Appearance.BackColor = Color.LightGreen;
		}
	}
}