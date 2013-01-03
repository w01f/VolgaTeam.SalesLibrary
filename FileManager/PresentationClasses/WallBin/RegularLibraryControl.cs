using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using FileManager.Controllers;
using FileManager.PresentationClasses.WallBin.Decorators;

namespace FileManager.PresentationClasses.WallBin
{
	[ToolboxItem(false)]
	public partial class RegularLibraryControl : UserControl
	{
		private readonly List<PageDecorator> _pages = new List<PageDecorator>();

		public RegularLibraryControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		public void Init(PageDecorator[] pages)
		{
			_pages.Clear();
			_pages.AddRange(pages);
			Controls.Clear();
			Controls.AddRange(_pages.Select(x => x.RegularPage).ToArray());
		}
	}
}