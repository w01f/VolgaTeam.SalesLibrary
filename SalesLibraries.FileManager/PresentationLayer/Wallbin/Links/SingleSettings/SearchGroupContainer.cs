using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.Common.Objects.SearchTags;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	class SearchGroupContainer
	{
		public SearchGroup DataSource { get; }
		public CheckedListBoxControl ListBox { get; private set; }

		public SearchGroupContainer(SearchGroup dataSource)
		{
			DataSource = dataSource;
			Init();
		}

		private void Init()
		{
			ListBox = new CheckedListBoxControl();
			ListBox.Appearance.Font = new Font("Arial", 9.75F, FontStyle.Regular);
			ListBox.Appearance.Options.UseFont = true;
			ListBox.BorderStyle = BorderStyles.NoBorder;
			ListBox.CheckOnClick = true;
			ListBox.ItemHeight = 30;
			ListBox.SelectionMode = SelectionMode.None;
			ListBox.Dock = DockStyle.Fill;
			ListBox.Items.AddRange(DataSource.Tags.Select(t => new CheckedListBoxItem(t, t.Name, false)).ToArray());
		}
	}
}
