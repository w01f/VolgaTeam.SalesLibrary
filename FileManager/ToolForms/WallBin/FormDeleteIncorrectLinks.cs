using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.ToolForms.WallBin
{
    public partial class FormDeleteIncorrectLinks : Form
    {
        public bool ExpiredLinks { get; set; }
        public List<ILibraryFile> IncorrectLinks { get; set; }
        public List<Guid> LinksForDelete { get; set; }

        public FormDeleteIncorrectLinks()
        {
            InitializeComponent();
            this.IncorrectLinks = new List<ILibraryFile>();
            this.LinksForDelete = new List<Guid>();
        }

        private void DeadLinksForm_Load(object sender, EventArgs e)
        {
            btExpiredDate.Visible = this.ExpiredLinks;
            grIncorrectLinks.Rows.Clear();
            foreach (LibraryFile incorrectLink in this.IncorrectLinks)
            {
                DataGridViewRow row = grIncorrectLinks.Rows[grIncorrectLinks.Rows.Add(incorrectLink.Identifier, incorrectLink.Parent.Name, incorrectLink.Name, incorrectLink.OriginalPath)];
                row.Tag = incorrectLink;
            }
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (grIncorrectLinks.SelectedRows.Count > 0)
            {
                if (AppManager.Instance.ShowQuestion("Are you Sure you want to REMOVE this Link from the Library?") == System.Windows.Forms.DialogResult.Yes)
                {
                    this.LinksForDelete.Add((grIncorrectLinks.SelectedRows[0].Tag as LibraryFile).Identifier);
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
                LibraryFile file = grIncorrectLinks.SelectedRows[0].Tag as LibraryFile;
                if (file != null)
                {
                    using (ToolForms.WallBin.FormLinkProperties form = new ToolForms.WallBin.FormLinkProperties())
                    {
                        form.CaptionName = file.PropertiesName;
                        form.Note = file.Note;
                        form.IsBold = file.IsBold;
                        form.AddDate = file.AddDate;
                        form.ExpirationDateOptions = file.ExpirationDateOptions;
                        form.xtraTabPageNotes.PageVisible = false;
                        form.xtraTabPageSearchTags.PageVisible = false;
                        form.SearchTags = file.SearchTags;
                        form.StartPosition = FormStartPosition.CenterScreen;

                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            file.Note = form.Note;
                            file.IsBold = form.IsBold;
                            file.SearchTags = form.SearchTags;
                            file.ExpirationDateOptions = form.ExpirationDateOptions;
                        }
                    }
                }
            }
            else
            {
                AppManager.Instance.ShowWarning("There are no selected links");
            }
        }
    }
}
