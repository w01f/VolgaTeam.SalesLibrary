using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Extensions;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Views;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.GroupSettings
{
	[ToolboxItem(false)]
	public sealed partial class SuperFiltersEditor : UserControl, IGroupSettingsEditor
	{
		private bool _loading;

		private SelectionManager Selection => MainController.Instance.WallbinViews.Selection;

		public SuperFiltersEditor()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			checkedListBoxControl.Items.Clear();
			checkedListBoxControl.Items.AddRange(MainController.Instance.Lists.SuperFilters.Items.ToArray());

			if (!((CreateGraphics()).DpiX > 96)) return;
			var styleControllerFont = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
			styleController.AppearanceDisabled.Font = styleControllerFont;
			styleController.AppearanceDropDown.Font = styleControllerFont;
			styleController.AppearanceDropDownHeader.Font = styleControllerFont;
			styleController.AppearanceFocused.Font = styleControllerFont;
			styleController.AppearanceReadOnly.Font = styleControllerFont;
			buttonXReset.Font = new Font(buttonXReset.Font.FontFamily, buttonXReset.Font.Size - 2, buttonXReset.Font.Style);
		}

		#region IGroupSettingsEditor Members
		public string Title => "Manage Super Filters";

		public event EventHandler<EventArgs> EditorChanged;

		public void UpdateData()
		{
			pnButtons.Enabled = false;
			pnData.Enabled = false;

			_loading = true;
			checkedListBoxControl.UnCheckAll();
			_loading = false;

			Enabled = false;

			var defaultLink = Selection.SelectedFiles.OfType<LibraryObjectLink>().FirstOrDefault(link => link.Tags.HasSuperFilters) ?? Selection.SelectedFiles.FirstOrDefault();
			Enabled = defaultLink != null;
			if (defaultLink == null) return;

			var noData = Selection.SelectedFiles.All(link => !link.Tags.SuperFilters.Any());
			var sameData = Selection.SelectedFiles.All(link => link.Tags.SuperFilters.Compare(defaultLink.Tags.SuperFilters));

			pnButtons.Enabled = !noData;
			pnData.Enabled = sameData || noData;

			if (!sameData) return;
			_loading = true;
			foreach (var item in checkedListBoxControl.Items.OfType<CheckedListBoxItem>())
				item.CheckState = defaultLink.Tags.SuperFilters.Contains(item.Value.ToString()) ? CheckState.Checked : CheckState.Unchecked;
			_loading = false;
		}

		private void ApplyData()
		{
			Selection.SelectedFiles.ApplySuperFilters(checkedListBoxControl.Items.OfType<CheckedListBoxItem>().Where(it => it.CheckState == CheckState.Checked).Select(it => it.Value.ToString()).ToArray());
			EditorChanged?.Invoke(this, new EventArgs());
		}

		public void ResetData()
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure You want to DELETE ALL Super Filters for the selected files?") != DialogResult.Yes) return;
			Selection.SelectedFiles.ApplySuperFilters(new string[] { });
			EditorChanged?.Invoke(this, new EventArgs());

			UpdateData();
		}
		#endregion

		private void buttonXReset_Click(object sender, EventArgs e)
		{
			ResetData();
		}

		private void checkedListBoxControl_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
		{
			if (!_loading)
				ApplyData();
		}
	}
}