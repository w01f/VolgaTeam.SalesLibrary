using System;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.XtraTab;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings.ResetLinks
{
	public partial class FormResetLibraryLinks : MetroForm
	{
		private IResetLibraryContentControl SelectedControl => xtraTabControl.SelectedTabPage as IResetLibraryContentControl;

		public FormResetLibraryLinks()
		{
			InitializeComponent();

			var resetImagesControl = new ResetImagesControl();
			resetImagesControl.SelectionChanged += OnControlSelectionChanged;
			xtraTabControl.TabPages.Add(resetImagesControl);

			var resetInfoControl = new ResetInfoControl();
			resetInfoControl.SelectionChanged += OnControlSelectionChanged;
			xtraTabControl.TabPages.Add(resetInfoControl);

			var resetFontControl = new ResetFontControl(MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library);
			resetFontControl.SelectionChanged += OnControlSelectionChanged;
			xtraTabControl.TabPages.Add(resetFontControl);

			if (MainController.Instance.Settings.EditorSettings.EnableTagsEdit)
			{
				var searchTagsControl = new ResetTagsControl();
				searchTagsControl.SelectionChanged += OnControlSelectionChanged;
				xtraTabControl.TabPages.Add(searchTagsControl);
			}

			var resetLineBreaksControl = new ResetLineBreaksControl();
			resetLineBreaksControl.SelectionChanged += OnControlSelectionChanged;
			xtraTabControl.TabPages.Add(resetLineBreaksControl);

			var resetPreviewContentControl = new ResetPreviewContentControl();
			resetPreviewContentControl.SelectionChanged += OnControlSelectionChanged;
			xtraTabControl.TabPages.Add(resetPreviewContentControl);

			if (MainController.Instance.Settings.OneDriveSettings.Enabled)
			{
				var resetOneDriveLinksControl = new ResetOneDriveLinksControl();
				resetOneDriveLinksControl.SelectionChanged += OnControlSelectionChanged;
				xtraTabControl.TabPages.Add(resetOneDriveLinksControl);
			}

			var resetAllControl = new ResetAllControl();
			resetAllControl.SelectionChanged += OnControlSelectionChanged;
			xtraTabControl.TabPages.Add(resetAllControl);

			layoutControlItemToggleEnable.MaxSize = RectangleHelper.ScaleSize(layoutControlItemToggleEnable.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemToggleEnable.MinSize = RectangleHelper.ScaleSize(layoutControlItemToggleEnable.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemDisableToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDisableToggle.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemDisableToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemDisableToggle.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemReset.MaxSize = RectangleHelper.ScaleSize(layoutControlItemReset.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemReset.MinSize = RectangleHelper.ScaleSize(layoutControlItemReset.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void OnEnableClick(object sender, EventArgs e)
		{
			var button = (ButtonX)sender;
			if (button.Checked) return;
			buttonXEnable.Checked = false;
			buttonXDisable.Checked = false;
			button.Checked = true;
		}

		private void OnEnableCheckedChanged(object sender, EventArgs e)
		{
			var button = (ButtonX)sender;
			if (!button.Checked) return;
			layoutControlItemTabControl.Enabled = buttonXEnable.Checked;
			layoutControlItemReset.Enabled = buttonXEnable.Checked && SelectedControl != null && SelectedControl.SelectionMade;
		}

		private void OnControlSelectionChanged(object sender, EventArgs e)
		{
			layoutControlItemReset.Enabled = buttonXEnable.Checked && SelectedControl != null && SelectedControl.SelectionMade;
		}

		private void OnSelectedControlTabChanged(object sender, TabPageChangedEventArgs e)
		{
			OnControlSelectionChanged(sender, e);
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			if (SelectedControl == null) return;
			if (SelectedControl is IResetLibraryLinkSettingsControl)
			{
				var resetLinkSettingsControl = SelectedControl as IResetLibraryLinkSettingsControl;
				using (var confirmationForm = new FormResetLibraryLinksConfirmation(resetLinkSettingsControl.GetSelectedSettingsGroups()))
				{
					if (confirmationForm.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
					SelectedControl.ResetContent(MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library);
					MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
					MainController.Instance.ProcessChanges();
					MainController.Instance.ProcessManager.RunInQueue("Loading Library...",
						() =>
							MainController.Instance.MainForm.ActiveForm.Invoke(
								new MethodInvoker(MainController.Instance.TabWallbin.UpdateWallbin)));
				}
			}
			else
			{
				SelectedControl.ResetContent(MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library);
			}
		}
	}
}