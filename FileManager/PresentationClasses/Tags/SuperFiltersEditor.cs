using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using FileManager.ConfigurationClasses;
using FileManager.Controllers;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.Tags
{
	[ToolboxItem(false)]
	public partial class SuperFiltersEditor : UserControl, ITagsEditor
	{
		private bool _loading;

		public SuperFiltersEditor()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			checkedListBoxControl.Items.Clear();
			checkedListBoxControl.Items.AddRange(ListManager.Instance.SuperFilters.Select(sf => sf.Name).ToArray());

			if (!((CreateGraphics()).DpiX > 96)) return;
			var styleControllerFont = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
			styleController.AppearanceDisabled.Font = styleControllerFont;
			styleController.AppearanceDropDown.Font = styleControllerFont;
			styleController.AppearanceDropDownHeader.Font = styleControllerFont;
			styleController.AppearanceFocused.Font = styleControllerFont;
			styleController.AppearanceReadOnly.Font = styleControllerFont;
			buttonXReset.Font = new Font(buttonXReset.Font.FontFamily, buttonXReset.Font.Size - 2, buttonXReset.Font.Style);
		}

		#region ITagsEditor Members
		private bool _needToApply;
		public bool NeedToApply
		{
			get { return _needToApply; }
			set
			{
				_needToApply = value;
				var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
				if (activePage != null) activePage.Parent.StateChanged = true;
			}
		}
		public event EventHandler<EventArgs> EditorChanged;

		public void UpdateData()
		{
			pnButtons.Enabled = false;
			pnData.Enabled = false;
			checkedListBoxControl.UnCheckAll();
			Enabled = false;

			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;
			var defaultLink = activePage.SelectedLinks.FirstOrDefault();
			Enabled = defaultLink != null;
			if (defaultLink == null) return;

			var noData = activePage.SelectedLinks.All(x => !x.SuperFilters.Any());
			var sameData = defaultLink != null && activePage.SelectedLinks.All(x => x.CustomKeywords.Compare(defaultLink.CustomKeywords));

			pnButtons.Enabled = !noData;
			pnData.Enabled = sameData || noData;

			if (sameData)
				foreach (var item in checkedListBoxControl.Items.OfType<CheckedListBoxItem>())
					item.CheckState = defaultLink.SuperFilters.Select(sf => sf.Name).Contains(item.Value.ToString()) ? CheckState.Checked : CheckState.Unchecked;
		}

		public void ApplyData()
		{
			var activePage = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.ActivePage : null;
			if (activePage == null) return;

			foreach (var link in activePage.SelectedLinks)
			{
				link.SuperFilters.Clear();
				link.SuperFilters.AddRange(checkedListBoxControl.Items.OfType<CheckedListBoxItem>().Where(it => it.CheckState == CheckState.Checked).Select(it => new SuperFilter() { Name = it.Value.ToString() }));
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
			if (AppManager.Instance.ShowWarningQuestion("Are you sure You want to DELETE ALL Super Filters for the selected files?") != DialogResult.Yes) return;
			foreach (var link in activePage.SelectedLinks)
				link.SuperFilters.Clear();
			activePage.Parent.StateChanged = true;
			activePage.RefreshSelectedLinks();
			if (EditorChanged != null)
				EditorChanged(this, new EventArgs());

			UpdateData();
		}

		private void checkedListBoxControl_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
		{
			if (!_loading)
				NeedToApply = true;
		}
	}
}