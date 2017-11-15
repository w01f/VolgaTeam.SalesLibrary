using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.Skins;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings.ResetLinks
{
	//public partial class ResetLineBreaksControl : UserControl, IResetLibraryContentControl
	public partial class ResetLineBreaksControl : XtraTabPage, IResetLibraryContentControl
	{
		public bool SelectionMade =>
				(buttonXReset.Checked && (checkEditWidget.Checked || checkEditBanner.Checked || checkEditHoverNote.Checked || checkEditText.Checked)) || buttonXDelete.Checked;

		public event EventHandler<EventArgs> SelectionChanged;

		public ResetLineBreaksControl()
		{
			InitializeComponent();

			Text = "Line-Breaks";

			layoutControlGroupSettings.Enabled = false;

			layoutControlItemReset.MaxSize = RectangleHelper.ScaleSize(layoutControlItemReset.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemReset.MinSize = RectangleHelper.ScaleSize(layoutControlItemReset.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemDelete.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDelete.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemDelete.MinSize = RectangleHelper.ScaleSize(layoutControlItemDelete.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		public void ResetContent(Library library)
		{
			if (buttonXReset.Checked)
			{
				var selectedLinkSettingsGroupForReset = new List<LinkSettingsGroupType>();
				if (checkEditWidget.Checked)
					selectedLinkSettingsGroupForReset.Add(LinkSettingsGroupType.Widgets);
				if (checkEditBanner.Checked)
					selectedLinkSettingsGroupForReset.Add(LinkSettingsGroupType.Banners);
				if (checkEditHoverNote.Checked)
					selectedLinkSettingsGroupForReset.Add(LinkSettingsGroupType.HoverNote);
				if (checkEditText.Checked)
					selectedLinkSettingsGroupForReset.Add(LinkSettingsGroupType.TextNote);

				using (var confirmationForm = new FormResetLineBreaksConfirmation(selectedLinkSettingsGroupForReset))
				{
					if (confirmationForm.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;

					library.ResetLinksToDefault(selectedLinkSettingsGroupForReset, link => link.Type == LinkType.LineBreak);

					MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
					MainController.Instance.ProcessChanges();
					MainController.Instance.ProcessManager.RunInQueue("Loading Library...",
						() =>
							MainController.Instance.MainForm.ActiveForm.Invoke(
								new MethodInvoker(MainController.Instance.TabWallbin.UpdateWallbin)));
				}
			}
			else if (buttonXDelete.Checked)
			{
				if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to delete ALL Line-Breaks?") != DialogResult.Yes) return;

				var lineBreaks = library.Pages
					.SelectMany(p => p.TopLevelLinks)
					.Where(link => link.Type == LinkType.LineBreak)
					.ToList();
				foreach (var lineBreak in lineBreaks)
					lineBreak.DeleteLink();

				MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
				MainController.Instance.ProcessChanges();
				MainController.Instance.ProcessManager.RunInQueue("Loading Library...",
						() =>
							MainController.Instance.MainForm.ActiveForm.Invoke(
								new MethodInvoker(MainController.Instance.TabWallbin.UpdateWallbin)));
			}
		}

		private void OnActionTypeClick(object sender, EventArgs e)
		{
			var button = (ButtonX)sender;
			var isButtonChecked = button.Checked;
			buttonXReset.Checked = false;
			buttonXDelete.Checked = false;
			if (!isButtonChecked)
				button.Checked = true;
		}

		private void OnActionTypeChanged(object sender, EventArgs e)
		{
			layoutControlGroupSettings.Enabled = buttonXReset.Checked;
			SelectionChanged?.Invoke(sender, e);
		}

		private void OnResetedSettingSelectionChanged(object sender, EventArgs e)
		{
			SelectionChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
