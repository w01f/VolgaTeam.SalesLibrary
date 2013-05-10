using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FileManager.ConfigurationClasses;
using FileManager.Controllers;

namespace FileManager.PresentationClasses.Tags
{
	[ToolboxItem(false)]
	public partial class SecurityEditor : UserControl, ITagsEditor
	{
		public SecurityEditor()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			memoEditSecurityUsers.Enter += FormMain.Instance.EditorEnter;
			memoEditSecurityUsers.MouseUp += FormMain.Instance.EditorMouseUp;
			memoEditSecurityUsers.MouseDown += FormMain.Instance.EditorMouseUp;

			if (!((CreateGraphics()).DpiX > 96)) return;
			var styleControllerFont = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
			styleController.AppearanceDisabled.Font = styleControllerFont;
			styleController.AppearanceDropDown.Font = styleControllerFont;
			styleController.AppearanceDropDownHeader.Font = styleControllerFont;
			styleController.AppearanceFocused.Font = styleControllerFont;
			styleController.AppearanceReadOnly.Font = styleControllerFont;
			buttonXReset.Font = new Font(buttonXReset.Font.FontFamily, buttonXReset.Font.Size - 2, buttonXReset.Font.Style);
			rbSecurityAllowed.Font = new Font(rbSecurityAllowed.Font.FontFamily, rbSecurityAllowed.Font.Size - 2, rbSecurityAllowed.Font.Style);
			rbSecurityDenied.Font = new Font(rbSecurityDenied.Font.FontFamily, rbSecurityDenied.Font.Size - 2, rbSecurityDenied.Font.Style);
			rbSecurityRestricted.Font = new Font(rbSecurityRestricted.Font.FontFamily, rbSecurityRestricted.Font.Size - 2, rbSecurityRestricted.Font.Style);
			ckSecurityShareLink.Font = new Font(ckSecurityShareLink.Font.FontFamily, ckSecurityShareLink.Font.Size - 2, ckSecurityShareLink.Font.Style);
		}

		#region ITagsEditor Members
		public event EventHandler<EventArgs> EditorChanged;

		public void UpdateData()
		{
			pnButtons.Enabled = false;
			pnData.Enabled = false;
			rbSecurityAllowed.Checked = true;
			ckSecurityShareLink.Checked = true;
			memoEditSecurityUsers.EditValue = !string.IsNullOrEmpty(SettingsManager.Instance.DefaultLinkUsers) ? SettingsManager.Instance.DefaultLinkUsers : null;
			Enabled = false;

			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			var defaultLink = activePage.SelectedLinks.FirstOrDefault();
			Enabled = defaultLink != null;
			if (defaultLink == null) return;

			var noData = activePage.SelectedLinks.All(x => !x.IsRestricted && !x.NoShare);
			var sameData = defaultLink != null && activePage.SelectedLinks.All(x => x.IsRestricted = defaultLink.IsRestricted && x.AssignedUsers == defaultLink.AssignedUsers && x.NoShare == defaultLink.NoShare);

			pnButtons.Enabled = !noData;
			pnData.Enabled = sameData || noData;

			if (sameData)
			{
				rbSecurityAllowed.Checked = !defaultLink.IsRestricted;
				rbSecurityDenied.Checked = defaultLink.IsRestricted && string.IsNullOrEmpty(defaultLink.AssignedUsers);
				rbSecurityRestricted.Checked = defaultLink.IsRestricted && !string.IsNullOrEmpty(defaultLink.AssignedUsers);
				ckSecurityShareLink.Checked = defaultLink.NoShare;
				memoEditSecurityUsers.EditValue = defaultLink.IsRestricted && !string.IsNullOrEmpty(defaultLink.AssignedUsers) ? defaultLink.AssignedUsers : (!string.IsNullOrEmpty(SettingsManager.Instance.DefaultLinkUsers) ? SettingsManager.Instance.DefaultLinkUsers : null);
			}

		}

		public void ApplyData()
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;

			foreach (var link in activePage.SelectedLinks)
			{
				link.IsRestricted = rbSecurityDenied.Checked || rbSecurityRestricted.Checked;
				link.NoShare = !ckSecurityShareLink.Checked;
				if (rbSecurityRestricted.Checked && memoEditSecurityUsers.EditValue != null && !string.IsNullOrEmpty(memoEditSecurityUsers.EditValue.ToString().Trim()))
				{
					link.AssignedUsers = memoEditSecurityUsers.EditValue.ToString().Trim();
					SettingsManager.Instance.DefaultLinkUsers = link.AssignedUsers;
					SettingsManager.Instance.Save();
				}
				else
					link.AssignedUsers = null;
			}

			activePage.Parent.StateChanged = true;
			activePage.RefreshSelectedLinks();
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());
		}
		#endregion

		private void buttonXReset_Click(object sender, EventArgs e)
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Are you sure You want to DELETE ALL KEYWORD TAGS for the selected files?") != DialogResult.Yes) return;
			foreach (var link in activePage.SelectedLinks)
			{
				link.NoShare = false;
				link.IsRestricted = false;
				link.AssignedUsers = null;
			}
			activePage.Parent.StateChanged = true;
			activePage.RefreshSelectedLinks();
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());

			UpdateData();
		}

		private void rbSecurityRestricted_CheckedChanged(object sender, EventArgs e)
		{
			memoEditSecurityUsers.Enabled = rbSecurityRestricted.Checked;
		}
	}
}