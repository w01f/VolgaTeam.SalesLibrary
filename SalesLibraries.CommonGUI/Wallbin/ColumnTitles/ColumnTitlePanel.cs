using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.Persistent;

namespace SalesLibraries.CommonGUI.Wallbin.ColumnTitles
{
	[ToolboxItem(false)]
	public partial class ColumnTitlePanel : UserControl
	{
		private LibraryPage _page;

		public int PanelBottom => Bottom;

		public ColumnTitlePanel(LibraryPage page)
		{
			InitializeComponent();
			_page = page;
			LoadData();
		}

		private void LoadData()
		{
			if (_page.Settings.EnableColumnTitles)
			{
				foreach (var columnTitle in _page.ColumnTitles)
				{
					var columnTitleControl = new ColumnTitleControl(columnTitle)
					{
						Dock = columnTitle.ColumnOrder == 2 ? DockStyle.Fill : DockStyle.Left
					};
					Controls.Add(columnTitleControl);
					columnTitleControl.BringToFront();
					Application.DoEvents();
				}
				UpdateSize();
			}
			else
				Height = 0;
		}

		public new void Dispose()
		{
			_page = null;
			base.Dispose(true);
		}

		public void UpdateSize()
		{
			if (!_page.Settings.EnableColumnTitles) return;
			var panelWidth = Width / 3;
			var columTitles = Controls.OfType<ColumnTitleControl>().ToList();
			foreach (var columnTitleControl in columTitles)
				columnTitleControl.Width = panelWidth;
			Height = columTitles.Max(columnTitleControl => columnTitleControl.GetHeight());
		}
	}
}
