using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(LibraryObjectLink))]
	//public partial class ExpiredDateOptions : UserControl, ILinkSetSettingsEditControl
	public partial class ExpiredDateOptions : XtraTabPage, ILinkSetSettingsEditControl
	{
		private readonly LibraryObjectLink _sourceLink;
		private readonly ILinksGroup _sourceLinkGroup;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.ExpirationDate };
		public int Order => 0;
		public bool AvailableForEmbedded => false;
		public SettingsEditorHeaderInfo HeaderInfo => null;

		public event EventHandler<EventArgs> ForceCloseRequested;

		private ExpiredDateOptions()
		{
			InitializeComponent();

			dateEditExpirationDate.Properties.NullDate = DateTime.MinValue;
			if ((CreateGraphics()).DpiX > 96)
			{
				laAddDateValue.Font = new Font(laAddDateValue.Font.FontFamily, laAddDateValue.Font.Size - 1, laAddDateValue.Font.Style);
				laExpireddateActions.Font = new Font(laExpireddateActions.Font.FontFamily, laExpireddateActions.Font.Size - 2, laExpireddateActions.Font.Style);
				checkBoxLabelLink.Font = new Font(checkBoxLabelLink.Font.FontFamily, checkBoxLabelLink.Font.Size - 2, checkBoxLabelLink.Font.Style);
				checkBoxEnableExpiredLinks.Font = new Font(checkBoxEnableExpiredLinks.Font.FontFamily, checkBoxEnableExpiredLinks.Font.Size - 2, checkBoxEnableExpiredLinks.Font.Style);
				checkBoxSendEmailWhenDelete.Font = new Font(checkBoxSendEmailWhenDelete.Font.FontFamily, checkBoxSendEmailWhenDelete.Font.Size - 2, checkBoxSendEmailWhenDelete.Font.Style);
			}
		}

		public ExpiredDateOptions(LibraryObjectLink sourceLink) : this()
		{
			_sourceLink = sourceLink;
		}

		public ExpiredDateOptions(ILinksGroup linkGroup, FileTypes? defaultLinkType = null) : this()
		{
			_sourceLinkGroup = linkGroup;
			_sourceLink = _sourceLinkGroup.AllLinks.OfType<LibraryObjectLink>()
				.FirstOrDefault(link => !defaultLinkType.HasValue || link.Type == defaultLinkType.Value);
		}

		public void LoadData()
		{
			laAddDateValue.Text = String.Format(laAddDateValue.Text, _sourceLink.AddDate.ToString("M/dd/yy"));
			dateEditExpirationDate.DateTime = _sourceLink.ExpirationSettings.ExpirationDate;
			timeEditExpirationTime.Time = _sourceLink.ExpirationSettings.ExpirationDate;
			checkBoxSendEmailWhenDelete.Checked = _sourceLink.ExpirationSettings.SendEmailOnSync;
			checkBoxLabelLink.Checked = _sourceLink.ExpirationSettings.MarkWhenExpired;
			checkBoxEnableExpiredLinks.Checked = _sourceLink.ExpirationSettings.Enable;
		}

		public void SaveData()
		{
			foreach (var link in (_sourceLinkGroup?.AllLinks ?? new[] {_sourceLink}).OfType<LibraryObjectLink>().ToList())
			{
				link.ExpirationSettings.ExpirationDate = new DateTime(dateEditExpirationDate.DateTime.Year,
					dateEditExpirationDate.DateTime.Month, dateEditExpirationDate.DateTime.Day, timeEditExpirationTime.Time.Hour,
					timeEditExpirationTime.Time.Minute, timeEditExpirationTime.Time.Second);
				link.ExpirationSettings.SendEmailOnSync = checkBoxSendEmailWhenDelete.Checked;
				link.ExpirationSettings.MarkWhenExpired = checkBoxLabelLink.Checked;
				link.ExpirationSettings.Enable = checkBoxEnableExpiredLinks.Checked;
			}
		}

		private void checkBoxEnableExpiredLinks_CheckedChanged(object sender, EventArgs e)
		{
			foreach (var control in Controls.OfType<Control>())
			{
				if (control == checkBoxEnableExpiredLinks) continue;
				control.Enabled = checkBoxEnableExpiredLinks.Checked;
			}
			if (checkBoxEnableExpiredLinks.Checked)
			{
				checkBoxEnableExpiredLinks.ForeColor = Color.Black;
				dateEditExpirationDate.ForeColor = Color.Black;
				timeEditExpirationTime.ForeColor = Color.Black;
				dateEditExpirationDate.DateTime = _sourceLink.ExpirationSettings.ExpirationDate;
				timeEditExpirationTime.Time = _sourceLink.ExpirationSettings.ExpirationDate;
			}
			else
			{
				checkBoxEnableExpiredLinks.ForeColor = Color.Gray;
				dateEditExpirationDate.ForeColor = Color.Gray;
				timeEditExpirationTime.ForeColor = Color.Gray;
				dateEditExpirationDate.EditValue = DateTime.MinValue;
				timeEditExpirationTime.Time = DateTime.MinValue;
			}
		}
	}
}
