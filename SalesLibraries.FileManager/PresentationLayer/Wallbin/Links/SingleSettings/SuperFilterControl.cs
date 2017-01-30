using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	public partial class SuperFilterControl : UserControl
	{
		private bool _loading;

		public event EventHandler<EventArgs> EditorChanged;

		private bool AllowEdit => MainController.Instance.Lists.SuperFilters.Items.Any() && MainController.Instance.WallbinViews.FormatState.AllowEdit;

		public SuperFilterControl()
		{
			InitializeComponent();
		}

		public void Init()
		{
			Visible = MainController.Instance.Lists.SuperFilters.Items.Any();
			checkedListBoxControl.Items.Clear();
			checkedListBoxControl.Items.AddRange(MainController.Instance.Lists.SuperFilters.Items.Cast<object>().ToArray());
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
				foreach (CheckedListBoxItem item in checkedListBoxControl.Items)
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
			checkedListBoxControl.UnCheckAll();
			_loading = false;
		}

		private void checkedListBoxControl_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
		{
			if (_loading) return;
			var filtersToRemove =
				checkedListBoxControl.Items
					.OfType<CheckedListBoxItem>()
					.Where(it => it.CheckState != CheckState.Indeterminate)
					.Select(it => it.Value.ToString());
			var filtersToAdd =
				checkedListBoxControl.Items
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
