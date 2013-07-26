using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using FileManager.ConfigurationClasses;
using FileManager.Controllers;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.WallBin
{
	public partial class SuperFilterControl : UserControl
	{
		private bool _loading = false;
		public LibraryLink SelectedLink { get; set; }

		public SuperFilterControl()
		{
			InitializeComponent();
		}

		public void Init()
		{
			Visible = ListManager.Instance.SuperFilters.Any();
			checkedListBoxControl.Items.Clear();
			checkedListBoxControl.Items.AddRange(ListManager.Instance.SuperFilters.Select(sf => sf.Name).ToArray());
		}

		public void UpdateLink()
		{
			_loading = true;
			checkedListBoxControl.UnCheckAll();
			checkedListBoxControl.Enabled = false;
			if (SelectedLink != null && SelectedLink.Type != FileTypes.LineBreak)
			{
				checkedListBoxControl.Enabled = true;
				foreach (CheckedListBoxItem item in checkedListBoxControl.Items)
					item.CheckState = SelectedLink.SuperFilters.Select(sf => sf.Name).Contains(item.Value.ToString()) ? CheckState.Checked : CheckState.Unchecked;
			}
			_loading = false;
		}

		private void checkedListBoxControl_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
		{
			if (!_loading)
			{
				SelectedLink.SuperFilters.Clear();
				SelectedLink.SuperFilters.AddRange(checkedListBoxControl.Items.OfType<CheckedListBoxItem>().Where(it => it.CheckState == CheckState.Checked).Select(it => new SuperFilter { Name = it.Value.ToString() }));
				MainController.Instance.ActiveDecorator.StateChanged = true;
			}
		}
	}
}
