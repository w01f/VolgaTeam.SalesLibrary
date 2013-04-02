using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using SalesDepot.Services.TickerService;

namespace SalesDepot.SiteManager.PresentationClasses.Ticker.LinkEditors
{
	[ToolboxItem(false)]
	public partial class FileEditor : UserControl, ITickerEditor
	{
		private string _linkType;
		public string LinkType
		{
			get { return _linkType; }
			set
			{
				_linkType = value;
				labelControlPathTitle.Text = TickerLink.GetTagTextByKey(_linkType, "path");
				labelControlPathTitle.Tag = "path";
			}
		}

		public KeyValuePair[] Details
		{
			get
			{
				var details = new List<KeyValuePair>();
				details.Add(new KeyValuePair() { tag = "path", data = textEditPathValue.EditValue != null ? textEditPathValue.EditValue.ToString() : null });
				return details.ToArray();
			}
			set
			{
				foreach (var keyValuePair in value)
				{
					textEditPathValue.EditValue = keyValuePair.data;
					break;
				}
			}
		}

		public FileEditor()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}
	}
}
