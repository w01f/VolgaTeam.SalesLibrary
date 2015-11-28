using System;
using System.Drawing;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Common;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings
{
	public partial class FormSelectWidget : MetroForm
	{
		public FormSelectWidget()
		{
			InitializeComponent();
			xtraTabControlWidgets.TabPages.Clear();
			foreach (var imageGroup in MainController.Instance.Lists.Widgets.Items)
			{
				var tabPage = new LinkImagesContainer(imageGroup);
				tabPage.SelectedImageChanged += OnSelectedWidgetChanged;
				tabPage.OnImageDoubleClick += OnImageDoubleClick;
				xtraTabControlWidgets.TabPages.Add(tabPage);
			}

			if ((CreateGraphics()).DpiX > 96)
			{
				laAvailableWidgets.Font = new Font(laAvailableWidgets.Font.FontFamily, laAvailableWidgets.Font.Size - 2, laAvailableWidgets.Font.Style);
				laSelectedWidget.Font = new Font(laSelectedWidget.Font.FontFamily, laSelectedWidget.Font.Size - 2, laSelectedWidget.Font.Style);
			}
		}

		private void OnImageDoubleClick(object sender, EventArgs e)
		{
			Close();
		}

		private void OnSelectedWidgetChanged(object sender, LinkImageEventArgs e)
		{
			pbSelectedWidget.Image = e.Image;
		}
	}
}