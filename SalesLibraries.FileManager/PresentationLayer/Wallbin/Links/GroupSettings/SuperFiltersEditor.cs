﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Common.Extensions;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Views;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.GroupSettings
{
	[ToolboxItem(false)]
	public sealed partial class SuperFiltersEditor : UserControl, IGroupSettingsEditor
	{
		private bool _loading;

		private SelectionManager Selection
		{
			get { return MainController.Instance.WallbinViews.Selection; }
		}

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
		public string Title
		{
			get { return "Manage Super Filters"; }
		}
		public bool NeedToApply { get; set; }

		public event EventHandler<EventArgs> EditorChanged;

		public void UpdateData()
		{
			pnButtons.Enabled = false;
			pnData.Enabled = false;
			checkedListBoxControl.UnCheckAll();
			Enabled = false;

			var defaultLink = Selection.SelectedLinks.FirstOrDefault(link => link.Tags.HasSuperFilters) ?? Selection.SelectedLinks.FirstOrDefault();
			Enabled = defaultLink != null;
			if (defaultLink == null) return;

			var noData = Selection.SelectedLinks.All(link => !link.Tags.SuperFilters.Any());
			var sameData = Selection.SelectedLinks.All(link => link.Tags.SuperFilters.Compare(defaultLink.Tags.SuperFilters));

			pnButtons.Enabled = !noData;
			pnData.Enabled = sameData || noData;

			if (!sameData) return;
			_loading = true;
			foreach (var item in checkedListBoxControl.Items.OfType<CheckedListBoxItem>())
				item.CheckState = defaultLink.Tags.SuperFilters.Contains(item.Value.ToString()) ? CheckState.Checked : CheckState.Unchecked;
			_loading = false;
		}

		public void ApplyData()
		{
			Selection.SelectedLinks.ApplySuperFilters(checkedListBoxControl.Items.OfType<CheckedListBoxItem>().Where(it => it.CheckState == CheckState.Checked).Select(it => it.Value.ToString()).ToArray());
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());
		}

		public void ResetData()
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure You want to DELETE ALL Super Filters for the selected files?") != DialogResult.Yes) return;
			Selection.SelectedLinks.ApplySuperFilters(new string[] { });
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());

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
				NeedToApply = true;
		}
	}
}