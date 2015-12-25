using System;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class LinkExpirationSettings : SettingsContainer
	{
		private bool _enable;
		public bool Enable
		{
			get { return _enable; }
			set
			{
				if (_enable != value)
					OnSettingsChanged();
				_enable = value;
			}
		}

		private DateTime _expirationDate;
		public DateTime ExpirationDate
		{
			get { return _expirationDate; }
			set
			{
				if (_expirationDate != value)
					OnSettingsChanged();
				_expirationDate = value;
			}
		}

		private bool _markWhenExpired;
		public bool MarkWhenExpired
		{
			get { return _markWhenExpired; }
			set
			{
				if (_markWhenExpired != value)
					OnSettingsChanged();
				_markWhenExpired = value;
			}
		}

		private bool _sendEmailOnSync;
		public bool SendEmailOnSync
		{
			get { return _sendEmailOnSync; }
			set
			{
				if (_sendEmailOnSync != value)
					OnSettingsChanged();
				_sendEmailOnSync = value;
			}
		}

		[JsonIgnore]
		public bool IsExpired
		{
			get
			{
				if (Enable && ExpirationDate != DateTime.MinValue)
					return ((long)ExpirationDate.Subtract(DateTime.Now).TotalMilliseconds) < 0;
				return false;
			}
		}

		public LinkExpirationSettings()
		{
			MarkWhenExpired = true;
		}
	}
}
