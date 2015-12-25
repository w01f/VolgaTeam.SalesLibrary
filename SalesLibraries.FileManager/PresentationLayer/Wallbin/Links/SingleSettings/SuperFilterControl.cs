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
		public SuperFilterControl()
		{
			InitializeComponent();
		}

		public void Init()
		{
			Visible = MainController.Instance.Lists.SuperFilters.Items.Any();
			checkedListBoxControl.Items.Clear();
			checkedListBoxControl.Items.AddRange(MainController.Instance.Lists.SuperFilters.Items.Cast<object>().ToArray());
			UpdateData();
		}
		
		public void UpdateData()
		{
			_loading = true;
			checkedListBoxControl.UnCheckAll();
			var selectedFolder = MainController.Instance.WallbinViews.Selection.SelectedFolder;
			var selectedLink = MainController.Instance.WallbinViews.Selection.SelectedLink;
			if (selectedFolder!= null && selectedLink != null)
			{
				Enabled = true;
				foreach (CheckedListBoxItem item in checkedListBoxControl.Items)
					item.CheckState = selectedLink.Tags.SuperFilters.Contains(item.Value.ToString()) ? CheckState.Checked : CheckState.Unchecked;
			}
			else
				Enabled = false;
			Visible = MainController.Instance.Lists.SuperFilters.Items.Any() && Enabled && MainController.Instance.WallbinViews.FormatState.AllowEdit;
			_loading = false;
		}

		private void checkedListBoxControl_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
		{
			if (_loading) return;
			var selectedLink = MainController.Instance.WallbinViews.Selection.SelectedLink;
			selectedLink.Tags.SuperFilters.Clear();
			selectedLink.Tags.SuperFilters.AddRange(checkedListBoxControl.Items.OfType<CheckedListBoxItem>().Where(it => it.CheckState == CheckState.Checked).Select(it => it.Value.ToString()));
			if (EditorChanged != null)
				EditorChanged(this, EventArgs.Empty);
		}
	}
}
