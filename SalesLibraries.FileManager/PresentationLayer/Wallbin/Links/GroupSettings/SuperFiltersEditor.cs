using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Common.Helpers;
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

			layoutControlItemReset.MinSize = RectangleHelper.ScaleSize(layoutControlItemReset.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemReset.MaxSize = RectangleHelper.ScaleSize(layoutControlItemReset.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			checkedListBoxControl.ItemHeight = (Int32)(checkedListBoxControl.ItemHeight * Utils.GetScaleFactor(CreateGraphics().DpiX).Height);
		}

		#region IGroupSettingsEditor Members
		public string Title => "Manage Super Filters";

		public event EventHandler<EventArgs> EditorChanged;

		public void UpdateData()
		{
			_loading = true;
			checkedListBoxControl.UnCheckAll();

			Enabled = Selection.SelectedObjects.Any();

			var commonFilters = Selection.SelectedObjects.GetCommonSuperFilters().ToList();
			var allFilters = Selection.SelectedObjects.SelectMany(l => l.Tags.SuperFilters).ToList();

			foreach (var item in checkedListBoxControl.Items.OfType<CheckedListBoxItem>())
			{
				if (commonFilters.Contains(item.Value.ToString()))
					item.CheckState = CheckState.Checked;
				else
				{
					item.CheckState = allFilters.Contains(item.Value.ToString())
						? CheckState.Checked
						: CheckState.Unchecked;
				}
			}
			_loading = false;
		}

		private void ApplyData()
		{
			Selection.SelectedObjects.ApplySuperFilters(checkedListBoxControl.Items.OfType<CheckedListBoxItem>().Where(it => it.CheckState == CheckState.Checked).Select(it => it.Value.ToString()).ToArray());
			EditorChanged?.Invoke(this, new EventArgs());
		}

		public void ResetData()
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure You want to DELETE ALL Super Filters for the selected files?") != DialogResult.Yes) return;
			Selection.SelectedObjects.ApplySuperFilters(new string[] { });
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