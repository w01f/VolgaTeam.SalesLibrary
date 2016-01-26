using System;
using System.Drawing;
using System.Linq;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraTab;
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
			xtraTabControlWidgets.TabPages.AddRange(
				MainController.Instance.Lists.Widgets.Items.Select(imageGroup =>
				{
					var tabPage = BaseLinkImagesContainer.Create(imageGroup);
					tabPage.SelectedImageChanged += OnSelectedWidgetChanged;
					tabPage.OnImageDoubleClick += OnImageDoubleClick;
					return (XtraTabPage)tabPage;
				}).ToArray()
			);
			xtraTabControlWidgets.SelectedPageChanged += (o, e) => ((BaseLinkImagesContainer)e.Page).Init();
			((BaseLinkImagesContainer)xtraTabControlWidgets.SelectedTabPage).Init();

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