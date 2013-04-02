using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using SalesDepot.Services.TickerService;

namespace SalesDepot.SiteManager.PresentationClasses.Ticker.LinkEditors
{
	[ToolboxItem(false)]
	public partial class LibraryLinkEditor : UserControl, ITickerEditor
	{
		public string LinkType { get; set; }

		public KeyValuePair[] Details
		{
			get
			{
				var details = new List<KeyValuePair>();
				details.AddRange(new[]
				{
					new KeyValuePair { tag = "library", data = textEditLibraryValue.EditValue != null ? textEditLibraryValue.EditValue.ToString() : null },
					new KeyValuePair { tag = "page", data = textEditPageValue.EditValue != null ? textEditPageValue.EditValue.ToString() : null },
					new KeyValuePair { tag = "link", data = textEditLinkValue.EditValue != null ? textEditLinkValue.EditValue.ToString() : null }
				});
				return details.ToArray();
			}
			set
			{
				foreach (var keyValuePair in value)
				{
					switch (keyValuePair.tag)
					{
						case "library":
							textEditLibraryValue.EditValue = keyValuePair.data;
							break;
						case "page":
							textEditPageValue.EditValue = keyValuePair.data;
							break;
						case "link":
							textEditLinkValue.EditValue = keyValuePair.data;
							break;
					}
				}
			}
		}

		public LibraryLinkEditor()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}
	}
}
