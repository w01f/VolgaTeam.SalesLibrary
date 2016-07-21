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
			var selectedFolder = MainController.Instance.WallbinViews.Selection.SelectedFolder;
			var selectedLink = MainController.Instance.WallbinViews.Selection.SelectedLink;
			if (selectedFolder != null && selectedLink != null)
			{
				Enabled = true;
				foreach (CheckedListBoxItem item in checkedListBoxControl.Items)
					item.CheckState = selectedLink.Tags.SuperFilters.Contains(item.Value.ToString()) ? CheckState.Checked : CheckState.Unchecked;
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
			var selectedLink = MainController.Instance.WallbinViews.Selection.SelectedLink;
			selectedLink.Tags.SuperFilters.Clear();
			selectedLink.Tags.SuperFilters.AddRange(checkedListBoxControl.Items.OfType<CheckedListBoxItem>().Where(it => it.CheckState == CheckState.Checked).Select(it => it.Value.ToString()));
			selectedLink.MarkAsModified();
			EditorChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
