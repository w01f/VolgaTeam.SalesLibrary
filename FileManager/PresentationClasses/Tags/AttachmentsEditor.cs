using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using FileManager.Controllers;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.Tags
{
	[ToolboxItem(false)]
	public partial class AttachmentsEditor : UserControl, ITagsEditor
	{
		private AttachmentProperties _attachmentProperties;
		private bool _needToApply;
		public bool NeedToApply
		{
			get { return _needToApply; }
			set
			{
				_needToApply = value;
				var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
				if (activePage != null) activePage.Parent.StateChanged = true;
			}
		}
		public AttachmentsEditor()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			repositoryItemButtonEditAttachmentsFiles.Enter += FormMain.Instance.EditorEnter;
			repositoryItemButtonEditAttachmentsFiles.MouseUp += FormMain.Instance.EditorMouseUp;
			repositoryItemButtonEditAttachmentsFiles.MouseDown += FormMain.Instance.EditorMouseUp;
			repositoryItemButtonEditAttachmentsWeb.Enter += FormMain.Instance.EditorEnter;
			repositoryItemButtonEditAttachmentsWeb.MouseUp += FormMain.Instance.EditorMouseUp;
			repositoryItemButtonEditAttachmentsWeb.MouseDown += FormMain.Instance.EditorMouseUp;

			if (!((base.CreateGraphics()).DpiX > 96)) return;
			var styleControllerFont = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
			styleController.AppearanceDisabled.Font = styleControllerFont;
			styleController.AppearanceDropDown.Font = styleControllerFont;
			styleController.AppearanceDropDownHeader.Font = styleControllerFont;
			styleController.AppearanceFocused.Font = styleControllerFont;
			styleController.AppearanceReadOnly.Font = styleControllerFont;
			buttonXAttachmentsFilesAdd.Font = new Font(buttonXAttachmentsFilesAdd.Font.FontFamily, buttonXAttachmentsFilesAdd.Font.Size - 2, buttonXAttachmentsFilesAdd.Font.Style);
			buttonXAttachmentsWebAdd.Font = new Font(buttonXAttachmentsWebAdd.Font.FontFamily, buttonXAttachmentsWebAdd.Font.Size - 2, buttonXAttachmentsWebAdd.Font.Style);
		}

		#region ITagsEditor Members
		public event EventHandler<EventArgs> EditorChanged;
		public void UpdateData()
		{
			checkBoxEnableAttachmnets.Checked = false;
			gridControlAttachmentsFiles.DataSource = null;
			gridControlAttachmentsWeb.DataSource = null;
			_attachmentProperties = null;
			xtraTabControl.SelectedTabPageIndex = 0;
			Enabled = false;

			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			var defaultLink = activePage.SelectedLinks.FirstOrDefault();
			Enabled = defaultLink != null;
			if (defaultLink == null) return;

			_attachmentProperties = defaultLink.AttachmentProperties.Clone(defaultLink);
			checkBoxEnableAttachmnets.Checked = _attachmentProperties.Enable;
			gridControlAttachmentsFiles.DataSource = _attachmentProperties.FilesAttachments;
			gridControlAttachmentsWeb.DataSource = _attachmentProperties.WebAttachments;
		}
		public void ApplyData()
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;

			_attachmentProperties.Enable = checkBoxEnableAttachmnets.Checked;
			gridViewAttachmentsFiles.PostEditor();
			gridViewAttachmentsWeb.PostEditor();
			foreach (var link in activePage.SelectedLinks)
			{
				if (!_attachmentProperties.Enable)
				{
					_attachmentProperties.FilesAttachments.Clear();
					_attachmentProperties.WebAttachments.Clear();
				}
				link.AttachmentProperties = _attachmentProperties.Clone(link);
			}

			activePage.Parent.StateChanged = true;
			activePage.RefreshSelectedLinks();
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());
		}
		#endregion

		private void checkBoxEnableAttachments_CheckedChanged(object sender, EventArgs e)
		{
			xtraTabControl.Enabled = checkBoxEnableAttachmnets.Checked;
		}

		#region Files
		private void buttonXAttachmentsFilesAdd_Click(object sender, EventArgs e)
		{
			using (var dialog = new OpenFileDialog())
			{
				dialog.Multiselect = true;
				dialog.Title = "Attach file";
				if (dialog.ShowDialog() != DialogResult.OK) return;
				foreach (var fileName in dialog.FileNames)
				{
					if (_attachmentProperties.FilesAttachments.Any(x => x.OriginalPath.ToLower().Equals(fileName.ToLower()))) continue;
					var attachment = new LinkAttachment(_attachmentProperties);
					attachment.Type = AttachmentType.File;
					attachment.OriginalPath = fileName;
					_attachmentProperties.FilesAttachments.Add(attachment);
					gridViewAttachmentsFiles.RefreshData();
					if (gridViewAttachmentsFiles.RowCount <= 0) continue;
					gridViewAttachmentsFiles.FocusedRowHandle = gridViewAttachmentsFiles.RowCount - 1;
					gridViewAttachmentsFiles.MakeRowVisible(gridViewAttachmentsFiles.FocusedRowHandle, true);
					NeedToApply = true;
				}
			}
		}

		private void repositoryItemButtonEditAttachmentsFiles_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			gridViewAttachmentsFiles.CloseEditor();
			if (gridViewAttachmentsFiles.FocusedRowHandle == GridControl.InvalidRowHandle) return;
			switch (e.Button.Index)
			{
				case 0:
					{
						var attachment = _attachmentProperties.FilesAttachments[gridViewAttachmentsFiles.GetDataSourceRowIndex(gridViewAttachmentsFiles.FocusedRowHandle)];
						if (attachment.IsSourceAvailable)
						{
							try
							{
								Process.Start(attachment.OriginalPath);
							}
							catch
							{
								AppManager.Instance.ShowWarning("Attachment is not available");
							}
						}
						else
							AppManager.Instance.ShowWarning("Attachment is not available");
					}
					break;
				case 1:
					_attachmentProperties.FilesAttachments.RemoveAt(gridViewAttachmentsFiles.GetDataSourceRowIndex(gridViewAttachmentsFiles.FocusedRowHandle));
					gridViewAttachmentsFiles.RefreshData();
					NeedToApply = true;
					break;
			}
		}

		private void gridViewAttachmentsFiles_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			int attachmentIndex = gridViewAttachmentsFiles.GetDataSourceRowIndex(e.RowHandle);
			if (attachmentIndex < 0 || attachmentIndex >= _attachmentProperties.FilesAttachments.Count) return;
			var attachment = _attachmentProperties.FilesAttachments[attachmentIndex];
			e.Appearance.ForeColor = attachment.IsSourceAvailable ? Color.Black : Color.Red;
		}
		#endregion

		#region Websites
		private void buttonXAttachmentsWebAdd_Click(object sender, EventArgs e)
		{
			gridViewAttachmentsWeb.CloseEditor();
			_attachmentProperties.WebAttachments.RemoveAll(x => string.IsNullOrEmpty(x.OriginalPath));
			var attachment = new LinkAttachment(_attachmentProperties);
			attachment.Type = AttachmentType.Url;
			_attachmentProperties.WebAttachments.Add(attachment);
			gridViewAttachmentsWeb.RefreshData();
			if (gridViewAttachmentsWeb.RowCount > 0)
			{
				gridViewAttachmentsWeb.FocusedRowHandle = gridViewAttachmentsWeb.RowCount - 1;
				gridViewAttachmentsWeb.MakeRowVisible(gridViewAttachmentsWeb.FocusedRowHandle, true);
			}
			NeedToApply = true;
		}

		private void repositoryItemButtonEditAttachmentsWeb_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			gridViewAttachmentsWeb.CloseEditor();
			if (gridViewAttachmentsWeb.FocusedRowHandle == GridControl.InvalidRowHandle) return;
			switch (e.Button.Index)
			{
				case 0:
					{
						var attachment = _attachmentProperties.WebAttachments[gridViewAttachmentsWeb.GetDataSourceRowIndex(gridViewAttachmentsWeb.FocusedRowHandle)];
						try
						{
							Process.Start(attachment.OriginalPath);
						}
						catch
						{
							AppManager.Instance.ShowWarning("Website is not available");
						}
					}
					break;
				case 1:
					_attachmentProperties.WebAttachments.RemoveAt(gridViewAttachmentsWeb.GetDataSourceRowIndex(gridViewAttachmentsWeb.FocusedRowHandle));
					gridViewAttachmentsWeb.RefreshData();
					NeedToApply = true;
					break;
			}
		}
		#endregion
	}
}