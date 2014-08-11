using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesDepot.Services.TickerService;
using SalesDepot.SiteManager.PresentationClasses.Ticker.LinkEditors;

namespace SalesDepot.SiteManager.ToolForms
{
	public partial class FormEditTickerLink : MetroForm
	{
		private ITickerEditor _editor;
		public TickerLink Link { get; set; }

		public FormEditTickerLink(bool newLink, TickerLink link)
		{
			InitializeComponent();
			Text = newLink ? "New Ticker Link" : "Edit Ticker Link";
			Link = link;
			comboBoxEditType.EditValue = Link.TypeString;
			memoEditText.EditValue = Link.text;
			comboBoxEditType.Properties.Items.AddRange(TickerLink.AvailableTypes);
			LinkTypeChanged();
			comboBoxEditType.EditValueChanged += (o, e) => LinkTypeChanged();
		}

		private void LinkTypeChanged()
		{
			pnDetails.Controls.Clear();
			if (_editor != null)
			{
				(_editor as Control).Dispose();
				_editor = null;
			}
			var type = comboBoxEditType.EditValue != null ? comboBoxEditType.EditValue.ToString() : string.Empty;
			Link.TypeString = type;
			switch (Link.type)
			{
				case "url":
				case "video":
				case "file":
					_editor = new FileEditor();
					_editor.LinkType = Link.type;
					break;
				case "link":
					_editor = new LibraryLinkEditor();
					break;
			}
			if (_editor == null) return;
			_editor.Details = Link.details ?? new KeyValuePair[] { };
			pnDetails.Controls.Add(_editor as Control);
		}

		private void FormEditTickerLink_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			if (DialogResult == DialogResult.OK)
			{
				if (comboBoxEditType.EditValue == null)
				{
					AppManager.Instance.ShowWarning("You did not selected link type");
					return;
				}
				if (memoEditText.EditValue == null)
				{
					AppManager.Instance.ShowWarning("You did not type the link text");
					return;
				}
				Link.TypeString = comboBoxEditType.EditValue.ToString();
				Link.text = memoEditText.EditValue.ToString();
				Link.details = _editor != null ? _editor.Details : new KeyValuePair[] { };
				e.Cancel = false;
			}
			else
				e.Cancel = false;
		}
	}
}
