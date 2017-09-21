using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	public sealed partial class SuperFilterControl : CheckedListBoxControl
	{
		private bool _loading;

		public event EventHandler<EventArgs> EditorChanged;

		private bool AllowEdit => MainController.Instance.Lists.SuperFilters.Items.Any() && MainController.Instance.WallbinViews.FormatState.AllowEdit;

		public SuperFilterControl()
		{
			CheckOnClick = true;
			ItemHeight = (Int32)(23f * Utils.GetScaleFactor(CreateGraphics().DpiX).Height);
			MultiColumn = true;
			SelectionMode = SelectionMode.None;
			ItemCheck += OnItemCheck;
		}

		public void Init()
		{
			Items.AddRange(MainController.Instance.Lists.SuperFilters.Items.Cast<object>().ToArray());
		}

		public void UpdateData()
		{
			if (!AllowEdit)
			{
				Visible = false;
				return;
			}

			Reset();

			_loading = true;
			var selectedLinks = MainController.Instance.WallbinViews.Selection.SelectedLinks;
			if (selectedLinks.Any())
			{
				Enabled = true;
				foreach (CheckedListBoxItem item in Items)
				{
					var superFilter = item.Value.ToString();
					if (selectedLinks.All(link => link.Tags.SuperFilters.Any(tag => String.Equals(tag, superFilter, StringComparison.OrdinalIgnoreCase))))
						item.CheckState = CheckState.Checked;
					else if (selectedLinks.Any(link => link.Tags.SuperFilters.Any(tag => String.Equals(tag, superFilter, StringComparison.OrdinalIgnoreCase))))
						item.CheckState = CheckState.Indeterminate;
					else
						item.CheckState = CheckState.Unchecked;
				}
			}
			else
				Enabled = false;
			Visible = Enabled && AllowEdit;
			_loading = false;
		}

		public void Reset()
		{
			Enabled = false;
			Visible = false;
			if (!AllowEdit) return;
			_loading = true;
			UnCheckAll();
			_loading = false;
		}

		private void OnItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
		{
			if (_loading) return;
			var filtersToRemove =
				Items
					.OfType<CheckedListBoxItem>()
					.Where(it => it.CheckState != CheckState.Indeterminate)
					.Select(it => it.Value.ToString());
			var filtersToAdd =
				Items
					.OfType<CheckedListBoxItem>()
					.Where(it => it.CheckState == CheckState.Checked)
					.Select(it => it.Value.ToString());
			foreach (var link in MainController.Instance.WallbinViews.Selection.SelectedLinks)
			{
				link.Tags.SuperFilters.RemoveAll(
					item => filtersToRemove.Any(filter => String.Equals(filter, item, StringComparison.OrdinalIgnoreCase)));
				link.Tags.SuperFilters.AddRange(filtersToAdd);
				link.MarkAsModified();
			}
			EditorChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
