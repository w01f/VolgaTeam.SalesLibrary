using System;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Common.Extensions;
using SalesLibraries.CloudAdmin.Controllers;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Settings
{
	public partial class FormAutoWidgets : MetroForm
	{
		public FormAutoWidgets()
		{
			InitializeComponent();
		}

		public Library Library { get; set; }

		private void LoadData()
		{
			gridControlAutoWidgets.DataSource = Library.Settings.AutoWidgets;
			gridViewAutoWidgets.RefreshData();
			gridViewAutoWidgets.Focus();
		}

		private void FormApplicationSettings_Load(object sender, EventArgs e)
		{
			LoadData();
		}

		private void FormAutoWidgets_FormClosed(object sender, FormClosedEventArgs e)
		{
			gridViewAutoWidgets.CloseEditor();
		}

		private void repositoryItemButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			var autoWidget = gridViewAutoWidgets.GetFocusedRow() as AutoWidget;
			if (autoWidget == null) return;
			switch (e.Button.Index)
			{
				case 0:
					Library.Settings.AutoWidgets.Remove(autoWidget);
					LoadData();
					break;
				case 1:
					using (var form = new FormSelectWidget())
					{
						form.laWidgetDescription.Text = autoWidget.Extension.ToUpper();
						form.checkEditInvert.Checked = autoWidget.Inverted;
						form.colorEditInversionColor.Color = autoWidget.InversionColor;
						form.OriginalImage = autoWidget.Widget;
						form.OriginalImageName = autoWidget.WidgetName;
						if (form.ShowDialog() != DialogResult.OK) return;
						autoWidget.Inverted = form.checkEditInvert.Checked;
						autoWidget.InversionColor = form.checkEditInvert.Checked ? form.colorEditInversionColor.Color : GraphicObjectExtensions.DefaultInversionColor;
						autoWidget.Widget = form.OriginalImage;
						autoWidget.WidgetName = form.OriginalImageName;
						gridViewAutoWidgets.UpdateCurrentRow();
						gridViewAutoWidgets.RefreshData();
						gridViewAutoWidgets.Focus();
					}
					break;
			}
		}

		private void buttonEditNewExtension_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				buttonEditNewExtension_ButtonClick(null, null);
		}

		private void buttonEditNewExtension_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			var newExtension = buttonEditNewExtension.EditValue as String;
			if (String.IsNullOrEmpty(newExtension))
			{
				MainController.Instance.PopupMessages.ShowWarning("Extension not set");
				return;
			}
			newExtension = newExtension.Trim().ToLower();
			if (Library.Settings.AutoWidgets.Any(widget => widget.Extension.Equals(newExtension)))
			{
				MainController.Instance.PopupMessages.ShowWarning("Auto Widter is existed for this extension already");
				return;
			}
			var autoWidget = new AutoWidget();
			autoWidget.Extension = newExtension;
			Library.Settings.AutoWidgets.Add(autoWidget);
			buttonEditNewExtension.EditValue = null;
			LoadData();
		}
	}
}