using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.Common.Objects.SearchTags;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Common
{
	class SearchGroupContainer
	{
		public SearchGroup DataSource { get; }
		public CheckedListBoxControl ListBox { get; private set; }
		public ButtonX ToggleButton { get; private set; }

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
			ListBox.CheckOnClick = true;
			ListBox.ItemHeight = 30;
			ListBox.SelectionMode = SelectionMode.None;
			ListBox.Dock = DockStyle.Fill;
			ListBox.Items.AddRange(DataSource.Tags.Select(t => new CheckedListBoxItem(t, t.Name, false)).ToArray());

			ToggleButton = new ButtonX();
			ToggleButton.AccessibleRole = AccessibleRole.PushButton;
			ToggleButton.ColorTable = eButtonColor.OrangeWithBackground;
			ToggleButton.Size = new Size(250, 30);
			ToggleButton.Style = eDotNetBarStyle.StyleManagerControlled;
			ToggleButton.Text = DataSource.Description.Replace("&", "&&");
			ToggleButton.TextColor = Color.Black;
			ToggleButton.TextAlignment = eButtonTextAlignment.Left;
			ToggleButton.Tag = ListBox;
		}
	}
}
