using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(LibraryObjectLink))]
	//public partial class ExpiredDateOptions : UserControl, ILinkSettingsEditControl
	public partial class ExpiredDateOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private readonly LibraryObjectLink _data;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.ExpirationDate };
		public int Order => 0;
		public bool AvailableForEmbedded => false;
		public SettingsEditorHeaderInfo HeaderInfo => null;

		public event EventHandler<EventArgs> ForceCloseRequested;

		public ExpiredDateOptions(LibraryObjectLink data)
		{
			InitializeComponent();
			_data = data;

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

		public void LoadData()
		{
			laAddDateValue.Text = String.Format(laAddDateValue.Text, _data.AddDate.ToString("M/dd/yy"));
			dateEditExpirationDate.DateTime = _data.ExpirationSettings.ExpirationDate;
			timeEditExpirationTime.Time = _data.ExpirationSettings.ExpirationDate;
			checkBoxSendEmailWhenDelete.Checked = _data.ExpirationSettings.SendEmailOnSync;
			checkBoxLabelLink.Checked = _data.ExpirationSettings.MarkWhenExpired;
			checkBoxEnableExpiredLinks.Checked = _data.ExpirationSettings.Enable;
		}

		public void SaveData()
		{
			_data.ExpirationSettings.ExpirationDate = new DateTime(dateEditExpirationDate.DateTime.Year, dateEditExpirationDate.DateTime.Month, dateEditExpirationDate.DateTime.Day, timeEditExpirationTime.Time.Hour, timeEditExpirationTime.Time.Minute, timeEditExpirationTime.Time.Second);
			_data.ExpirationSettings.SendEmailOnSync = checkBoxSendEmailWhenDelete.Checked;
			_data.ExpirationSettings.MarkWhenExpired = checkBoxLabelLink.Checked;
			_data.ExpirationSettings.Enable = checkBoxEnableExpiredLinks.Checked;
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
				dateEditExpirationDate.DateTime = _data.ExpirationSettings.ExpirationDate;
				timeEditExpirationTime.Time = _data.ExpirationSettings.ExpirationDate;
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
