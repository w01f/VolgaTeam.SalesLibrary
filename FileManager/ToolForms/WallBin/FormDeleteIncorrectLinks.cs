using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.ToolForms.WallBin
{
	public partial class FormDeleteIncorrectLinks : MetroForm
	{
		public FormDeleteIncorrectLinks()
		{
			InitializeComponent();
			IncorrectLinks = new List<ILibraryLink>();
			LinksForDelete = new List<Guid>();
		}

		public bool ExpiredLinks { get; set; }
		public List<ILibraryLink> IncorrectLinks { get; set; }
		public List<Guid> LinksForDelete { get; set; }

		private void DeadLinksForm_Load(object sender, EventArgs e)
		{
			buttonXExpiredDate.Visible = ExpiredLinks;
			grIncorrectLinks.Rows.Clear();
			foreach (LibraryLink incorrectLink in IncorrectLinks)
			{
				DataGridViewRow row = grIncorrectLinks.Rows[grIncorrectLinks.Rows.Add(incorrectLink.Identifier, incorrectLink.Parent.Name, incorrectLink.Name, incorrectLink.OriginalPath)];
				row.Tag = incorrectLink;
			}
		}

		private void btDelete_Click(object sender, EventArgs e)
		{
			if (grIncorrectLinks.SelectedRows.Count > 0)
			{
				if (AppManager.Instance.ShowQuestion("Are you Sure you want to REMOVE this Link from the Library?") == DialogResult.Yes)
				{
					LinksForDelete.Add((grIncorrectLinks.SelectedRows[0].Tag as LibraryLink).Identifier);
					grIncorrectLinks.Rows.Remove(grIncorrectLinks.SelectedRows[0]);
				}
			}
			else
			{
				AppManager.Instance.ShowWarning("There are no selected links");
			}
		}

		private void btExpiredDate_Click(object sender, EventArgs e)
		{
			if (grIncorrectLinks.SelectedRows.Count > 0)
			{
				var file = grIncorrectLinks.SelectedRows[0].Tag as LibraryLink;
				if (file == null) return;
				using (var form = new FormLinkProperties(file.Parent.Parent.Parent as Library))
				{
					form.xtraTabControl.SelectedTabPage = form.xtraTabPageExpiredLinks;
					form.CaptionName = file.PropertiesName;
					form.AddDate = file.AddDate;
					form.ExpirationDateOptions = file.ExpirationDateOptions;
					form.StartPosition = FormStartPosition.CenterScreen;

					if (form.ShowDialog() != DialogResult.OK) return;
					file.LastChanged = DateTime.Now;
					file.ExpirationDateOptions = form.ExpirationDateOptions;
				}
			}
			else
			{
				AppManager.Instance.ShowWarning("There are no selected links");
			}
		}
	}
}