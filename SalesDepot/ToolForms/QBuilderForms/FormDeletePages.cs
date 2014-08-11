using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors.Controls;
using SalesDepot.Services.QBuilderService;

namespace SalesDepot.ToolForms.QBuilderForms
{
	public partial class FormDeletePages : MetroForm
	{
		public IEnumerable<string> SelectedPageIds
		{
			get
			{
				var result = new List<string>();
				foreach (CheckedListBoxItem item in checkedListBoxControl.Items)
					if (item.CheckState == CheckState.Checked)
						result.Add(item.Value.ToString());
				return result;
			}
		}

		public FormDeletePages()
		{
			InitializeComponent();
		}

		public void Init(IEnumerable<QPageModel> _pages)
		{
			checkedListBoxControl.Items.Clear();
			foreach (var page in _pages)
				checkedListBoxControl.Items.Add(page.id, page.title, CheckState.Unchecked, true);
		}

		private void simpleButtonSelectAll_Click(object sender, EventArgs e)
		{
			checkedListBoxControl.CheckAll();
		}

		private void simpleButtonClearAll_Click(object sender, EventArgs e)
		{
			checkedListBoxControl.UnCheckAll();
		}
	}
}
