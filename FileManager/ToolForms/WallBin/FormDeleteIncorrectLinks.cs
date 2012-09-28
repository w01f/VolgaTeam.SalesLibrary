using System;
using System.Collections.Generic;
using System.Linq;
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
                        form.EnableWidget = file.EnableWidget;
                        form.Widget = file.EnableWidget ? file.Widget : null;
                        form.BannerProperties = file.BannerProperties;
                        form.AddDate = file.AddDate;
                        form.ExpirationDateOptions = file.ExpirationDateOptions;
                        form.xtraTabPageNotes.PageVisible = false;
                        form.xtraTabPageSearchTags.PageVisible = false;
                        form.SearchTags = file.SearchTags;
                        form.Keywords.AddRange(file.CustomKeywords.Tags.Select(x => new StringDataSourceWrapper(x)));
                        form.FileCard = file.FileCard;
                        form.FileCardImportantInfo.Clear();
                        form.FileCardImportantInfo.AddRange(file.FileCard.Notes.Select(x => new StringDataSourceWrapper(x)));
                        form.AttachmentProperties = file.AttachmentProperties;
                        form.StartPosition = FormStartPosition.CenterScreen;

                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            file.LastChanged = DateTime.Now;
                            file.Widget = form.EnableWidget ? form.Widget : null;
                            file.EnableWidget = form.EnableWidget;
                            file.BannerProperties = form.BannerProperties;
                            file.Note = form.Note;
                            file.IsBold = form.IsBold;
                            file.SearchTags = form.SearchTags;
                            file.ExpirationDateOptions = form.ExpirationDateOptions;
                            file.FileCard = form.FileCard;
                            file.FileCard.Notes.Clear();
                            file.FileCard.Notes.AddRange(form.FileCardImportantInfo.Where(x => !string.IsNullOrEmpty(x.Value)).Select(x => x.Value));
                            file.AttachmentProperties = form.AttachmentProperties;
                            file.CustomKeywords.Tags.Clear();
                            file.CustomKeywords.Tags.AddRange(form.Keywords.Where(x => !string.IsNullOrEmpty(x.Value)).Select(x => x.Value));
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
