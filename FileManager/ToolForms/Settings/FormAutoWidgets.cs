using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors.Controls;
using FileManager.ToolForms.WallBin;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.ToolForms.Settings
{
	public partial class FormAutoWidgets : MetroForm
	{
		public FormAutoWidgets()
		{
			InitializeComponent();
		}

		public Library Library { get; set; }

		private void FormApplicationSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				foreach (LibraryPage page in Library.Pages)
					page.LastChanged = DateTime.Now;
				Library.Save();
			}
		}

		private void FormApplicationSettings_Load(object sender, EventArgs e)
		{
			gridControlAutoWidgets.DataSource = new BindingList<AutoWidget>(Library.AutoWidgets);
		}

		private void repositoryItemButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (gridViewAutoWidgets.FocusedRowHandle >= 0)
			{
				if (e.Button.Index == 0)
				{
					Library.AutoWidgets.RemoveAt(gridViewAutoWidgets.GetDataSourceRowIndex(gridViewAutoWidgets.FocusedRowHandle));
					(gridControlAutoWidgets.DataSource as BindingList<AutoWidget>).ResetBindings();
				}
				else if (e.Button.Index == 1)
				{
					using (var form = new FormSelectWidget())
					{
						AutoWidget autoWidget = Library.AutoWidgets[gridViewAutoWidgets.GetDataSourceRowIndex(gridViewAutoWidgets.FocusedRowHandle)];
						form.pbSelectedWidget.Image = autoWidget.Widget;
						if (form.ShowDialog() == DialogResult.OK)
						{
							autoWidget.Widget = form.pbSelectedWidget.Image;
							(gridControlAutoWidgets.DataSource as BindingList<AutoWidget>).ResetBindings();
						}
					}
				}
			}
		}

		private void buttonEditNewExtension_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				buttonEditNewExtension_ButtonClick(null, null);
		}

		private void buttonEditNewExtension_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (buttonEditNewExtension.EditValue != null)
			{
				if (!Library.AutoWidgets.Any(x => x.Extension.ToLower().Equals(buttonEditNewExtension.EditValue.ToString().ToLower())))
				{
					var autoWidget = new AutoWidget();
					autoWidget.Extension = buttonEditNewExtension.EditValue.ToString().ToLower();
					Library.AutoWidgets.Add(autoWidget);
					(gridControlAutoWidgets.DataSource as BindingList<AutoWidget>).ResetBindings();
					buttonEditNewExtension.EditValue = null;
					gridViewAutoWidgets.Focus();
				}
				else
					AppManager.Instance.ShowWarning("Auto Widter is existed for this extension already");
			}
		}
	}
}