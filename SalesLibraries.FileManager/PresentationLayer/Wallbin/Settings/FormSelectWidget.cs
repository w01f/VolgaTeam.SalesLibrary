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
				var tabPage = BaseLinkImagesContainer.Create(imageGroup);
				tabPage.SelectedImageChanged += OnSelectedWidgetChanged;
				tabPage.OnImageDoubleClick += OnImageDoubleClick;
				xtraTabControlWidgets.TabPages.Add(tabPage);
			}

			if ((CreateGraphics()).DpiX > 96)
			{
				laWidgetDescription.Font = new Font(laWidgetDescription.Font.FontFamily, laWidgetDescription.Font.Size - 2, laWidgetDescription.Font.Style);
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