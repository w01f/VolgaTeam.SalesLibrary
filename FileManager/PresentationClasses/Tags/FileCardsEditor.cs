using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using FileManager.Controllers;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.Tags
{
	[ToolboxItem(false)]
	public partial class FileCardsEditor : UserControl, ITagsEditor
	{
		public List<StringDataSourceWrapper> FileCardImportantInfo { get; private set; }
		private bool _loading;
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
		public FileCardsEditor()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			FileCardImportantInfo = new List<StringDataSourceWrapper>();

			textEditFileCardAdvertiser.Enter += FormMain.Instance.EditorEnter;
			textEditFileCardAdvertiser.MouseUp += FormMain.Instance.EditorMouseUp;
			textEditFileCardAdvertiser.MouseDown += FormMain.Instance.EditorMouseUp;
			dateEditFileCardDateSold.Enter += FormMain.Instance.EditorEnter;
			dateEditFileCardDateSold.MouseUp += FormMain.Instance.EditorMouseUp;
			dateEditFileCardDateSold.MouseDown += FormMain.Instance.EditorMouseUp;
			spinEditFileCardBroadcastClosed.Enter += FormMain.Instance.EditorEnter;
			spinEditFileCardBroadcastClosed.MouseUp += FormMain.Instance.EditorMouseUp;
			spinEditFileCardBroadcastClosed.MouseDown += FormMain.Instance.EditorMouseUp;
			spinEditFileCardDigitalClosed.Enter += FormMain.Instance.EditorEnter;
			spinEditFileCardDigitalClosed.MouseUp += FormMain.Instance.EditorMouseUp;
			spinEditFileCardDigitalClosed.MouseDown += FormMain.Instance.EditorMouseUp;
			spinEditFileCardPublishingClosed.Enter += FormMain.Instance.EditorEnter;
			spinEditFileCardPublishingClosed.MouseUp += FormMain.Instance.EditorMouseUp;
			spinEditFileCardPublishingClosed.MouseDown += FormMain.Instance.EditorMouseUp;
			textEditFileCardSalesName.Enter += FormMain.Instance.EditorEnter;
			textEditFileCardSalesName.MouseUp += FormMain.Instance.EditorMouseUp;
			textEditFileCardSalesName.MouseDown += FormMain.Instance.EditorMouseUp;
			textEditFileCardSalesEmail.Enter += FormMain.Instance.EditorEnter;
			textEditFileCardSalesEmail.MouseUp += FormMain.Instance.EditorMouseUp;
			textEditFileCardSalesEmail.MouseDown += FormMain.Instance.EditorMouseUp;
			textEditFileCardSalesPhone.Enter += FormMain.Instance.EditorEnter;
			textEditFileCardSalesPhone.MouseUp += FormMain.Instance.EditorMouseUp;
			textEditFileCardSalesPhone.MouseDown += FormMain.Instance.EditorMouseUp;
			textEditFileCardSalesStation.Enter += FormMain.Instance.EditorEnter;
			textEditFileCardSalesStation.MouseUp += FormMain.Instance.EditorMouseUp;
			textEditFileCardSalesStation.MouseDown += FormMain.Instance.EditorMouseUp;
			repositoryItemMemoEditFileCardImportantInfo.Enter += FormMain.Instance.EditorEnter;
			repositoryItemMemoEditFileCardImportantInfo.MouseUp += FormMain.Instance.EditorMouseUp;
			repositoryItemMemoEditFileCardImportantInfo.MouseDown += FormMain.Instance.EditorMouseUp;

			if (!((CreateGraphics()).DpiX > 96)) return;
			var styleControllerFont = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
			styleController.AppearanceDisabled.Font = styleControllerFont;
			styleController.AppearanceDropDown.Font = styleControllerFont;
			styleController.AppearanceDropDownHeader.Font = styleControllerFont;
			styleController.AppearanceFocused.Font = styleControllerFont;
			styleController.AppearanceReadOnly.Font = styleControllerFont;
			checkBoxEnableFileCard.Font = new Font(checkBoxEnableFileCard.Font.FontFamily, checkBoxEnableFileCard.Font.Size - 2, checkBoxEnableFileCard.Font.Style);
			checkBoxFileCardAdvertiser.Font = new Font(checkBoxFileCardAdvertiser.Font.FontFamily, checkBoxFileCardAdvertiser.Font.Size - 2, checkBoxFileCardAdvertiser.Font.Style);
			checkBoxFileCardDateSold.Font = new Font(checkBoxFileCardDateSold.Font.FontFamily, checkBoxFileCardDateSold.Font.Size - 2, checkBoxFileCardDateSold.Font.Style);
			checkBoxFileCardBroadcastClosed.Font = new Font(checkBoxFileCardBroadcastClosed.Font.FontFamily, checkBoxFileCardBroadcastClosed.Font.Size - 2, checkBoxFileCardBroadcastClosed.Font.Style);
			checkBoxFileCardDigitalClosed.Font = new Font(checkBoxFileCardDigitalClosed.Font.FontFamily, checkBoxFileCardDigitalClosed.Font.Size - 2, checkBoxFileCardDigitalClosed.Font.Style);
			checkBoxFileCardPublishingClosed.Font = new Font(checkBoxFileCardPublishingClosed.Font.FontFamily, checkBoxFileCardPublishingClosed.Font.Size - 2, checkBoxFileCardPublishingClosed.Font.Style);
			checkBoxFileCardSalesInfo.Font = new Font(checkBoxFileCardSalesInfo.Font.FontFamily, checkBoxFileCardSalesInfo.Font.Size - 2, checkBoxFileCardSalesInfo.Font.Style);
			checkBoxFileCardImportantInfo.Font = new Font(checkBoxFileCardImportantInfo.Font.FontFamily, checkBoxFileCardImportantInfo.Font.Size - 2, checkBoxFileCardImportantInfo.Font.Style);
			buttonXImportantInfoAdd.Font = new Font(buttonXImportantInfoAdd.Font.FontFamily, buttonXImportantInfoAdd.Font.Size - 2, buttonXImportantInfoAdd.Font.Style);
			buttonXReset.Font = new Font(buttonXReset.Font.FontFamily, buttonXReset.Font.Size - 2, buttonXReset.Font.Style);
		}

		#region ITagsEditor Members
		public event EventHandler<EventArgs> EditorChanged;

		public void UpdateData()
		{
			pnButtons.Enabled = false;
			pnData.Enabled = false;
			_loading = true;
			//Clear Controls
			{
				checkBoxEnableFileCard.Checked = false;
				textEditFileCardTitle.EditValue = null;
				checkBoxFileCardAdvertiser.Checked = false;
				textEditFileCardAdvertiser.EditValue = null;
				checkBoxFileCardDateSold.Checked = false;
				dateEditFileCardDateSold.EditValue = null;
				checkBoxFileCardBroadcastClosed.Checked = false;
				spinEditFileCardBroadcastClosed.EditValue = null;
				checkBoxFileCardDigitalClosed.Checked = false;
				spinEditFileCardDigitalClosed.EditValue = null;
				checkBoxFileCardPublishingClosed.Checked = false;
				spinEditFileCardPublishingClosed.EditValue = null;
				checkBoxFileCardSalesInfo.Checked = false;
				textEditFileCardSalesName.EditValue = null;
				textEditFileCardSalesEmail.EditValue = null;
				textEditFileCardSalesPhone.EditValue = null;
				textEditFileCardSalesStation.EditValue = null;

				checkBoxFileCardImportantInfo.Checked = false;
				gridControlFileCardImportantInfo.DataSource = null;
				Enabled = false;
			}
			FileCardImportantInfo.Clear();

			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			var defaultLink = activePage.SelectedLinks.FirstOrDefault();
			Enabled = defaultLink != null;
			if (defaultLink == null) return;

			var noData = activePage.SelectedLinks.All(x => !x.FileCard.Enable);
			var sameData = defaultLink != null && activePage.SelectedLinks.All(x => x.FileCard.Compare(defaultLink.FileCard));

			pnButtons.Enabled = !noData;
			pnData.Enabled = sameData || noData;

			if (sameData)
			{
				checkBoxEnableFileCard.Checked = defaultLink.FileCard.Enable;
				textEditFileCardTitle.EditValue = defaultLink.FileCard.Title;
				checkBoxFileCardAdvertiser.Checked = !string.IsNullOrEmpty(defaultLink.FileCard.Advertiser);
				textEditFileCardAdvertiser.EditValue = defaultLink.FileCard.Advertiser;
				checkBoxFileCardDateSold.Checked = defaultLink.FileCard.DateSold.HasValue;
				dateEditFileCardDateSold.EditValue = defaultLink.FileCard.DateSold;
				checkBoxFileCardBroadcastClosed.Checked = defaultLink.FileCard.BroadcastClosed.HasValue;
				spinEditFileCardBroadcastClosed.EditValue = defaultLink.FileCard.BroadcastClosed;
				checkBoxFileCardDigitalClosed.Checked = defaultLink.FileCard.DigitalClosed.HasValue;
				spinEditFileCardDigitalClosed.EditValue = defaultLink.FileCard.DigitalClosed;
				checkBoxFileCardPublishingClosed.Checked = defaultLink.FileCard.PublishingClosed.HasValue;
				spinEditFileCardPublishingClosed.EditValue = defaultLink.FileCard.PublishingClosed;
				checkBoxFileCardSalesInfo.Checked = !string.IsNullOrEmpty(defaultLink.FileCard.SalesName) || !string.IsNullOrEmpty(defaultLink.FileCard.SalesEmail) || !string.IsNullOrEmpty(defaultLink.FileCard.SalesPhone) || !string.IsNullOrEmpty(defaultLink.FileCard.SalesStation);
				textEditFileCardSalesName.EditValue = defaultLink.FileCard.SalesName;
				textEditFileCardSalesEmail.EditValue = defaultLink.FileCard.SalesEmail;
				textEditFileCardSalesPhone.EditValue = defaultLink.FileCard.SalesPhone;
				textEditFileCardSalesStation.EditValue = defaultLink.FileCard.SalesStation;

				FileCardImportantInfo.AddRange(defaultLink.FileCard.Notes.Select(x => new StringDataSourceWrapper(x)));
				checkBoxFileCardImportantInfo.Checked = FileCardImportantInfo.Count > 0;
				gridControlFileCardImportantInfo.DataSource = FileCardImportantInfo;
				gridViewFileCardImportantInfo.RefreshData();
			}
			gridControlFileCardImportantInfo.DataSource = FileCardImportantInfo;
			_loading = false;
		}

		public void ApplyData()
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;

			gridViewFileCardImportantInfo.CloseEditor();
			FileCardImportantInfo.RemoveAll(x => string.IsNullOrEmpty(x.Value));

			foreach (var link in activePage.SelectedLinks)
			{
				link.FileCard.Enable = checkBoxEnableFileCard.Checked;
				link.FileCard.Title = textEditFileCardTitle.EditValue != null ? textEditFileCardTitle.EditValue.ToString() : string.Empty;
				link.FileCard.Advertiser = link.FileCard.Enable && checkBoxFileCardAdvertiser.Checked && textEditFileCardAdvertiser.EditValue != null ? textEditFileCardAdvertiser.EditValue.ToString() : null;
				link.FileCard.DateSold = link.FileCard.Enable && checkBoxFileCardDateSold.Checked && dateEditFileCardDateSold.EditValue != null ? (DateTime?)dateEditFileCardDateSold.DateTime : null;
				link.FileCard.BroadcastClosed = link.FileCard.Enable && checkBoxFileCardBroadcastClosed.Checked && spinEditFileCardBroadcastClosed.EditValue != null ? (double?)spinEditFileCardBroadcastClosed.Value : null;
				link.FileCard.DigitalClosed = link.FileCard.Enable && checkBoxFileCardDigitalClosed.Checked && spinEditFileCardDigitalClosed.EditValue != null ? (double?)spinEditFileCardDigitalClosed.Value : null;
				link.FileCard.PublishingClosed = link.FileCard.Enable && checkBoxFileCardPublishingClosed.Checked && spinEditFileCardPublishingClosed.EditValue != null ? (double?)spinEditFileCardPublishingClosed.Value : null;
				link.FileCard.SalesName = link.FileCard.Enable && checkBoxFileCardSalesInfo.Checked && textEditFileCardSalesName.EditValue != null ? textEditFileCardSalesName.EditValue.ToString() : null;
				link.FileCard.SalesEmail = link.FileCard.Enable && checkBoxFileCardSalesInfo.Checked && textEditFileCardSalesEmail.EditValue != null ? textEditFileCardSalesEmail.EditValue.ToString() : null;
				link.FileCard.SalesPhone = link.FileCard.Enable && checkBoxFileCardSalesInfo.Checked && textEditFileCardSalesPhone.EditValue != null ? textEditFileCardSalesPhone.EditValue.ToString() : null;
				link.FileCard.SalesStation = link.FileCard.Enable && checkBoxFileCardSalesInfo.Checked && textEditFileCardSalesStation.EditValue != null ? textEditFileCardSalesStation.EditValue.ToString() : null;
				link.FileCard.Notes.Clear();
				link.FileCard.Notes.AddRange(FileCardImportantInfo.Where(x => !string.IsNullOrEmpty(x.Value)).Select(x => x.Value));
			}

			activePage.Parent.StateChanged = true;
			activePage.RefreshSelectedLinks();
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());
		}

		public void ResetData()
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Are you sure You want to DELETE ALL FILECARDS for the selected files?") != DialogResult.Yes) return;
			foreach (var link in activePage.SelectedLinks)
			{
				link.FileCard.Enable = false;
				link.FileCard.Title = string.Empty;
				link.FileCard.Advertiser = null;
				link.FileCard.DateSold = null;
				link.FileCard.BroadcastClosed = null;
				link.FileCard.DigitalClosed = null;
				link.FileCard.PublishingClosed = null;
				link.FileCard.SalesName = null;
				link.FileCard.SalesEmail = null;
				link.FileCard.SalesPhone = null;
				link.FileCard.SalesStation = null;
				link.FileCard.Notes.Clear();
			}
			activePage.Parent.StateChanged = true;
			activePage.RefreshSelectedLinks();
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());

			UpdateData();			
		}
		#endregion

		private void buttonXReset_Click(object sender, EventArgs e)
		{
			ResetData();
		}

		private void checkBoxEnableFileCard_CheckedChanged(object sender, EventArgs e)
		{
			groupBoxFileCard.Enabled = checkBoxEnableFileCard.Checked;
			textEditFileCardTitle.Enabled = checkBoxEnableFileCard.Checked;
			if (!checkBoxEnableFileCard.Checked)
			{
				checkBoxFileCardAdvertiser.Checked = false;
				checkBoxFileCardDateSold.Checked = false;
				checkBoxFileCardBroadcastClosed.Checked = false;
				checkBoxFileCardDigitalClosed.Checked = false;
				checkBoxFileCardPublishingClosed.Checked = false;
				checkBoxFileCardSalesInfo.Checked = false;
			}
		}

		private void checkBoxFileCardAdvertiser_CheckedChanged(object sender, EventArgs e)
		{
			textEditFileCardAdvertiser.Enabled = checkBoxFileCardAdvertiser.Checked;
			if (!checkBoxFileCardAdvertiser.Checked)
				textEditFileCardAdvertiser.EditValue = null;
		}

		private void checkBoxFileCardDateSold_CheckedChanged(object sender, EventArgs e)
		{
			dateEditFileCardDateSold.Enabled = checkBoxFileCardDateSold.Checked;
			if (!checkBoxFileCardDateSold.Checked)
				dateEditFileCardDateSold.EditValue = null;
		}

		private void checkBoxFileCardBroadcastClosed_CheckedChanged(object sender, EventArgs e)
		{
			spinEditFileCardBroadcastClosed.Enabled = checkBoxFileCardBroadcastClosed.Checked;
			if (!checkBoxFileCardBroadcastClosed.Checked)
				spinEditFileCardBroadcastClosed.EditValue = null;
		}

		private void checkBoxFileCardDigitalClosed_CheckedChanged(object sender, EventArgs e)
		{
			spinEditFileCardDigitalClosed.Enabled = checkBoxFileCardDigitalClosed.Checked;
			if (!checkBoxFileCardDigitalClosed.Checked)
				spinEditFileCardDigitalClosed.EditValue = null;
		}

		private void checkBoxFileCardPublishingClosed_CheckedChanged(object sender, EventArgs e)
		{
			spinEditFileCardPublishingClosed.Enabled = checkBoxFileCardPublishingClosed.Checked;
			if (!checkBoxFileCardPublishingClosed.Checked)
				spinEditFileCardPublishingClosed.EditValue = null;
		}

		private void checkBoxFileCardSalesInfo_CheckedChanged(object sender, EventArgs e)
		{
			textEditFileCardSalesName.Enabled = checkBoxFileCardSalesInfo.Checked;
			textEditFileCardSalesEmail.Enabled = checkBoxFileCardSalesInfo.Checked;
			textEditFileCardSalesPhone.Enabled = checkBoxFileCardSalesInfo.Checked;
			textEditFileCardSalesStation.Enabled = checkBoxFileCardSalesInfo.Checked;
			if (!checkBoxFileCardSalesInfo.Checked)
			{
				textEditFileCardSalesName.EditValue = null;
				textEditFileCardSalesEmail.EditValue = null;
				textEditFileCardSalesPhone.EditValue = null;
				textEditFileCardSalesStation.EditValue = null;
			}
		}

		private void checkBoxFileCardImportantInfo_CheckedChanged(object sender, EventArgs e)
		{
			buttonXImportantInfoAdd.Enabled = checkBoxFileCardImportantInfo.Checked;
			gridControlFileCardImportantInfo.Enabled = checkBoxFileCardImportantInfo.Checked;
			if (!checkBoxFileCardImportantInfo.Checked)
			{
				FileCardImportantInfo.Clear();
				gridViewFileCardImportantInfo.RefreshData();
			}
		}

		private void buttonXFileCardImportantInfoAdd_Click(object sender, EventArgs e)
		{
			gridViewFileCardImportantInfo.CloseEditor();
			FileCardImportantInfo.RemoveAll(x => string.IsNullOrEmpty(x.Value));
			FileCardImportantInfo.Add(new StringDataSourceWrapper());
			gridViewFileCardImportantInfo.RefreshData();
			if (gridViewFileCardImportantInfo.RowCount > 0)
			{
				gridViewFileCardImportantInfo.FocusedRowHandle = gridViewFileCardImportantInfo.RowCount - 1;
				gridViewFileCardImportantInfo.MakeRowVisible(gridViewFileCardImportantInfo.FocusedRowHandle, true);
			}
			NeedToApply = true;
		}

		private void repositoryItemButtonEditFileCardImportantInfo_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			gridViewFileCardImportantInfo.CloseEditor();
			if (gridViewFileCardImportantInfo.FocusedRowHandle >= 0 && gridViewFileCardImportantInfo.FocusedRowHandle < gridViewFileCardImportantInfo.RowCount)
			{
				FileCardImportantInfo.RemoveAt(gridViewFileCardImportantInfo.GetDataSourceRowIndex(gridViewFileCardImportantInfo.FocusedRowHandle));
				gridViewFileCardImportantInfo.RefreshData();
				NeedToApply = true;
			}
		}

		private void EditValueChanged(object sender, EventArgs e)
		{
			if (!_loading)
				NeedToApply = true;
		}
	}
}