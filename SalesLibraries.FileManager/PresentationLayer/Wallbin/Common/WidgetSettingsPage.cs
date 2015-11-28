using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Common
{
	[ToolboxItem(false)]
	public partial class WidgetSettingsPage : XtraTabPage
	{
		public WidgetSettingsControl DataControl { get; private set; }

		public WidgetSettingsPage(WidgetSettings data)
		{
			InitializeComponent();
			Disposed += OnDisposed;
			DataControl = new WidgetSettingsControl(data);
			Controls.Add(DataControl);
			DataControl.Dock = DockStyle.Fill;
		}

		private void OnDisposed(object sender, EventArgs e)
		{
			DataControl.Dispose();
		}
	}
}
